using System.Windows;
using WeatherApp.Interfaces;
using WeatherApp.ViewModels;

namespace WeatherApp.Commands
{
    /// <summary>
    /// Get weather forecast results.
    /// </summary>
    public class GetWeatherForecast : CommandBase
    {
        private readonly WeatherViewModel weatherViewModel;
        private readonly IWeatherServices weatherServices;
        private readonly ILoggerService logger;

        /// <summary>
        /// Constructor of the GetWeatherForecast.
        /// </summary>
        /// <param name="weatherViewModel">The viewmodel owns this command.</param>
        /// <param name="weatherServices">The weather service gets the forecast results.</param>
        /// <param name="logger">The logger.</param>
        public GetWeatherForecast(WeatherViewModel weatherViewModel, IWeatherServices weatherServices, ILoggerService logger)
        {
            this.weatherViewModel = weatherViewModel;
            this.weatherServices = weatherServices;
            this.logger = logger;
        }

        /// <summary>
        /// Get the weather forecast results.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override async void Execute(object? parameter)
        {
            var lat = this.weatherViewModel.Coord.Lat;
            var lon = this.weatherViewModel.Coord.Lon;

            this.logger.LogInfo($"Fetching the weather forecast for {this.weatherViewModel.Area} ({lat}, {lon}).");
            var forecastResult = await weatherServices.GetWeatherForecastAsync(lat, lon);

            if (forecastResult != null)
            {
                this.weatherViewModel.UpdateWeatherForecastResult(forecastResult);
            }
            else
            {
                this.logger.LogError($"fetching weather forecast for {this.weatherViewModel.Area} ({lat}, {lon}) failed.");
                MessageBox.Show($"fetching weather forecast for {this.weatherViewModel.Area} failed..");
                return;
            }
        }
    }
}
