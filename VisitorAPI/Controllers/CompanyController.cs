using Microsoft.AspNetCore.Mvc;
using VisitorBusinessLogic.Services.Interfaces;
using VisitorDTOs;

namespace VisitorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("companies")]
        public async Task<ActionResult<List<CompanyDTO>>> GetCompanies()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return companies.Select(c => new CompanyDTO { Id = c.Id, Name = c.Name }).ToList();
        }
    }
}
