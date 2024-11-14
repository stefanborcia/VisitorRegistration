using Microsoft.EntityFrameworkCore;
using VisitorDataAccess.Entities;
using VisitorDataAccess.Repositories.Interfaces;

namespace VisitorDataAccess.Repositories
{
    public class CompanyRepository:ICompanyRepository
    {
        private readonly VisitorDbContext _dbContext;

        public CompanyRepository(VisitorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Company> GetCompanyByIdAsync(long companyId)
        {
            return await _dbContext.Set<Company>().FirstOrDefaultAsync(c => c.Id == companyId);
        }

        public async Task<Employee> GetEmployeeByIdAsync(long employeeId)
        {
            return await _dbContext.Set<Employee>().FirstOrDefaultAsync(e => e.Id == employeeId);
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            return await _dbContext.Set<Company>().ToListAsync();
        }
    }
}
