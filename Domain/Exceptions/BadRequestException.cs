namespace Domain.Exceptions
{
    public class BadRequestException<T> : ApplicationException
    {
        public BadRequestException(string message, T details) : base(message)
        {
            Details = details;
        }

        public T Details { get; }
    }
}