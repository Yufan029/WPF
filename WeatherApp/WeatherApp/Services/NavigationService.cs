using WeatherApp.Stores;
using WeatherApp.ViewModels;

namespace WeatherApp.Services
{
    /// <summary>
    /// Navigation service for navigate between viewmodels.
    /// </summary>
    /// <typeparam name="TViewModel">The viewmodels.</typeparam>
    public class NavigationService<TViewModel, TParam> where TViewModel : ViewModelBase
    {
        private readonly NavigationStore store;
        private Func<TParam, TViewModel> viewModelFactory;

        /// <summary>
        /// Constructor for NavigationService.
        /// </summary>
        /// <param name="store">The place where CurrentViewModel store.</param>
        /// <param name="viewModelFactory">The callback factory function to create the viewmodel.</param>
        public NavigationService(NavigationStore store, Func<TParam, TViewModel> viewModelFactory)
        {
            this.store = store;
            this.viewModelFactory = viewModelFactory;
        }

        /// <summary>
        /// Navigate to specific viewmodel.
        /// </summary>
        public void Navigate(TParam param)
        {
            var viewModel = this.viewModelFactory(param);
            if (viewModel is FavouriteViewModel fav)
            {
                fav.GetAllFavouriteLocations();
            }

            this.store.CurrentViewModel = viewModel;
        }
    }
}
