using CinemaApp.Core.Interfaces;
using CinemaApp.Data.Repositories;
using CinemaApp.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace CinemaApp.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Настраиваем контейнер зависимостей
            var services = new ServiceCollection();
            services.AddSingleton<IMovieRepository, MovieRepository>(); //Регистрирует муви реп как синглтон интерфейса муви реп, это гарантирует,
                                                                        //что один и тот же экземпляр муви реп будет использоваться в приложении

            services.AddTransient<MoviesViewModel>();                   //Временная зависимость (каждый раз будет создаваться новый viewmodel

            ServiceProvider = services.BuildServiceProvider();

            // Создаем главное окно с внедрением зависимостей
            var mainWindow = new MainWindow
            {
                DataContext = ServiceProvider.GetRequiredService<MoviesViewModel>()   //контейнер создает PosterViewModel и
                                                                                      //автоматически внедряет IMovieRepository в конструктор
            };
            mainWindow.Show();

            //Внедрение зависимостей легко позволит нам заменить муви реп на другой репозиторий, например для работы с базой данных
        }

        protected override void OnExit(ExitEventArgs e)
        {
            ServiceProvider.Dispose(); //Освобождаем ресурсы контейнера
            base.OnExit(e); 
        }




    }

}
