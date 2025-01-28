namespace MovieAPI.Models
{
    public class Movie
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ReleaseDate { get; set; }
    }
}
