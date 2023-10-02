namespace Domain.Exceptions
{
    public class GenreNotFoundException : NotFoundException
    {
        public GenreNotFoundException(int id)
            : base("Genre", id)
        {
        }
    }
}