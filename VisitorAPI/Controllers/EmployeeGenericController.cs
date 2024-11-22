using Microsoft.AspNetCore.Mvc;
using VisitorBusinessLogic.Exceptions;
using VisitorBusinessLogic.Services.Interfaces;
using VisitorDataAccess.Entities;
using VisitorDTOs;

namespace VisitorAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class EmployeeGenericController : ControllerBase
    {
        private readonly IEmployeeServiceGeneric _employeeService;

        public EmployeeGenericController(IEmployeeServiceGeneric employeeService) => _employeeService = employeeService;

        [HttpGet("employees")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetEmployeesAsync();
            if (employees == null)
                return NotFound(new { message = "Employees not found." });

            return Ok(new
            {
                message = "Employees retrieved successfully.",
                data = employees
            });
        }

        [HttpGet("employeesWithCompanyName")]
        public async Task<ActionResult<IEnumerable<EmployeeWithCompanyDetailsDTO>>> GetEmployeesWithCompanyAsync()
        {

            var employees = await _employeeService.GetEmployeesWithCompanyAsync();
            if (employees == null)
                return NotFound(new { message = "Employees not found." });

            return Ok(employees);
        }

        [HttpGet("company/{companyId}/employees")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployeesByCompanyId(long companyId)
        {
            var employees = await _employeeService.GetEmployeesByCompanyIdAsync(companyId);
            if (employees == null || !employees.Any())
                return NotFound(new { message = "No employees found for this company." });

            return Ok(employees);
        }
        [HttpGet("employee/{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeById(long id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound(new { message = "Employee not found." });

            return Ok(new
            {
                message = "Employee details retrieved successfully.",
                data = employee
            });
        }

        [HttpPost("employee/")]
        public async Task<ActionResult> AddEmployee(long companyId, [FromBody] EmployeeDTO employeeDto)
        {
            try
            {
                await _employeeService.AddEmployeeAsync(employeeDto.CompanyId, employeeDto);
                return Ok(new
                {
                    message = "Employee added successfully.",
                    data = employeeDto
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (DuplicateEmployeeNameException ex)
            {
                // Handle the duplicate employee name exception
                return BadRequest(new { Errors = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Errors = ex.Message });
            }
        }

        [HttpPut("employee/{id}")]
        public async Task<ActionResult> UpdateEmployee(long id, EmployeeDTO employeeDto)
        {
            if (id != employeeDto.Id)
                return BadRequest("Employee ID mismatch.");

            await _employeeService.UpdateEmployeeAsync(employeeDto);
            return Ok(new { message = "Employee updated successfully.", data = employeeDto });
        }

        [HttpDelete("employee/{id}")]
        public async Task<ActionResult> DeleteEmployee(long id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return Ok(new { message = "Employee deleted successfully." });
        }
    }
}
