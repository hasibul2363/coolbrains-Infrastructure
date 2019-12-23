using FluentValidation.Results;

namespace CoolBrains.Infrastructure.Validation.FluentValidator
{
    public static class FluentValidationExtensions
    {
        public static Commands.ValidationResult ToMiddlewareValidationResult(this ValidationResult validationResult)
        {
            var suitValidationResult = new Commands.ValidationResult();
            if (validationResult == null)
                return suitValidationResult;

            foreach (var validationFailure in validationResult.Errors)
                suitValidationResult.AddError(validationFailure.ErrorMessage, validationFailure.PropertyName, validationFailure.ErrorCode, validationFailure.ResourceName);

            return suitValidationResult;
        }
    }
}
