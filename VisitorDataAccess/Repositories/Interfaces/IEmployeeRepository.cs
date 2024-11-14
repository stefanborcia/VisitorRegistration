using System;
using VisitorDataAccess.Entities;
using VisitorDTOs;

namespace VisitorDataAccess.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesByCompanyId(long companyId);

    }
}
