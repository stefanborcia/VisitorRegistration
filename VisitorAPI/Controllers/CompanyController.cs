using Microsoft.AspNetCore.Mvc;
using VisitorBusinessLogic.Services;

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
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return Ok(companies);
        }

        [HttpGet("companies/{companyId}/employees")]
        public async Task<IActionResult> GetEmployeesByCompany(long companyId)
        {
            var employees = await _companyService.GetEmployeesByCompanyIdAsync(companyId);
            if (!employees.Any())
                return NotFound("No employees found for the given company.");

            return Ok(employees);
        }
    }
}
