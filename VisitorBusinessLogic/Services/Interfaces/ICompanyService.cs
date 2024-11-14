using VisitorDTOs;

namespace VisitorBusinessLogic.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDTO>> GetAllCompaniesAsync();
    }
}
