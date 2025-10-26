using Navigation.ViewModels;

namespace Navigation.Stores
{
    public class NavigationStore
    {
        public event Action? CurrentViewModelChanged;

        // store the current view model state.
        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => this.currentViewModel;
            set
            {
                this.currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
