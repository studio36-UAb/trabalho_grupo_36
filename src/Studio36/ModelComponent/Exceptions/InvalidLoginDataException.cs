using System;

namespace Studio36.ModelComponent
{
    public class InvalidLoginInputException : Exception
    {
        public InvalidLoginInputException()
        {
        }

        public InvalidLoginInputException(string message) : base(message)
        {
        }

        public InvalidLoginInputException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}