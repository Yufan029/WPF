using System.Collections.ObjectModel;
using System.Windows.Input;
using WeatherApp.Interfaces;
using WeatherApp.Models;
using WeatherApp.Stores;

namespace WeatherApp.ViewModels
{
    /// <summary>
    /// Favourite viewmodel to store the favourited places.
    /// </summary>
    public class FavouriteViewModel : ViewModelBase
    {
        private string locationFilter;
        private readonly IFavouriteRepository favouriteRepository;

        public FavouriteCard FavCard { get; set; }

        /// <summary>
        /// Navigation command to go back weather view with location info.
        /// </summary>
        public ICommand NavigateHomeWithLocationCommand { get; set; }

        /// <summary>
        /// Navigate to weather viewmodel without parameter.
        /// </summary>
        public ICommand NavigateHomeCommand { get; set; }

        /// <summary>
        /// Collection for all favourite locations.
        /// </summary>
        public ObservableCollection<FavouriteCard> FavouriteLocations { get; } = new();

        /// <summary>
        /// Collection for filtered locations.
        /// </summary>

        public ObservableCollection<FavouriteCard> FilteredLocations { get; } = new();

        /// <summary>
        /// Location filter.
        /// </summary>
        public string LocationFilter
        {
            get => locationFilter;
            set
            {
                this.SetProperty(ref locationFilter, value);
                this.ApplyFilter();
            }
        }

        /// <summary>
        /// Constructor for FavouriteViewModel.
        /// </summary>
        /// <param name="store">The place where store the CurrentViewModel for MainWindow binding.</param>
        public FavouriteViewModel(NavigationStore store, IFavouriteRepository favouriteRepository)
        {
            this.favouriteRepository = favouriteRepository;
            this.GetAllFavouriteLocations();
        }

        /// <summary>
        /// Get all favourite locations from server.
        /// </summary>
        public void GetAllFavouriteLocations()
        {
            this.ClearCollection();
            foreach (var location in this.favouriteRepository.GetAllFavouriteLocations().ToArray())
            {
                var number = new Random().Next(1, 21);
                var favCard = new FavouriteCard
                {
                    LocationId = location.LocationId,
                    Location = location.Name,
                    IconUrl = $"pack://application:,,,/Assets/{number}.png",
                    Coord = new Coord
                    {
                        Lat = location.Latitude,
                        Lon = location.Longitude,
                    }
                };

                FavouriteLocations.Add(favCard);
                FilteredLocations.Add(favCard);
            }
        }

        private void ClearCollection()
        {
            this.FavouriteLocations.Clear();
            this.FilteredLocations.Clear();
        }

        private void ApplyFilter()
        {
            this.FilteredLocations.Clear();
            if (string.IsNullOrEmpty(this.LocationFilter))
            {
                foreach (var fav in FavouriteLocations)
                {
                    FilteredLocations.Add(fav);
                }
            }
            else
            {
                foreach (var location in FavouriteLocations.Where(l => l.Location.ToLower().Contains(LocationFilter.ToLower())))
                {
                    FilteredLocations.Add(location);
                }
            }
        }

        private void AddSeedData()
        {
            var f1 = new FavouriteCard
            {
                Location = "Brisbane",
                IconUrl = "pack://application:,,,/Assets/brisbane.png",
                Coord = new Coord { Lat = -27.4679, Lon = 153.0281 }
            };

            var f2 = new FavouriteCard
            {
                Location = "London",
                IconUrl = "pack://application:,,,/Assets/london.png",
                Coord = new Coord { Lat = 51.5085, Lon = -0.1257 }
            };

            var f3 = new FavouriteCard
            {
                Location = "Xi'an",
                IconUrl = "pack://application:,,,/Assets/xi'an.png",
                Coord = new Coord { Lat = 34.2583, Lon = 108.9286 }
            };

            FavouriteLocations.Clear();
            FavouriteLocations.Add(f1);
            FavouriteLocations.Add(f2);
            FavouriteLocations.Add(f3);
        }
    }
}
