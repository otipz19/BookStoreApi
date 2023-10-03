namespace Domain.Dto
{
    public record AuthorDto
    {
        public int Id { get; init; }
        public string Name { get; init; }

        public AuthorDto(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}