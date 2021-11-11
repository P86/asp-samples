using System;

namespace HandlingFailures.Exceptions
{
    public class InvalidDataException: Exception
    {
        protected InvalidDataException(string message) : base(message) { }
    }
}
