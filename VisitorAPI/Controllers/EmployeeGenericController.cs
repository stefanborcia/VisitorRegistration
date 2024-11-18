using Microsoft.AspNetCore.Mvc;
using VisitorBusinessLogic.Services.Interfaces;
using VisitorDTOs;

namespace VisitorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeGenericController : ControllerBase
    {
        private readonly IEmployeeServiceGeneric _employeeService;

        public EmployeeGenericController(IEmployeeServiceGeneric employeeService) => _employeeService = employeeService;

        [HttpGet("/employees")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetEmployeesAsync();
            return Ok(new
            {
                message = "Employees retrieved successfully.",
                data = employees
            });
        }

        [HttpGet("/employee/{id}")]
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

        [HttpPost("/add/employee/")]
       public async Task<ActionResult> AddEmployee(long companyId, [FromBody] EmployeeDTO employeeDto)
        {
            try
            {
                await _employeeService.AddEmployeeAsync(companyId, employeeDto);
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
        }

        [HttpPut("/edit/employee/{id}")]
        public async Task<ActionResult> UpdateEmployee(long id, EmployeeDTO employeeDto)
        {
            if (id != employeeDto.Id)
                return BadRequest("Employee ID mismatch.");

            await _employeeService.UpdateEmployeeAsync(employeeDto);
            return Ok(new { message = "Employee updated successfully.", data = employeeDto });
        }

        [HttpDelete("/delete/employee/{id}")]
        public async Task<ActionResult> DeleteEmployee(long id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return Ok(new { message = "Employee deleted successfully." });
        }
    }
}
