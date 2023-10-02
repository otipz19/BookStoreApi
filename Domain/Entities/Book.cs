using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Book : BaseEntity
{
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