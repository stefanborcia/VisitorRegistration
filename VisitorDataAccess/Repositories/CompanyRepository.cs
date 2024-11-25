using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorDataAccess.Entities;
using VisitorDataAccess.Repositories.Interfaces;

namespace VisitorDataAccess.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        private readonly VisitorDbContext _dbContext;

        public CompanyRepository(VisitorDbContext context) : base(context)
        {
            _dbContext = context;
        }

        // Get company by name (case insensitive)
        public async Task<Company?> GetCompanyByNameAsync(string name)
        {
            return await _dbContext.Companies
                .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
        }
    }
}
