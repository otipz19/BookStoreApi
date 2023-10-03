namespace Domain.Dto
{
    public record GenreDto
    {
        public int Id { get; init; }
        public string Name { get; init; }

        public GenreDto(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}