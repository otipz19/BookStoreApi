namespace Domain.Exceptions
{
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException(string entityName, int id)
            : base($"{entityName} with ID {id} was not found.")
        {
        }
    }
}