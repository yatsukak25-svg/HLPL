namespace HLPL_lab_1.Models
{
    public class MovieInfoViewModel
    {
        public required string Slug { get; init; }
        public required string Title { get; init; }
        public string Subtitle { get; init; } = string.Empty;
        public required string Genre { get; init; }
        public required string Duration { get; init; }
        public required string AgeRating { get; init; }
        public required string Language { get; init; }
        public required string Format { get; init; }
        public required string Country { get; init; }
        public required string Year { get; init; }
        public required string Director { get; init; }
        public required string Cast { get; init; }
        public required string Description { get; init; }
        public required string PosterPath { get; init; }
        public IReadOnlyList<string> Showtimes { get; init; } = [];
    }
}
