using Navigation.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigation.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private NavigationStore store;

        public ViewModelBase CurrentViewModel => this.store.CurrentViewModel;

        public MainViewModel(NavigationStore store)
        {
            this.store = store;
            this.store.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
