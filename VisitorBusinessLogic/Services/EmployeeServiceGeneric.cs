using VisitorBusinessLogic.Exceptions;
using VisitorBusinessLogic.Services.Interfaces;
using VisitorDataAccess.Entities;
using VisitorDataAccess.Repositories.Interfaces;
using VisitorDTOs;

namespace VisitorBusinessLogic.Services
{
    public class EmployeeServiceGeneric : IEmployeeServiceGeneric
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeServiceGeneric(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync()
        {
            var employees = await _repository.GetAllAsync();
            return employees.Select(e => new EmployeeDTO { Id = e.Id, Name = e.Name, CompanyId = e.CompanyId });
        }

        public async Task<IEnumerable<EmployeeWithCompanyDetailsDTO>> GetEmployeesWithCompanyAsync()
        {
            return await _repository.GetEmployeesWithCompanyAsync();
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesByCompanyIdAsync(long companyId)
        {
            var employees = await _repository.GetEmployeesByCompanyIdAsync(companyId);
            return employees.Select(e => new EmployeeDTO { Id = e.Id, Name = e.Name, CompanyId = e.CompanyId });
        }

        public async Task<EmployeeDTO?> GetEmployeeByIdAsync(long id)
        {
            var employee = await _repository.GetByIdAsync(id);
            return employee == null ? null : new EmployeeDTO { Id = employee.Id, Name = employee.Name, CompanyId = employee.CompanyId };
        }

        public async Task<EmployeeDTO> AddEmployeeAsync(long companyId, EmployeeDTO employeeDto)
        {
            // Await the result of GetAllAsync
            var employees = await _repository.GetAllAsync();

            // Check if an employee with the same name exists for the given company
            var existingEmployee = employees.FirstOrDefault(e => e.Name == employeeDto.Name);

            if (existingEmployee != null)
            {
                // Throw the exception if a duplicate is found
                throw new DuplicateEmployeeNameException($"Employee with name: {employeeDto.Name} already exists.");
            }

            //add the employee if no duplicate is found
            var employee = new Employee
            {
                Name = employeeDto.Name,
                CompanyId = companyId
            };

            await _repository.AddAsync(employee);

            // Return the created EmployeeDTO
            return new EmployeeDTO { Id = employee.Id, Name = employee.Name, CompanyId = employee.CompanyId };
        }


        public async Task UpdateEmployeeAsync(EmployeeDTO employeeDto)
        {
            var existingEmployee = await _repository.GetByIdAsync(employeeDto.Id);
            if (existingEmployee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {employeeDto.Id} does not exist.");
            }

            existingEmployee.Name = employeeDto.Name;
            existingEmployee.CompanyId = employeeDto.CompanyId;

            await _repository.UpdateAsync(existingEmployee);
        }

        public async Task DeleteEmployeeAsync(long id)
        {
            // Check if the company exists
            var company = await _repository.GetByIdAsync(id);
            if (company == null)
            {
                throw new KeyNotFoundException($"Company with ID {id} does not exist.");
            }

            // Implement the soft delete
            await _repository.SoftDeleteAsync(id);
        }
    }
}
