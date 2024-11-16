using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VisitorBusinessLogic.Services.Interfaces;
using VisitorDataAccess.Entities;
using VisitorDataAccess.Repositories.Interfaces;
using VisitorDTOs;

namespace VisitorAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("/company{companyId}/employee")]
        public async Task<IActionResult> GetEmployeesByCompanyId(long companyId)
        {
            var employees = await _employeeService.GetEmployeesByCompanyIdAsync(companyId);

            if (employees == null || !employees.Any())
                return NotFound("No employees found for the given company ID.");

            return Ok(employees.Select(e => new { e.Id, e.Name }));
        }

        [HttpGet("/employee")]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeService.GetEmployeesAsync();

            if (employees == null || !employees.Any())
                return NotFound("No employees found.");

            return Ok(employees.Select(e => new { e.Id, e.Name }));
        }

        [HttpGet("/employee{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeesById(long id)
        {
            var employees = await _employeeService.GetEmployeesByIdAsync(id);

            if (employees == null)
                return NotFound("No employees found for the given employee ID.");

            return employees;
        }
    }
}
