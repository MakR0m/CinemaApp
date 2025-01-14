namespace CinemaApp.Core.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public string PosterUrl { get; set; }
    }
}
