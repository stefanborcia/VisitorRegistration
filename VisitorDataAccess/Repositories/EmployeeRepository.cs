using Microsoft.EntityFrameworkCore;
using VisitorDataAccess.Entities;
using VisitorDataAccess.Repositories.Interfaces;

namespace VisitorDataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly VisitorDbContext _context;

        public EmployeeRepository(VisitorDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByCompanyIdAsync(long companyId)
        {
            return await _context.Set<Employee>()
                                 .Where(e => e.CompanyId == companyId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _context.Set<Employee>().ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(long Id)
        {
            return await _context.Set<Employee>().FirstOrDefaultAsync(e => e.Id == Id);
            ;
        }
    }
}
