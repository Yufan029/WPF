using System.Windows;
using WeatherApp.Interfaces;
using WeatherApp.ViewModels;

namespace WeatherApp.Commands
{
    /// <summary>
    /// Get weather report command.
    /// </summary>
    public class GetWeatherCommand : CommandBase
    {
        private readonly WeatherViewModel weatherViewModel;
        private readonly IWeatherServices weatherServices;
        private readonly ILoggerService logger;

        /// <summary>
        /// Constructor of the GetWeatherCommand.
        /// </summary>
        /// <param name="weatherViewModel">The viewmodel which own this command.</param>
        /// <param name="weatherServices">The weather services to get the results.</param>
        /// <param name="logger">The logger.</param>
        public GetWeatherCommand(WeatherViewModel weatherViewModel, IWeatherServices weatherServices, ILoggerService logger)
        {
            this.weatherViewModel = weatherViewModel;
            this.weatherServices = weatherServices;
            this.logger = logger;
            this.weatherViewModel.PropertyChanged += WeatherViewModel_PropertyChanged;
        }

        /// <summary>
        /// Check if the command can be clicked,  disable the button if there's no input location.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>True for enable, otherwise disable.</returns>
        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter)
                && this.weatherViewModel.Location != null
                && !string.IsNullOrEmpty(this.weatherViewModel.Location.Trim());
        }

        /// <summary>
        /// Check if the view model property changed, if the property name is location then raise OnCanExecuteChanged.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The property args.</param>
        private void WeatherViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.weatherViewModel.Location))
            {
                OnCanExecuteChanged();
            }
        }

        /// <summary>
        /// Get the weather results.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override async void Execute(object? parameter)
        {
            var location = weatherViewModel.Location.Trim();
            if (string.IsNullOrEmpty(location))
            {
                this.logger.LogError("Location cannot be empty");
                MessageBox.Show("Please enter a location.");
                return;
            }

            try
            {
                this.logger.LogInfo("Fetching weather...");
                var weather = await weatherServices.GetWeatherAsync(location);
                if (weather != null)
                {
                    this.weatherViewModel.UpdateWeatherResult(weather);
                }
                else
                {
                    this.logger.LogError("Location not found.");
                    MessageBox.Show("Location not found.");
                    return;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Exception happened: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
