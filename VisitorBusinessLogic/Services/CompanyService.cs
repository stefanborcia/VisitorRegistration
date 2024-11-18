using VisitorBusinessLogic.Services.Interfaces;
using VisitorDataAccess.Repositories.Interfaces;
using VisitorDTOs;

namespace VisitorBusinessLogic.Services
{
    public class CompanyService:ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<CompanyDTO>> GetAllCompaniesAsync()
        {
            var companies = await _companyRepository.GetAllCompaniesAsync();
            return companies.Select(c => new CompanyDTO { Id = c.Id, Name = c.Name });
        }
        public async Task<CompanyDTO> GetCompanyByIdAsync(long companyId)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(companyId);
            if (company == null) return null; 

            return new CompanyDTO
            {
                Id = company.Id,
                Name = company.Name,
            };
        }
    }
}
