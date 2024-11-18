using VisitorBusinessLogic.Services.Interfaces;
using VisitorDataAccess.Entities;
using VisitorDataAccess.Repositories.Interfaces;
using VisitorDTOs;

namespace VisitorBusinessLogic.Services
{
    public class CompanyServiceGeneric : ICompanyServiceGeneric
    {
        private readonly IGenericRepository<Company> _repository;

        public CompanyServiceGeneric(IGenericRepository<Company> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CompanyDTO>> GetCompaniesAsync()
        {
            var companies = await _repository.GetAllRecordsAsync();
            return companies.Select(c => new CompanyDTO { Id = c.Id, Name = c.Name });
        }

        public async Task<CompanyDTO> GetCompanyByIdAsync(long id)
        {
            var company = await _repository.GetRecordsByIdAsync(id);
            if (company == null)
                return null;

            return new CompanyDTO { Id = company.Id, Name = company.Name };
        }

        public async Task<CompanyDTO> AddCompanyAsync(CompanyDTO companyDto)
        {
            var company = new Company
            {
                Name = companyDto.Name
            };

            await _repository.AddRecordsAsync(company);

            companyDto.Id = company.Id;
            return companyDto;
        }

        public async Task UpdateCompanyAsync(CompanyDTO companyDto)
        {
            var existingCompany = await _repository.GetRecordsByIdAsync(companyDto.Id);
            if (existingCompany == null)
            {
                throw new KeyNotFoundException($"Company with ID {companyDto.Id} does not exist.");
            }

            existingCompany.Name = companyDto.Name;

            await _repository.UpdateRecordsAsync(existingCompany);
        }

        public async Task DeleteCompanyAsync(long id)
        {
            await _repository.DeleteRecordsAsync(id);
        }
    }
}
