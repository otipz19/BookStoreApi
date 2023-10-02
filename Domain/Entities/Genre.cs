using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Genre : BaseEntity
{
    [MaxLength(500)] 
    public string Name { get; set; } = default!;

    public ICollection<Book> Books { get; set; } = new List<Book>();
}