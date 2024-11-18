using Microsoft.AspNetCore.Mvc;
using VisitorBusinessLogic.Services.Interfaces;
using VisitorDTOs;

namespace VisitorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyGenericController : ControllerBase
    {
        private readonly ICompanyServiceGeneric _companyService;

        public CompanyGenericController(ICompanyServiceGeneric companyService) => _companyService = companyService;

        [HttpGet("/companies")]
        public async Task<ActionResult<IEnumerable<CompanyDTO>>> GetAllCompanies()
        {
            var companies = await _companyService.GetCompaniesAsync();
            if (companies == null || !companies.Any())
                return NotFound(new { message = "No companies found." });

            return Ok(new { message = "Companies retrieved successfully.", data = companies });
        }

        [HttpGet("/company/{id}")]
        public async Task<ActionResult<CompanyDTO>> GetCompanyById(long id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            if (company == null)
                return NotFound(new { message = $"Company with ID {id} not found." });

            return Ok(new { message = "Company retrieved successfully.", data = company });
        }

        [HttpPost("/add/company")]
        public async Task<ActionResult> AddCompany(CompanyDTO companyDto)
        {
            await _companyService.AddCompanyAsync(companyDto);
            return CreatedAtAction(nameof(GetCompanyById), new { id = companyDto.Id }, new { message = "Company added successfully.", data = companyDto });
        }

        [HttpPut("/edit/company/{id}")]
        public async Task<ActionResult> UpdateCompany(long id, CompanyDTO companyDto)
        {
            if (id != companyDto.Id)
                return BadRequest(new { message = "Company ID mismatch." });

            await _companyService.UpdateCompanyAsync(companyDto);
            return Ok(new { message = "Company updated successfully." });
        }

        [HttpDelete("/delete/company/{id}")]
        public async Task<ActionResult> DeleteCompany(long id)
        {
            await _companyService.DeleteCompanyAsync(id);
            return Ok(new { message = "Company deleted successfully." });
        }
    }
}
