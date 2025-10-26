using Navigation.Commands;
using Navigation.Services;
using Navigation.Stores;
using System.Windows.Input;

namespace Navigation.ViewModels
{
    public class AccountViewModel : ViewModelBase
    {
        public string AccountMessage => "Account message";

        public ICommand NavigateHomeCommand { get; }

        public AccountViewModel(NavigationStore navigationStore)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(
                new NavigationService<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore)));
        }
    }
}
