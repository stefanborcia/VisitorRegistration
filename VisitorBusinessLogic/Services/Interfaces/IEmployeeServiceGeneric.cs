using VisitorDTOs;

namespace VisitorBusinessLogic.Services.Interfaces
{
    public interface IEmployeeServiceGeneric
    {
        Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync();
        Task<IEnumerable<EmployeeWithCompanyDetailsDTO>> GetEmployeesWithCompanyAsync();
        Task<IEnumerable<EmployeeDTO>> GetEmployeesByCompanyIdAsync(long companyId);
        Task<EmployeeDTO?> GetEmployeeByIdAsync(long id);
        Task<EmployeeDTO> AddEmployeeAsync(long companyId, EmployeeDTO employeeDto);
        Task UpdateEmployeeAsync(EmployeeDTO employeeDto);
        Task DeleteEmployeeAsync(long id);
    }
}
