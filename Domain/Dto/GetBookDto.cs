namespace Domain.Dto
{
    public record GetBookDto
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public double Price { get; init; }
        public int Quantity { get; init; }
        public int AuthorId { get; init; }
        public string AuthorName { get; init; }
        public int GenreId { get; init; }
        public string GenreName { get; init; }

        public GetBookDto(int id, string title, double price, int quantity, int authorId, string authorName, int genreId, string genreName)
        {
            Id = id;
            Title = title;
            Price = price;
            Quantity = quantity;
            AuthorId = authorId;
            AuthorName = authorName;
            GenreId = genreId;
            GenreName = genreName;
        }
    }
}