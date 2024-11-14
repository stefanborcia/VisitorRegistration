using System.Collections;
using VisitorBusinessLogic.Services.Interfaces;
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

        public async Task<List<EmployeeDTO>> GetEmployeesByCompanyId(long companyId)
        {
            // Get the employees for the given companyId
            var employees = await _employeeRepository.GetEmployeesByCompanyId(companyId);

            // You can map the data if necessary
            return employees.Select(e => new EmployeeDTO
            {
                Id = e.Id,
                Name = e.Name
            }).ToList();
        }
    }
}
