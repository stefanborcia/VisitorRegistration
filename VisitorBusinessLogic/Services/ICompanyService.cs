using VisitorDTOs;

namespace VisitorBusinessLogic.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDTO>> GetAllCompaniesAsync();
        Task<IEnumerable<EmployeeDTO>> GetEmployeesByCompanyIdAsync(long companyId);
    }
}
