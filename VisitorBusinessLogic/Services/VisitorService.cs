using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VisitorBusinessLogic.Exceptions;
using VisitorBusinessLogic.Services.Interfaces;
using VisitorBusinessLogic.Validation;
using VisitorDataAccess.Entities;
using VisitorDataAccess.Repositories.Interfaces;
using VisitorDTOs;
using VisitorDTOs.VisitorDTO;
using Action = VisitorDataAccess.Entities.Action;
using ValidationException = VisitorBusinessLogic.Exceptions.ValidationException;

namespace VisitorBusinessLogic.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly IVisitorRepository _visitorRepository;
        private readonly IVisitRepository _visitRepository;
        private readonly IGenericRepository<Company> _companyRepository;
        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IGenericRepository<VisitorLog> _logRepository;
        private readonly IValidator<SignInVisitorDTO> _signInValidator;
        private readonly IValidator<SignOutVisitorDTO> _signOutValidator;

        public VisitorService(
            IVisitorRepository visitorRepository,
            IVisitRepository visitRepository,
            IGenericRepository<Company> companyRepository,
            IGenericRepository<Employee> employeeRepository,
            IGenericRepository<VisitorLog> logRepository,
            IValidator<SignInVisitorDTO> signInValidator,
            IValidator<SignOutVisitorDTO> signOutValidator)
        {
            _visitorRepository = visitorRepository;
            _companyRepository = companyRepository;
            _employeeRepository = employeeRepository;
            _visitRepository = visitRepository;
            _logRepository = logRepository;
            _signInValidator = signInValidator;
            _signOutValidator = signOutValidator;
        }

        public async Task<Visit> RegisterVisitorAsync(SignInVisitorDTO visitorDto)
        {
            // Validate the DTO
            var validationResult = await _signInValidator.ValidateAsync(visitorDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }
            // Check if the visitor already exists in the database
            var visitor = await _visitorRepository.GetVisitorByEmailAsync(visitorDto.Email)
                         ?? new Visitor
                         {
                             Name = visitorDto.Name,
                             Email = visitorDto.Email,
                             Company = visitorDto.Company
                         };

            if (visitor.Id == 0)
            {
                // Add visitor if not found
                await _visitorRepository.AddAsync(visitor);
            }

            // Check for an active visit
            var activeVisit = await _visitorRepository.GetActiveVisitByVisitorAsync(visitor.Id);
            if (activeVisit != null)
            {
                throw new VisitorAlreadySignedInException("Visitor is already signed in.");
            }

            // Create and store a new visit
            var newVisit = await CreateAndStoreVisitAsync(visitor, visitorDto);
            return newVisit;
        }

        private async Task<Visit> CreateAndStoreVisitAsync(Visitor visitor, SignInVisitorDTO visitorDto)
        {
            // Fetch the visiting company by its ID using the generic repository.
            var visitingCompany = await _companyRepository.GetByIdAsync(visitorDto.VisitingCompanyId)
                ?? throw new Exception("Visiting company not found.");

            // Fetch the employee by their ID using the generic repository.
            var appointmentWith = await _employeeRepository.GetByIdAsync(visitorDto.AppointmentWithId)
                ?? throw new Exception("Appointment employee not found.");

            // Create a new visit
            var visit = new Visit
            {
                Visitor = visitor,
                VisitingCompany = visitingCompany,
                AppointmentWith = appointmentWith,
                StartTime = DateTime.Now,
                CurrentStatus = Action.SignIn
            };
            // Store the visit in the database (use the visit repository)
            await _visitRepository.AddAsync(visit);

            return visit;
        }

        public async Task SignOutVisitorAsync(SignOutVisitorDTO visitorDto)
        {
            // Validate the DTO
            var validationResult = await _signOutValidator.ValidateAsync(visitorDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            // Find the visitor
            var visitor = await _visitorRepository.GetVisitorByEmailAsync(visitorDto.Email);
                if (visitor == null)
                {
                    throw new VisitorNotSignedInException("Visitor not found.");
                }

                // Check for an active visit using the VisitRepository
                var activeVisit = await _visitRepository.GetActiveVisitByVisitorAsync(visitor.Id);
                if (activeVisit == null)
                {
                    throw new VisitorNotSignedInException("Visitor does not have an active visit. Please ensure the visitor is signed in.");
                }

                // Update the visit's EndTime and status
                activeVisit.EndTime = DateTime.Now;

                // Ensure EndTime is after StartTime
                if (activeVisit.EndTime < activeVisit.StartTime)
                {
                    // Handle the case where EndTime is before StartTime
                    throw new InvalidOperationException("EndTime cannot be earlier than StartTime.");
                }

                activeVisit.CurrentStatus = Action.SignOut;

                // Calculate time spent
                var timeSpent = activeVisit.EndTime.Value - activeVisit.StartTime;

                // Log the visit
                var visitorLog = new VisitorLog
                {
                    Visit = activeVisit,
                    Actions = Action.SignOut,
                    TimeSpent = timeSpent
                };

                // Save updates
                await _visitRepository.UpdateVisitAsync(activeVisit);
                await _logRepository.AddAsync(visitorLog);
            
        }

        public async Task<IEnumerable<VisitorMonitoringDTO>> GetVisitorMonitoringAsync()
        {
            return await _visitorRepository.GetVisitorMonitoringAsync();
        }

        public async Task<IEnumerable<VisitorRegistrationSearchDTO>> GetVisitorRegistrationSearchAsync(string search)
        {
            return await _visitorRepository.GetVisitorRegistrationSearchAsync(search);
        }
    }
}
