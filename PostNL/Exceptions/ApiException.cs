using System;

namespace PostNLApi.Exceptions
{
    public class ApiException : Exception
    {
        protected ApiException()
        {
        }

        public ApiException(string message) : base(message)
        {
        }
    }
}