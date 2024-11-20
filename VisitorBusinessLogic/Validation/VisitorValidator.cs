using FluentValidation;
using VisitorDTOs.VisitorDTO;

namespace VisitorBusinessLogic.Validation
{
    public class VisitorValidator : AbstractValidator<SignInVisitorDTO>
    {
        public VisitorValidator()
        {
            RuleFor(visitor => visitor.Name)
                .NotEmpty().WithMessage("The name is required.")
                .MinimumLength(3).WithMessage("The name must contain at least 3 characters.");

            RuleFor(visitor => visitor.Email)
                .NotEmpty().WithMessage("The email is required.")
                .EmailAddress().WithMessage("You must enter a valid email.");

            RuleFor(visitor => visitor.VisitingCompanyId)
                .GreaterThan(0).WithMessage("A valid visiting company is required.");

            RuleFor(visitor => visitor.AppointmentWithId)
                .GreaterThan(0).WithMessage("A valid appointment with an employee is required.");
        }
    }
}
