using System.Collections;
using VisitorDataAccess.Entities;
using VisitorDTOs;

namespace VisitorBusinessLogic.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> GetEmployeesByCompanyIdAsync(long companyId);
        Task<List<EmployeeDTO>> GetEmployeesAsync();
        Task<EmployeeDTO> GetEmployeesByIdAsync(long Id);
    }
}
