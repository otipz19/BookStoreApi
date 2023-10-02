namespace Domain.Exceptions
{
    public class AuthorNotFoundException : NotFoundException
    {
        public AuthorNotFoundException(int id)
            : base("Author", id)
        {
        }
    }
}