namespace CoolBrains.Infrastructure.Commands
{
    public class ValidationFailure
    {
        public ValidationFailure()
        {

        }

        public ValidationFailure(string errorCode, string errorMessage)
        {
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;

        }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string PropertyName { get; set; }
        public string ResourceName { get; set; }
    }
}
