using Microsoft.AspNetCore.Mvc;
using VisitorBusinessLogic.Services.Interfaces;
using VisitorDTOs;

namespace VisitorAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CompanyGenericController : ControllerBase
    {
        private readonly ICompanyServiceGeneric _companyService;

        public CompanyGenericController(ICompanyServiceGeneric companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("companies")]
        public async Task<IEnumerable<CompanyDTO>> GetAllCompanies()
        {
            var companies = await _companyService.GetCompaniesAsync();
            return companies.Select(c => new CompanyDTO { Id = c.Id, Name = c.Name }).ToList();
        }

        [HttpGet("company/{id:long}")]
        public async Task<IActionResult> GetCompanyById([FromRoute] long id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            if (company == null)
                return NotFound(new { message = $"Company with ID {id} not found." });

            return Ok(company);
        }

        [HttpPost("company")]
        public async Task<IActionResult> AddCompany([FromBody] CompanyDTO companyDto)
        {
            if (companyDto == null)
            {
                return BadRequest("Company data is required.");
            }

            try
            {
                var createdCompany = await _companyService.AddCompanyAsync(companyDto);
                return CreatedAtAction(nameof(GetCompanyById), new { id = createdCompany.Id }, createdCompany);
            }
            catch (InvalidOperationException ex)
            {
                // If the company already exists, return a conflict status
                return Conflict(ex.Message);
            }
        }


        [HttpPut("company/{id:long}")]
        public async Task<IActionResult> UpdateCompany([FromRoute] long id, [FromBody] CompanyDTO companyDto)
        {
            if (id != companyDto.Id)
                return BadRequest(new { message = "Company ID mismatch." });

            await _companyService.UpdateCompanyAsync(companyDto);
            return NoContent();
        }

        [HttpDelete("company/{id:long}")]
        public async Task<IActionResult> DeleteCompany([FromRoute] long id)
        {
            await _companyService.DeleteCompanyAsync(id);
            return NoContent();
        }
    }
}
