using VisitorBusinessLogic.Services.Interfaces;
using VisitorDataAccess.Entities;
using VisitorDataAccess.Repositories.Interfaces;
using VisitorDTOs;

namespace VisitorBusinessLogic.Services
{
    public class CompanyServiceGeneric : ICompanyServiceGeneric
    {
        private readonly ICompanyRepository _repository;

        public CompanyServiceGeneric(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CompanyDTO>> GetCompaniesAsync()
        {
            var companies = await _repository.GetAllAsync();
            return companies.Select(c => new CompanyDTO { Id = c.Id, Name = c.Name });
        }

        public async Task<CompanyDTO?> GetCompanyByIdAsync(long id)
        {
            var company = await _repository.GetByIdAsync(id);
            return company == null ? null : new CompanyDTO { Id = company.Id, Name = company.Name };
        }

        public async Task<CompanyDTO> AddCompanyAsync(CompanyDTO companyDto)
        {
            var existingCompany = await _repository.GetCompanyByNameAsync(companyDto.Name);
            if (existingCompany != null)
            {
                throw new InvalidOperationException("A company with the same name already exists.");
            }

            var company = new Company { Name = companyDto.Name };
            await _repository.AddAsync(company);

            return new CompanyDTO { Id = company.Id, Name = company.Name };
        }

        public async Task UpdateCompanyAsync(CompanyDTO companyDto)
        {
            var existingCompany = await _repository.GetByIdAsync(companyDto.Id);
            if (existingCompany == null)
            {
                throw new KeyNotFoundException($"Company with ID {companyDto.Id} does not exist.");
            }

            existingCompany.Name = companyDto.Name;
            await _repository.UpdateAsync(existingCompany);
        }

        public async Task DeleteCompanyAsync(long id)
        {
            // Check if the employee exists
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} does not exist.");
            }

            // Implement the soft delete
            await _repository.SoftDeleteAsync(id);
        }
    }
}
