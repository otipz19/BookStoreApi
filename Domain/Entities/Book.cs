using System.ComponentModel.DataAnnotations;

public class Book
{
    public int Id { get; set; }

    [MaxLength(500)]
    public string Title { get; set; } = default!;

    [Range(0, int.MaxValue)]
    public double Price { get; set; }

    public int Quantity { get; set; }

    public int AuthorId { get; set; }
    public Author? Author { get; set; }

    public int GenreId { get; set; }
    public Genre? Genre { get; set; }
}