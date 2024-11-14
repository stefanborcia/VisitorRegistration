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
    }
}
