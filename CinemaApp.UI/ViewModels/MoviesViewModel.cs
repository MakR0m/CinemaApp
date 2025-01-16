using CinemaApp.Core.Interfaces;
using CinemaApp.Core.Models;
using CinemaApp.UI.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CinemaApp.UI.ViewModels
{
    public class MoviesViewModel
    {
        private readonly IMovieRepository _movieRepository;  // Данное поле нужно, чтобы вьюмодел работала с данными не зная откуда они берутся.
                                                             // Соответствует принципу внедрения зависимосимостей (Dependency Injection)
                                                             // и призципу разделения ответственности (Separaion of Concerns

        public ObservableCollection<Movie> Movies { get; set; }
        public Movie? SelectedMovie { get; set; }
        
        public ICommand LoadMovieCommand { get;}
        public ICommand AddMovieCommand { get; }
        public ICommand EditMovieCommand { get; }
        public ICommand DeleteMovieCommand { get; }





        public MoviesViewModel(IMovieRepository repository)
        {
            _movieRepository = repository;
            Movies = new ObservableCollection<Movie>();
            LoadMovieCommand = new RelayCommand(async () => await LoadMoviesAsync());  //Мы связали команду и метод, который она будет выполнять,это сделано в конструкторе, чтобы она свойство не могло выполнять другие методы, через конструктор так же можно будет использовать DI
            AddMovieCommand = new RelayCommand(async () => await AddMovieAsync());
            EditMovieCommand = new RelayCommand(async () => await EditMovieAsync());
            DeleteMovieCommand = new RelayCommand(async () => await DeleteMovieAsync());
        }

        private async Task LoadMoviesAsync()
        {
            var movies = await _movieRepository.GetAllAsync();
            Movies.Clear();
            foreach (var movie in movies)
            {
                Movies.Add(movie);
            } 
        }

        private async Task AddMovieAsync()
        {
            var newMovie = new Movie()
            {
                Id = Movies.Last().Id + 1,
                PosterUrl = "/Images/Posters/default.jpg",
                Description = "adasdsad",
                Duration = new TimeSpan(2,10,0)
            };
            await _movieRepository.AddAsync(newMovie);
            Movies.Add(newMovie);
        }

        private async Task EditMovieAsync()
        {
            if (SelectedMovie != null)
            {
                SelectedMovie.Tittle = "Редактед фильм";
                await _movieRepository.UpdateAsync(SelectedMovie);
            }
        }

        private async Task DeleteMovieAsync()
        {
            if (SelectedMovie != null)
            {
                await _movieRepository.DeleteAsync(SelectedMovie.Id);
                Movies.Remove(SelectedMovie);
                SelectedMovie = null;
            }
        }

    }
}
