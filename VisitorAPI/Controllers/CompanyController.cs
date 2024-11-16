using Microsoft.AspNetCore.Mvc;
using VisitorBusinessLogic.Services.Interfaces;
using VisitorDataAccess.Entities;
using VisitorDTOs;

namespace VisitorAPI.Controllers
{
    [Route("/api")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("/company")]
        public async Task<ActionResult<List<CompanyDTO>>> GetCompanies()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return companies.Select(c => new CompanyDTO { Id = c.Id, Name = c.Name }).ToList();
        }

        [HttpGet("/company/{id}")]
        public async Task<ActionResult<CompanyDTO>> GetCompanies(long id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);

            if (company == null)
            {
                return NotFound(new { Message = $"Company with ID {id} not found." });
            }

            return Ok(company);
        }
    }
}
