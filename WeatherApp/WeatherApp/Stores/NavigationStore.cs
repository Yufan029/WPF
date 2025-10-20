using WeatherApp.ViewModels;

namespace WeatherApp.Stores
{
    public class NavigationStore
    {
        private ViewModelBase? currentViewModel;

        /// <summary>
        /// viewmodel will listen to this event, then update the UI. 
        /// </summary>
        public event Action? CurrentViewChanged;

        /// <summary>
        /// The CurrentViewModel which MainWindow binding to.
        /// </summary>
        public ViewModelBase? CurrentViewModel
        {
            get => this.currentViewModel;
            set
            {
                this.currentViewModel = value;
                OnCurrentViewChanged();
            }
        }

        /// <summary>
        /// Raised when current view changed, 
        /// </summary>
        public void OnCurrentViewChanged()
        {
            CurrentViewChanged?.Invoke();
        }
    }
}
