using System;

namespace CoolBrains.Infrastructure.Exceptions
{
    public class QueryException : Exception
    {
        public QueryException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
