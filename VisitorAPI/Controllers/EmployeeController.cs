using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VisitorBusinessLogic.Services.Interfaces;
using VisitorDataAccess.Repositories.Interfaces;
using VisitorDTOs;

namespace VisitorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("{companyId}/employees")]
        public async Task<IActionResult> GetEmployeesByCompanyId(long companyId)
        {
            var employees = await _employeeService.GetEmployeesByCompanyId(companyId);

            if (employees == null || !employees.Any())
                return NotFound("No employees found for the given company ID.");

            return Ok(employees.Select(e => new { e.Id, e.Name }));
        }
    }
}
