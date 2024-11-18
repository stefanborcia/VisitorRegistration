using FluentValidation;
using VisitorDTOs;

namespace VisitorBusinessLogic.Validation
{
    public class SignOutVisitorValidator : AbstractValidator<SignOutVisitorDTO>
    {
        public SignOutVisitorValidator()
        {
            RuleFor(visitor => visitor.Email)
                .NotEmpty().WithMessage("Email is required for sign out.")
                .EmailAddress().WithMessage("A valid email address is required.");
        }
    }
}
