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
    public class PosterViewModel
    {
        private readonly IMovieRepository _movieRepository;  // Данное поле нужно, чтобы вьюмодел работала с данными не зная откуда они берутся.
                                                             // Соответствует принципу внедрения зависимосимостей (Dependency Injection)
                                                             // и призципу разделения ответственности (Separaion of Concerns

        public ObservableCollection<Movie> Movies { get; set; }
        public ICommand LoadMovieCommand { get;}

        public PosterViewModel(IMovieRepository repository)
        {
            _movieRepository = repository;
            Movies = new ObservableCollection<Movie>();
            LoadMovieCommand = new RelayCommand(async () => await LoadMoviesAsync());  //Мы связали команду и метод, который она будет выполнять,
                                                                                        //это сделано в конструкторе, чтобы она свойство не могло выполнять
                                                                                        //другие методы, через конструктор так же можно будет использовать DI
        }

        public async Task LoadMoviesAsync()
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
