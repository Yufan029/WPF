using Navigation.Stores;
using Navigation.ViewModels;

namespace Navigation.Services
{
    public class NavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private NavigationStore store;
        private Func<TViewModel> viewModelFactory;

        public NavigationService(NavigationStore store, Func<TViewModel> viewModelFactory)
        {
            this.store = store;
            this.viewModelFactory = viewModelFactory;
        }

        public void Navigate()
        {
            this.store.CurrentViewModel = viewModelFactory();
        }
    }
}
