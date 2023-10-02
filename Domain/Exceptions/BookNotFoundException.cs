namespace Domain.Exceptions
{
    public class BookNotFoundException : NotFoundException
    {
        public BookNotFoundException(int id)
            : base("Book", id)
        {
        }
    }
}