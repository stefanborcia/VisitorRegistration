

using System.Linq.Expressions;
using VisitorDataAccess.Entities;

namespace VisitorDataAccess.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllRecordsAsync();
        Task<T> GetRecordsByIdAsync(long id);
        Task AddRecordsAsync(T entity);
        Task UpdateRecordsAsync(T entity);
        Task DeleteRecordsAsync(long id);
        Task<IEnumerable<Employee>> GetEmployeesByCompanyIdAsync(long companyId);
        Task<Company> GetCompanyByNameAsync(string name);
    }
}
