using CinemaApp.Core.Interfaces;
using CinemaApp.Core.Models;

namespace CinemaApp.Data.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly List<Movie> _movies;


        public MovieRepository() //Тестовые данные
        {
            _movies = new List<Movie>()
            {
                new Movie {Id=1,Tittle="65 миллионов лет назад",Description="Что-то",Duration = new TimeSpan(2,49,0),PosterUrl="/Images/Posters/1.jpg"},
                new Movie {Id=2,Tittle="Микро женщина",Description="Что-то",Duration = new TimeSpan(2,20,0),PosterUrl="/Images/Posters/2.jpg"},
                new Movie {Id=3,Tittle="Цветной спайс",Description="Что-то",Duration = new TimeSpan(2,39,0),PosterUrl="/Images/Posters/3.jpg"},
                new Movie {Id=4,Tittle="Какой то фильм",Description="Что-то",Duration = new TimeSpan(2,29,0),PosterUrl="/Images/Posters/4.jpg"},
                new Movie {Id=5,Tittle="Звездные вояки",Description="Что-то",Duration = new TimeSpan(3,30,0),PosterUrl="/Images/Posters/5.jpg"},
            };
        }


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
