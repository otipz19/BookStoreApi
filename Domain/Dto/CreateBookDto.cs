namespace Domain.Dto
{
    public record CreateBookDto
    {
        public string Title { get; init; }
        public double Price { get; init; }
        public int Quantity { get; init; }
        public int AuthorId { get; init; }
        public int GenreId { get; init; }

        public CreateBookDto(string title, double price, int quantity, int authorId, int genreId)
        {
            Title = title;
            Price = price;
            Quantity = quantity;
            AuthorId = authorId;
            GenreId = genreId;
        }
    }
}