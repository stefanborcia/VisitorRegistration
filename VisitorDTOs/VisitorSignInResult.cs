

namespace VisitorDTOs
{
    public class VisitorSignInResult
    {
        public bool IsSuccess { get; }
        public string ErrorMessage { get; }

        private VisitorSignInResult(bool isSuccess, string errorMessage = null)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static VisitorSignInResult Success() => new VisitorSignInResult(true);
        public static VisitorSignInResult Fail(string errorMessage) => new VisitorSignInResult(false, errorMessage);
    }
}
