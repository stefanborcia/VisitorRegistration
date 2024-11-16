using VisitorDataAccess.Entities;

namespace VisitorDataAccess.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<IEnumerable<Employee>> GetEmployeesByCompanyIdAsync(long companyId);
        Task<Employee> GetEmployeeByIdAsync(long id);
    }
}
