namespace HandlingFailures.Exceptions
{
    public class InvalidNumberOfDaysException : InvalidDataException
    {
        public InvalidNumberOfDaysException(int days) : base($"Waether cannot be predicted for {days} days") { }
    }
}
