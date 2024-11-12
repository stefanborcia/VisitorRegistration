using System.Text.RegularExpressions;
using VisitorDTOs;

namespace VisitorBusinessLogic.Validation
{
    public class VisitorValidator
    {
        public static VisitorSignInResult ValidateName(string name) =>
            string.IsNullOrWhiteSpace(name)
                ? VisitorSignInResult.Fail("The name is required.") 
                : name.Length >= 3
                    ? VisitorSignInResult.Success() 
                    : VisitorSignInResult.Fail("The name must contain at least 3 characters.");

        public static VisitorSignInResult ValidateEmail(string email) =>
            string.IsNullOrWhiteSpace(email)
                ? VisitorSignInResult.Fail("The email is required.") 
                : IsValidEmail(email)
                    ? VisitorSignInResult.Success() 
                    : VisitorSignInResult.Fail("You must enter a valid email.");

        public static VisitorSignInResult ValidateVisitingCompanyId(long companyId) =>
            companyId > 0 ? VisitorSignInResult.Success() : VisitorSignInResult.Fail("A valid visiting company is required.");

        public static VisitorSignInResult ValidateAppointmentWithId(long appointmentId) =>
            appointmentId > 0 ? VisitorSignInResult.Success() : VisitorSignInResult.Fail("A valid appointment with an employee is required.");

        private static bool IsValidEmail(string email) =>
            Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}
