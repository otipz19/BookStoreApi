using System.ComponentModel.DataAnnotations;

public class Genre
{
    public int Id { get; set; }

    [MaxLength(500)] 
    public string Name { get; set; } = default!;

    public ICollection<Book> Books { get; set; } = new List<Book>();
}