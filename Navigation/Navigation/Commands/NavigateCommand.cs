using Navigation.Services;
using Navigation.ViewModels;

namespace Navigation.Commands
{
    public class NavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        private NavigationService<TViewModel> navigationService;

        public NavigateCommand(NavigationService<TViewModel> navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            this.navigationService.Navigate();
        }
    }
}
