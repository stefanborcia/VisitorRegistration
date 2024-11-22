using Microsoft.EntityFrameworkCore;
using VisitorDataAccess.Entities;
using VisitorDataAccess.Repositories.Interfaces;
using VisitorDTOs;

namespace VisitorDataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly VisitorDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(VisitorDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllRecordsAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetRecordsByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddRecordsAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRecordsAsync(long id)
        {
            var entity = await GetRecordsByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateRecordsAsync(T entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Employee>> GetEmployeesByCompanyIdAsync(long companyId)
        {
            return await _dbContext.Employees
                                   .Where(e => e.CompanyId == companyId)
                                   .ToListAsync();
        }
        public async Task<Company> GetCompanyByNameAsync(string name)
        {
            return await _dbContext.Set<Company>()
                                   .Where(c => c.Name.ToLower() == name.ToLower())
                                   .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<EmployeeWithCompanyDetailsDTO>> GetEmployeesWithCompanyAsync()
        {
            return await _dbContext.Employees

                .Select(e => new EmployeeWithCompanyDetailsDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    CompanyId = e.CompanyId,
                    CompanyName = e.Company.Name
                })
                .ToListAsync();
        }
        public async Task<Employee> GetVisitorByEmailAsync(string name)
        {
            return await _dbContext.Set<Employee>().FirstOrDefaultAsync(v => v.Name == name);
        }
    }
}
