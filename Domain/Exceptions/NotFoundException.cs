namespace Domain.Exceptions
{
    public class NotFoundException<T> : Exception
    {
        public NotFoundException(int id)
            : base($"{typeof(T).Name} with ID {id} was not found.")
        {
        }
    }
}