using System;

namespace API.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string message) : base(message) {}
    }
}
