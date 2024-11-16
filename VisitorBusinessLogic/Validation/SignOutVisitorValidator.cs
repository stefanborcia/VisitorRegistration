using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
