using System;

namespace Studio36.ModelComponent
{
    public class InvalidLoginDataException : Exception
    {
        public InvalidLoginDataException()
        {
        }

        public InvalidLoginDataException(string message) : base(message)
        {
        }

        public InvalidLoginDataException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}