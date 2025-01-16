using CinemaApp.UI.Commands;
using CinemaApp.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CinemaApp.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
		private object _currentView;

		public object CurrentView
		{
			get => _currentView;
			set 
			{ 
				_currentView = value; 
				OnPropertyChanged(nameof(CurrentView));
			}
		}

		public ICommand NavigateToMovieCommand { get; }

		public MainWindowViewModel(IServiceProvider serviceProvider)
        {
            var moviesView = serviceProvider.GetService(typeof(MoviesView));
            NavigateToMovieCommand = new RelayCommand(async () => 
			{
				if (moviesView != null)
				{
					CurrentView = moviesView;
				}
				await Task.CompletedTask;
			});

			CurrentView = moviesView;
        }


    }
}
