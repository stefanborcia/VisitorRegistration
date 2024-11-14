using Microsoft.EntityFrameworkCore;
using System.Collections;
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

        public async Task<IEnumerable<Employee>> GetEmployeesByCompanyId(long companyId)
        {
            return await _context.Set<Employee>()
                                 .Where(e => e.CompanyId == companyId)
                                 .ToListAsync();
        }
    }
}
