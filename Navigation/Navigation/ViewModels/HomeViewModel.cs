using Navigation.Commands;
using Navigation.Services;
using Navigation.Stores;
using System.Windows.Input;

namespace Navigation.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public string WelcomeMessage => "Welcome to my application.";

        public ICommand NavigateLoginCommand { get; }


        public HomeViewModel(NavigationStore store)
        {
            NavigateLoginCommand = new NavigateCommand<LoginViewModel>(
                new NavigationService<LoginViewModel>(store, () => new LoginViewModel(store)));
        }
    }
}
