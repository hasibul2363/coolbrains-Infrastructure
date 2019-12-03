using System;

namespace CoolBrains.Infrastructure.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
