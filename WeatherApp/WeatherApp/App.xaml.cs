using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WeatherApp.Commands;
using WeatherApp.Interfaces;
using WeatherApp.Models;
using WeatherApp.Repository;
using WeatherApp.Services;
using WeatherApp.Stores;
using WeatherApp.ViewModels;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            // Get the service collection from dependency injection library.
            var serviceCollection = new ServiceCollection();

            SetUpApiKey(serviceCollection);

            // Configuration DB connection info.
            ConfigurationDbContext(serviceCollection);

            // Register the services into dependency injection container.
            ConfigureServices(serviceCollection);

            serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            var navigationService = serviceProvider.GetRequiredService<NavigationService<WeatherViewModel, FavouriteCard>>();
            navigationService.Navigate(new FavouriteCard());
            
            mainWindow.DataContext = serviceProvider.GetRequiredService<MainWindowViewModel>();
            mainWindow.Show();
        }

        private void SetUpApiKey(ServiceCollection serviceCollection)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddUserSecrets<App>();

            var configRoot = builder.Build();
            serviceCollection.AddOptions().Configure<OpenWeatherApiKey>(apiKey => configRoot.Bind(apiKey));
        }

        private void ConfigurationDbContext(ServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<WeatherAppDbContext>(options =>
                options.UseSqlServer("Server=.;Database=WeatherAppDB;Integrated Security=true;TrustServerCertificate=True"));

            serviceCollection.AddScoped<IFavouriteRepository, FavouriteRepository>();
        }

        private void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IWeatherServices, WeatherService>();
            serviceCollection.AddSingleton<ILoggerService, LoggerService>();
            serviceCollection.AddSingleton<NavigationStore>();

            ConfigurationNavigationService(serviceCollection);
            ConfigurationNavigationCommand(serviceCollection);

            serviceCollection.AddSingleton<FavouriteViewModel>(sp =>
            {
                var store = sp.GetRequiredService<NavigationStore>();
                var favouriteRepository = sp.GetRequiredService<IFavouriteRepository>();
                return new FavouriteViewModel(store, favouriteRepository)
                {
                    NavigateHomeWithLocationCommand = sp.GetRequiredService<NavigateCommand<WeatherViewModel, FavouriteCard>>(),
                    NavigateHomeCommand = sp.GetRequiredService<NavigateCommand<WeatherViewModel, object>>()
                };
            });

            serviceCollection.AddSingleton<MainWindowViewModel>();            
            serviceCollection.AddSingleton<MainWindow>();
        }

        private void ConfigurationNavigationService(ServiceCollection serviceCollection)
        {
            // Register NavigationService for navigate to FavouriteViewModel without parameter.
            serviceCollection.AddSingleton<NavigationService<FavouriteViewModel, object>>(sp =>
            {
                var store = sp.GetRequiredService<NavigationStore>();
                return new NavigationService<FavouriteViewModel, object>(
                    store,

                    // This is lambda, it will defer the required service, can be registered later.
                    _ => sp.GetRequiredService<FavouriteViewModel>()
                );
            });

            // Register NavigationService for navigate to WeatherViewModel with FavouriteCard as parameter
            // click individual card will navigate to the weatherViewModel with location info to get weather result directly.
            serviceCollection.AddSingleton<NavigationService<WeatherViewModel, FavouriteCard>>(sp =>
            {
                var store = sp.GetRequiredService<NavigationStore>();
                var weatherServices = sp.GetRequiredService<IWeatherServices>();
                var logger = sp.GetRequiredService<ILoggerService>();
                var favouriteRepository = sp.GetRequiredService<IFavouriteRepository>();

                return new NavigationService<WeatherViewModel, FavouriteCard>(
                    store,
                    favouriteCard =>
                    {
                        // Create a new WeatherViewModel for this NavigationService.
                        var vm = new WeatherViewModel(weatherServices, logger, favouriteCard, favouriteRepository);

                        // Then inject the command for navigate to FavouriteViewModel without param after creation
                        vm.NavigateFavouriteCommand = sp.GetRequiredService<NavigateCommand<FavouriteViewModel, object>>();
                        return vm;
                    });
            });

            // Register NavigationService for navigate to WeatherViewModel without params, used for FavouriteView Home button.
            serviceCollection.AddSingleton<NavigationService<WeatherViewModel, object>>(sp =>
            {
                var store = sp.GetRequiredService<NavigationStore>();
                var weatherServices = sp.GetRequiredService<IWeatherServices>();
                var logger = sp.GetRequiredService<ILoggerService>();
                var favouriteRepository = sp.GetRequiredService<IFavouriteRepository>();

                return new NavigationService<WeatherViewModel, object>(
                    store,
                    _ =>
                    {
                        // Create a new WeatherViewModel for this NavigationService.
                        var vm = new WeatherViewModel(weatherServices, logger, default, favouriteRepository);

                        // Inject command for navigate to FavouriteViewModel without parameter.
                        vm.NavigateFavouriteCommand = sp.GetRequiredService<NavigateCommand<FavouriteViewModel, object>>();
                        return vm;
                    });
            });
        }

        private void ConfigurationNavigationCommand(ServiceCollection serviceCollection)
        {
            // Register command for navigating to WeatherViewModel with location info parameter. 
            serviceCollection.AddSingleton<NavigateCommand<WeatherViewModel, FavouriteCard>>(sp =>
            {
                var navigationService = sp.GetRequiredService<NavigationService<WeatherViewModel, FavouriteCard>>();
                return new NavigateCommand<WeatherViewModel, FavouriteCard>(navigationService);
            });

            // Register command for navigating to FavouriteViewModle without param, click WeatherView Heart Icon.
            serviceCollection.AddSingleton<NavigateCommand<FavouriteViewModel, object>>(sp =>
            {
                var navigationService = sp.GetRequiredService<NavigationService<FavouriteViewModel, object>>();
                return new NavigateCommand<FavouriteViewModel, object>(navigationService);
            });

            // Register command for navigating to WeatherViewModel without param, FavouriteView Home Icon.
            serviceCollection.AddSingleton<NavigateCommand<WeatherViewModel, object>>(sp =>
            {
                var navigationService = sp.GetRequiredService<NavigationService<WeatherViewModel, object>>();
                return new NavigateCommand<WeatherViewModel, object>(navigationService);
            });
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            // Dispose the service provider.
            if (serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }

}
