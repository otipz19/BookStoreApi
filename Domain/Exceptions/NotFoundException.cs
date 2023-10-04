namespace Domain.Exceptions
{
    public class NotFoundException<T> : System.Exception
    {
        public NotFoundException(int id)
            : base($"{typeof(T).Name} with ID {id} was not found.")
        {
        }
    }
}