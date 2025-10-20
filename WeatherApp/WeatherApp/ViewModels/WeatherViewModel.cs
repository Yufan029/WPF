using System.Collections.ObjectModel;
using System.Windows.Input;
using WeatherApp.Commands;
using WeatherApp.Interfaces;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    /// <summary>
    /// Weather view model class.
    /// </summary>
    public partial class WeatherViewModel : ViewModelBase
    {
        private string location;
        private string area;
        private string temperature;
        private string description;
        private string iconUrl;
        private string humidity;
        private string windSpeed;
        private string selectedTimeFilter;
        private readonly ILoggerService logger;

        /// <summary>
        /// Command for getting today's weather report.
        /// </summary>
        public ICommand GetWeatherCommand { get; }

        /// <summary>
        /// Command for getting forecast weather report.
        /// </summary>
        public ICommand GetWeatherForecast { get; }

        /// <summary>
        /// Command for navigate to favourite page.
        /// </summary>
        //public ICommand NavigateFavouriteCommand { get; set; }

        public NavigateCommand<FavouriteViewModel, object> NavigateFavouriteCommand { get; set; }

        /// <summary>
        /// All the forecast results.
        /// </summary>
        public ObservableCollection<WeatherCard> WeatherCards { get; set; } = new();

        /// <summary>
        /// Filtered forecast weather results.
        /// </summary>
        public ObservableCollection<WeatherCard> FilteredWeatherCards { get; } = new();

        /// <summary>
        /// The combo box select options, binding to UI drop down menu.
        /// </summary>
        public ObservableCollection<string> TimeOptions { get; } = new();

        /// <summary>
        /// Control the visibility of the drop down menu.
        /// </summary>
        public bool IsFilterVisible => WeatherCards.Any();

        /// <summary>
        /// The location binding to the text input.
        /// </summary>
        public string Location
        {
            get => location;
            set => this.SetProperty(ref location, value);
        }

        /// <summary>
        /// The temperature of the weather report.
        /// </summary>
        public string Temperature
        {
            get => temperature;
            set => this.SetProperty(ref temperature, value);
        }

        /// <summary>
        /// The description of the weather report.
        /// </summary>
        public string Description
        {
            get => description;
            set => this.SetProperty(ref description, value);
        }

        /// <summary>
        /// The icon url for the weather report.
        /// </summary>
        public string IconUrl
        {
            get => iconUrl;
            set => this.SetProperty(ref iconUrl, value);
        }

        /// <summary>
        /// The humidity of the weather report.
        /// </summary>
        public string Humidity
        {
            get => humidity;
            set => this.SetProperty(ref humidity, value);
        }

        /// <summary>
        /// The wind speed of the weather report.
        /// </summary>
        public string WindSpeed
        {
            get => windSpeed;
            set => this.SetProperty(ref windSpeed, value);
        }

        /// <summary>
        /// The area name of the weather report.
        /// </summary>
        public string Area
        {
            get => area;
            set => this.SetProperty(ref area, value);
        }

        /// <summary>
        /// The coordinators of the weather report location.
        /// </summary>
        public Coord Coord { get; set; }

        /// <summary>
        /// The selected filter of the combo box.
        /// </summary>
        public string SelectedTimeFilter
        {
            get => selectedTimeFilter;
            set
            {
                if (selectedTimeFilter != value)
                {
                    selectedTimeFilter = value;
                    OnPropertyChanged();
                    ApplyFilter();
                }
            }
        }

        /// <summary>
        /// Constructor for WeatherViewModel.
        /// </summary>
        /// <param name="weatherServices">The WeatherServices.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="favouriteCard">The favourite card passed from favourite viewmodel.</param>
        public WeatherViewModel(
            IWeatherServices weatherServices, 
            ILoggerService logger, 
            FavouriteCard favouriteCard)
        {
            GetWeatherCommand = new GetWeatherCommand(this, weatherServices, logger);
            GetWeatherForecast = new GetWeatherForecast(this, weatherServices, logger);

            this.logger = logger;

            if (favouriteCard is not null && favouriteCard.Coord is not null)
            {
                Location = $"{favouriteCard.Coord.Lat}, {favouriteCard.Coord.Lon}";
                this.GetWeatherCommand.Execute(null);
            }
        }

        /// <summary>
        /// Update the weather results for client to show.
        /// </summary>
        /// <param name="weather">The weather results.</param>
        public void UpdateWeatherResult(WeatherResponse weather)
        {
            this.ClearCollection();

            Area = $"{weather.Name}";
            Temperature = $"{weather.WeatherDetails.Temp - 273.15:F1} °C";
            Description = $"{weather.Weather[0].Description}";
            Humidity = $"{weather.WeatherDetails.Humidity}";
            WindSpeed = $"{weather.Wind.Speed}m/s";
            Coord = weather.Coord;
            IconUrl = $"https://openweathermap.org/img/wn/{weather.Weather[0].Icon}@2x.png";

            this.logger.LogInfo($"Get {Area} weather info success.");
            this.logger.LogInfo($"Temperature is {Temperature}.");
        }

        /// <summary>
        /// Update the weather forecast results for UI.
        /// </summary>
        /// <param name="forecastResult">The weather forecast results.</param>
        public void UpdateWeatherForecastResult(WeatherForecastResponse forecastResult)
        {
            this.ClearCollection();
            var timeOptions = new HashSet<string>();

            foreach (var weatherInfo in forecastResult.WeatherForecasts)
            {
                var newCard = new WeatherCard
                {
                    UtcTime = weatherInfo.UtcDatetime,
                    TimeText = weatherInfo.TimeText,
                    Temperature = $"{weatherInfo.WeatherDetails.Temp - 273.15:F1} °C",
                    Description = weatherInfo.Weather[0].Description,
                    IconUrl = $"https://openweathermap.org/img/wn/{weatherInfo.Weather[0].Icon}@2x.png",
                };

                var time = DateTimeOffset.FromUnixTimeSeconds(weatherInfo.UtcDatetime);
                var day = time.ToLocalTime().ToString("yyyy-MM-dd");
                timeOptions.Add(day);
                this.WeatherCards.Add(newCard);
            }

            this.logger.LogInfo($"Get weather forecast from {timeOptions.FirstOrDefault()} to {timeOptions.LastOrDefault()}.");
            this.UpdateTimeOption(timeOptions);
        }

        /// <summary>
        /// Update the combo box select options.
        /// </summary>
        /// <param name="timeOptions">The select options.</param>
        public void UpdateTimeOption(IEnumerable<string> timeOptions)
        {
            this.TimeOptions.Add("All");
            foreach (var time in timeOptions)
            {
                this.TimeOptions.Add(time);
            }

            SelectedTimeFilter = this.TimeOptions.FirstOrDefault() ?? "All";
            OnPropertyChanged(nameof(IsFilterVisible));
        }

        /// <summary>
        /// Apply filter after selected option changed.
        /// </summary>
        public void ApplyFilter()
        {
            this.FilteredWeatherCards.Clear();
            if (SelectedTimeFilter == "All" || string.IsNullOrEmpty(SelectedTimeFilter))
            {
                foreach (var card in WeatherCards)
                {
                    FilteredWeatherCards.Add(card);
                }
            }
            else
            {
                foreach (var cards in WeatherCards.Where(x => x.TimeText.Contains(this.SelectedTimeFilter)).ToArray())
                {
                    FilteredWeatherCards.Add(cards);
                }
            }
        }

        private void ClearCollection()
        {
            this.WeatherCards.Clear();
            this.TimeOptions.Clear();
            OnPropertyChanged(nameof(IsFilterVisible));
        }
    }
}
