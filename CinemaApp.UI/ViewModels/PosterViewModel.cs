using CinemaApp.Core.Interfaces;
using CinemaApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.UI.ViewModels
{
    public class PosterViewModel
    {
        private readonly IMovieRepository _movieRepository;  // Данное поле нужно, чтобы вьюмодел работала с данными не зная откуда они берутся.
                                                             // Соответствует принципу внедрения зависимосимостей (Dependency Injection)
                                                             // и призципу разделения ответственности (Separaion of Concerns

        ObservableCollection<Movie> Movies { get; set; }

        public PosterViewModel(IMovieRepository repository)
        {
            _movieRepository = repository;
            Movies = new ObservableCollection<Movie>();
        }

        public async Task LoadMoviesAsnync()
        {
            var movies = await _movieRepository.GetAllAsync();
            Movies.Clear();
            foreach (var movie in movies)
            {
                Movies.Add(movie);
            } 
        }

    }
}
