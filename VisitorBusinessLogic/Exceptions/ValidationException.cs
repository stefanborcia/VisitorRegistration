

namespace VisitorBusinessLogic.Exceptions
{
    public class ValidationException : Exception
    {
        public List<string> ValidationErrors { get; }

        public ValidationException(List<string> validationErrors)
        {
            ValidationErrors = validationErrors;
        }
    }
}
