using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoolBrains.Infrastructure.Commands
{
    public class ValidationResult
    {
        public List<ValidationFailure> Errors { get; set; }

        public ValidationResult()
        {
            Errors = new List<ValidationFailure>();
        }

        public ValidationResult(List<ValidationFailure> errors)
        {
            Errors = errors;
        }

        public void AddError(string errorMessage, string propertyName = "", string errorCode = "", string resourceName = "")
        {
            Errors.Add(new ValidationFailure { ErrorMessage = errorMessage, PropertyName = propertyName, ErrorCode = errorCode, ResourceName = resourceName });
        }

        public override string ToString()
        {
            var errorMessages = new StringBuilder();
            foreach (var error in Errors)
            {
                if (!string.IsNullOrEmpty(errorMessages.ToString()))
                    errorMessages.Append(",\n");

                errorMessages.Append(error.ErrorMessage);
            }

            return errorMessages.ToString();
        }


        public bool IsValid => !Errors.Any();
    }
}
