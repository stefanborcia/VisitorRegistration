using System.Text.RegularExpressions;
using VisitorDTOs;

namespace VisitorBusinessLogic.Validation
{
    public class VisitorValidator
    {
        public static VisitorSignInResult ValidateName(string name) =>
            !string.IsNullOrWhiteSpace(name) && name.Length >= 3
            ? VisitorSignInResult.Success()
            : VisitorSignInResult.Fail("Name must be at least 3 characters.");

        public static VisitorSignInResult ValidateEmail(string email) =>
            !string.IsNullOrWhiteSpace(email) && IsValidEmail(email)
            ? VisitorSignInResult.Success()
            : VisitorSignInResult.Fail("Invalid email format.");

        public static VisitorSignInResult ValidateVisitingCompanyId(long companyId) =>
            companyId > 0 ? VisitorSignInResult.Success() : VisitorSignInResult.Fail("A valid visiting company is required.");

        public static VisitorSignInResult ValidateAppointmentWithId(long appointmentId) =>
            appointmentId > 0 ? VisitorSignInResult.Success() : VisitorSignInResult.Fail("A valid appointment with an employee is required.");

        private static bool IsValidEmail(string email) =>
            Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}
