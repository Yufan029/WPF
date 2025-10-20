using NSubstitute;
using System.Collections.ObjectModel;
using System.Windows.Navigation;
using WeatherApp.Interfaces;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.Stores;
using WeatherApp.ViewModels;

namespace WeatherAppTests.ViewModelsTests
{
    public class WeatherViewModelTests
    {
        private IWeatherServices weatherServices;
        private ILoggerService logger;

        [SetUp]
        public void Setup()
        {
            this.weatherServices = Substitute.For<IWeatherServices>();
            this.logger = Substitute.For<ILoggerService>();
        }

        /// <summary>
        /// Test ApplyFilter, filter out items.
        /// </summary>
        [Test]
        public void ApplyFilter_FiltersCorrectly()
        {
            // Arrange
            var vm = new WeatherViewModel(this.weatherServices, this.logger, new FavouriteCard());
            vm.WeatherCards = new ObservableCollection<WeatherCard>
            {
                new WeatherCard { TimeText = "Morning" },
                new WeatherCard { TimeText = "Afternoon" },
                new WeatherCard { TimeText = "Morning" },
            };

            vm.SelectedTimeFilter = "Morning";

            // Act
            vm.ApplyFilter();

            // Assert
            Assert.AreEqual(2, vm.FilteredWeatherCards.Count);
            Assert.IsTrue(vm.FilteredWeatherCards.All(c => c.TimeText == "Morning"));
        }

        /// <summary>
        /// Test ApplyFilter with All option.
        /// </summary>
        [Test]
        public void ApplyFilter_All_ShowsAllCards()
        {
            var vm = new WeatherViewModel(this.weatherServices, this.logger, new FavouriteCard());
            vm.WeatherCards = new ObservableCollection<WeatherCard>
            {
                new WeatherCard { TimeText = "Morning" },
                new WeatherCard { TimeText = "Afternoon" },
            };

            vm.SelectedTimeFilter = "All";

            vm.ApplyFilter();

            Assert.That(vm.FilteredWeatherCards.Count, Is.EqualTo(2));
        }
    }
}
