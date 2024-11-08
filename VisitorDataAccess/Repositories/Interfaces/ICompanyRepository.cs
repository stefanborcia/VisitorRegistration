using VisitorDataAccess.Entities;

namespace VisitorDataAccess.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company> GetCompanyByIdAsync(long companyId);
        Task<Employee> GetEmployeeByIdAsync(long employeeId);
        Task<IEnumerable<Company>> GetAllCompaniesAsync(); 
        Task<IEnumerable<Employee>> GetEmployeesByCompanyIdAsync(long companyId);
    }
}
