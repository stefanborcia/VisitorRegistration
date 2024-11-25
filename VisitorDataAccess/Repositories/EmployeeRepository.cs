using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorDataAccess.Entities;
using VisitorDataAccess.Repositories.Interfaces;
using VisitorDTOs;

namespace VisitorDataAccess.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly VisitorDbContext _dbContext;

        public EmployeeRepository(VisitorDbContext context) : base(context)
        {
            _dbContext = context;
        }

        // Get employees by company ID
        public async Task<IEnumerable<Employee>> GetEmployeesByCompanyIdAsync(long companyId)
        {
            return await _dbContext.Employees
                                   .Where(e => e.CompanyId == companyId)
                                   .ToListAsync();
        }

        // Get employees with their company details
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
    }
}
