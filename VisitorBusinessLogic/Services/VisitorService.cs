using VisitorBusinessLogic.Validation;
using VisitorDataAccess.Entities;
using VisitorDataAccess.Repositories.Interfaces;
using VisitorDTOs;

namespace VisitorBusinessLogic.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly IVisitorRepository _visitorRepository;
        private readonly ICompanyRepository _companyRepository;
        public VisitorService(IVisitorRepository visitorRepository,ICompanyRepository companyRepository )
        {
            _visitorRepository = visitorRepository;
            _companyRepository = companyRepository;
        }

        // Implement the RegisterVisitorAsync method
        public async Task RegisterVisitorAsync(SignInVisitorDTO visitorDto)
        {
            ValidateVisitor(visitorDto);

            var company = await _companyRepository.GetCompanyByIdAsync(visitorDto.VisitingCompanyId)
                          ?? throw new Exception("Company not found.");

            var employee = await _companyRepository.GetEmployeeByIdAsync(visitorDto.AppointmentWithId)
                          ?? throw new Exception("Employee not found.");

            if (employee.CompanyId != visitorDto.VisitingCompanyId)
                throw new Exception("Employee does not belong to the selected company.");

            var visitor = new Visitor { Name = visitorDto.Name, Email = visitorDto.Email, Company = visitorDto.Company };
            await _visitorRepository.AddVisitorAsync(visitor);

            var visit = new Visit { Visitor = visitor, VisitingCompany = company, AppointmentWith = employee, StartTime = DateTime.Now };
            await _visitorRepository.CreateVisitAsync(visit);

        }

        private void ValidateVisitor(SignInVisitorDTO visitorDto)
        {
            var nameValidationResult = VisitorValidator.ValidateName(visitorDto.Name);
            if (!nameValidationResult.IsSuccess)
                throw new Exception(nameValidationResult.ErrorMessage);

            var emailValidationResult = VisitorValidator.ValidateEmail(visitorDto.Email);
            if (!emailValidationResult.IsSuccess)
                throw new Exception(emailValidationResult.ErrorMessage);

            if (!VisitorValidator.ValidateVisitingCompanyId(visitorDto.VisitingCompanyId).IsSuccess)
                throw new Exception("The visiting company is required.");
            if (!VisitorValidator.ValidateAppointmentWithId(visitorDto.AppointmentWithId).IsSuccess)
                throw new Exception("The employee which you have appointment is required.");
        }
    }
}