using VisitorDTOs;

namespace VisitorBusinessLogic.Services.Interfaces
{
    public interface ICompanyServiceGeneric
    {
        Task<IEnumerable<CompanyDTO>> GetCompaniesAsync();
        Task<CompanyDTO> GetCompanyByIdAsync(long id);
        Task<CompanyDTO> AddCompanyAsync(CompanyDTO companyDto);
        Task UpdateCompanyAsync(CompanyDTO companyDto);
        Task DeleteCompanyAsync(long id);
    }
}
