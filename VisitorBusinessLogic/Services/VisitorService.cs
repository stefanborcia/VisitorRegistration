using FluentValidation;
using VisitorBusinessLogic.Exceptions;
using VisitorBusinessLogic.Services.Interfaces;
using VisitorBusinessLogic.Validation;
using VisitorDataAccess.Entities;
using VisitorDataAccess.Repositories.Interfaces;
using VisitorDTOs;
using Action = VisitorDataAccess.Entities.Action;
using ValidationException = VisitorBusinessLogic.Exceptions.ValidationException;

namespace VisitorBusinessLogic.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly IVisitorRepository _visitorRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public VisitorService(
            IVisitorRepository visitorRepository,
            ICompanyRepository companyRepository,
            IEmployeeRepository employeeRepository)
        {
            _visitorRepository = visitorRepository;
            _companyRepository = companyRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<Visit> RegisterVisitorAsync(SignInVisitorDTO visitorDto)
        {
            // Validate the input 
            var validator = new VisitorValidator();
            var validationResult = await validator.ValidateAsync(visitorDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidationException(errors);
            }

            // Check if the visitor already exists in the database
            var visitor = await _visitorRepository.GetVisitorByEmailAsync(visitorDto.Email);
            if (visitor == null)
            {
                // Create a new visitor if not found
                visitor = new Visitor
                {
                    Name = visitorDto.Name,
                    Email = visitorDto.Email,
                    Company = visitorDto.Company
                };
                await _visitorRepository.AddVisitorAsync(visitor);
            }

            // Check if the visitor already has an active visit
            var activeVisit = await _visitorRepository.GetActiveVisitByVisitorAsync(visitor.Id);
            if (activeVisit != null)
            {
                throw new VisitorAlreadySignedInException("Visitor is already signed in.");
            }

            // Check if the employee is available for the appointment
            await EnsureEmployeeAvailability(visitorDto.AppointmentWithId);

            // Create and store a new visit
            var newVisit = await CreateAndStoreVisitAsync(visitor, visitorDto);
            return newVisit;
        }

        private async Task EnsureEmployeeAvailability(long appointmentWithId)
        {
            // Fetch all active visits for the employee
            var activeVisits = await _visitorRepository.GetActiveVisitsByEmployeeAsync(appointmentWithId);

            // Check if any active visit conflicts with the current request
            if (activeVisits.Any())
            {
                throw new Exception($"The employee with ID {appointmentWithId} is currently unavailable.");
            }
        }

        private async Task<Visit> CreateAndStoreVisitAsync(Visitor visitor, SignInVisitorDTO visitorDto)
        {
            // Fetch the visiting company by its ID from the repository.
            var visitingCompany = await _companyRepository.GetCompanyByIdAsync(visitorDto.VisitingCompanyId)
                                ?? throw new Exception("Visiting company not found.");

            // Fetch the employee by their ID from the repository to verify the appointment.
            var appointmentWith = await _employeeRepository.GetEmployeeByIdAsync(visitorDto.AppointmentWithId)
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

            // Store the visit in the database
            await _visitorRepository.CreateVisitAsync(visit);

            return visit;
        }

        public async Task SignOutVisitorAsync(SignOutVisitorDTO visitorDto)
        {
            // Validate the input 
            var validator = new SignOutVisitorValidator();
            var validationResult = await validator.ValidateAsync(visitorDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidationException(errors);
            }

            // Find the visitor by email
            var visitor = await _visitorRepository.GetVisitorByEmailAsync(visitorDto.Email);
            if (visitor == null)
            {
                throw new VisitorNotSignedInException("Visitor is not signed in.");
            }

            // Check for an active visit
            var activeVisit = await _visitorRepository.GetActiveVisitByVisitorAsync(visitor.Id);
            if (activeVisit == null)
            {
                throw new VisitorNotSignedInException("Visitor does not have an active visit.");
            }

            // Update the visit's EndTime and status
            activeVisit.EndTime = DateTime.Now;
            activeVisit.CurrentStatus = Action.SignOut;

            // Calculate time spent
            if (activeVisit.StartTime == null || activeVisit.EndTime == null)
            {
                throw new Exception("Invalid visit times for calculating time spent.");
            }
            var timeSpent = activeVisit.EndTime.Value - activeVisit.StartTime;

            // Log the visit
            var visitorLog = new VisitorLog
            {
                Visit = activeVisit,
                Actions = Action.SignOut,
                TimeSpent = timeSpent
            };

            // Save the updates and log
            await _visitorRepository.UpdateVisitAsync(activeVisit);
            await _visitorRepository.CreateVisitorLogAsync(visitorLog);
        }
    }

}
