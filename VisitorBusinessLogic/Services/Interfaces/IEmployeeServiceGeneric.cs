using VisitorDTOs;

namespace VisitorBusinessLogic.Services.Interfaces
{
    public interface IEmployeeServiceGeneric
    {
        Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync();
        Task<EmployeeDTO> GetEmployeeByIdAsync(long id);
        Task<EmployeeDTO> AddEmployeeAsync(long companyId, EmployeeDTO employeeDto); 
        Task UpdateEmployeeAsync(EmployeeDTO employeeDto);
        Task DeleteEmployeeAsync(long id);
        Task<IEnumerable<EmployeeDTO>> GetEmployeesByCompanyIdAsync(long companyId);
    }
}
