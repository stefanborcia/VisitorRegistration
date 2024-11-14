using System.Collections;
using VisitorDTOs;

namespace VisitorBusinessLogic.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> GetEmployeesByCompanyId(long companyId);
    }
}
