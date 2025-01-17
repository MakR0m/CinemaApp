using CinemaApp.Core.Interfaces;
using CinemaApp.Core.Models;
using CinemaApp.UI.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            LoadMovieCommand = new RelayCommand(() => LoadMoviesAsync());  //Мы связали команду и метод, который она будет выполнять,это сделано в конструкторе, чтобы она свойство не могло выполнять другие методы, через конструктор так же можно будет использовать DI
            AddMovieCommand = new RelayCommand(() => AddMovieAsync());
            EditMovieCommand = new RelayCommand(() => EditMovieAsync());
            DeleteMovieCommand = new RelayCommand(() => DeleteMovieAsync());
        }

        private async Task LoadMoviesAsync()
        {
            await ExecuteWithErrorHandling(async () =>
            {
                var movies = await _movieRepository.GetAllAsync();
                Movies.Clear();
                foreach (var movie in movies)
                {
                    Movies.Add(movie);
                }
            });
        }

        private async Task AddMovieAsync()
        {

            await ExecuteWithErrorHandling(async () =>
            {
                var newMovie = new Movie()
                {
                    Id = Movies.Last().Id + 1,
                    PosterUrl = "/Images/Posters/default.jpg",
                    Description = "adasdsad",
                    Duration = new TimeSpan(2, 10, 0)
                };
                await _movieRepository.AddAsync(newMovie);
                Movies.Add(newMovie);
                MessageBox.Show("Успех");
            });

        }

        private async Task EditMovieAsync()
        {

            await ExecuteWithErrorHandling(async () =>
            {
                if (SelectedMovie != null)
                {
                    SelectedMovie.Tittle = "Редактед фильм";
                    await _movieRepository.UpdateAsync(SelectedMovie);
                    MessageBox.Show("Успех");
                }
            });

        }

        private async Task DeleteMovieAsync()
        {
            await ExecuteWithErrorHandling(async () =>
            {
                if (SelectedMovie != null)
                {
                    await _movieRepository.DeleteAsync(SelectedMovie.Id);
                    Movies.Remove(SelectedMovie);
                    SelectedMovie = null;
                    MessageBox.Show("Успех");
                }
            });

        }

        public async Task ExecuteWithErrorHandling (Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex}");
            }
        }

    }
}
