using System;

namespace Funda.Core
{
    public class RequestLimitExceededException : Exception
    {
        public RequestLimitExceededException(string message) 
            : base(message)
        {
        }
    }
}
