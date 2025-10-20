using WeatherApp.Stores;

namespace WeatherApp.ViewModels
{
    /// <summary>
    /// The MainWindowViewModle
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly NavigationStore store;

        /// <summary>
        /// The CurrentViewModel binding to the view.
        /// </summary>
        public ViewModelBase? CurrentViewModel => this.store.CurrentViewModel;

        public MainWindowViewModel(NavigationStore store)
        {
            this.store = store;
            this.store.CurrentViewChanged += Store_CurrentViewChanged;
        }

        /// <summary>
        /// Update the UI when current view changed in the store.
        /// </summary>
        private void Store_CurrentViewChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
