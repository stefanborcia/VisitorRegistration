using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorDataAccess.Entities;
using VisitorDTOs;

namespace VisitorDataAccess.Repositories.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetEmployeesByCompanyIdAsync(long companyId);
        Task<IEnumerable<EmployeeWithCompanyDetailsDTO>> GetEmployeesWithCompanyAsync();
        Task SoftDeleteAsync(long id);
    }
}
