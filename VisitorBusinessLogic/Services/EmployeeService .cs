using System.Collections;
using VisitorBusinessLogic.Services.Interfaces;
using VisitorDataAccess.Entities;
using VisitorDataAccess.Repositories.Interfaces;
using VisitorDTOs;

namespace VisitorBusinessLogic.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeDTO>> GetEmployeesByCompanyIdAsync(long companyId)
        {
            // Get the employees for the given companyId
            var employees = await _employeeRepository.GetEmployeesByCompanyIdAsync(companyId);

            // You can map the data if necessary
            return employees.Select(e => new EmployeeDTO
            {
                Id = e.Id,
                Name = e.Name
            }).ToList();
        }

        public async Task<List<EmployeeDTO>> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetEmployeesAsync();

            return employees.Select(e => new EmployeeDTO
            {
                Id = e.Id,
                Name = e.Name

            }).ToList();
        }

        public async Task<EmployeeDTO> GetEmployeesByIdAsync(long Id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(Id);
            if (employee == null) return null; 

            return new EmployeeDTO
            {
                Id = employee.Id,
                Name = employee.Name,
            };
        }
    }
}
