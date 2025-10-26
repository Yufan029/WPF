using Navigation.Services;
using Navigation.ViewModels;
using System.Windows;

namespace Navigation.Commands
{
    public class LoginCommand : CommandBase
    {
        private readonly NavigationService<AccountViewModel> navigationService;

        public LoginCommand(NavigationService<AccountViewModel> navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            MessageBox.Show("login success");

            this.navigationService.Navigate();
        }
    }
}
