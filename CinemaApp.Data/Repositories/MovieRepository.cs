using CinemaApp.Core.Interfaces;
using CinemaApp.Core.Models;

namespace CinemaApp.Data.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly List<Movie> _movies;

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await Task.FromResult(_movies);
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await Task.FromResult(_movies.FirstOrDefault(m => m.Id == id));
        }

        public async Task AddAsync(Movie movie)
        {
            _movies.Add(movie);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Movie movie)
        {
            var updatedMovie = _movies.FirstOrDefault(m => m.Id == movie.Id);
            if (updatedMovie != null)
            {
                updatedMovie.Tittle = movie.Tittle;
                updatedMovie.Description = movie.Description;
                updatedMovie.Duration = movie.Duration;
                updatedMovie.PosterUrl = movie.PosterUrl;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var movie = _movies.FirstOrDefault(m => m.Id == id);
            if (movie != null)
            {
                _movies.Remove(movie);
            }
            await Task.CompletedTask;
        }
    }
}
