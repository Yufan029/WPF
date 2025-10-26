using Navigation.Commands;
using Navigation.Services;
using Navigation.Stores;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Navigation.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string usename;
        private string password;
        private readonly NavigationStore store;

        public ICommand LoginCommand { get; }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string Usename
        {
            get => usename;
            set
            {
                usename = value;
                OnPropertyChanged(nameof(Usename));
            }
        }

        public LoginViewModel(NavigationStore store)
        {
            this.store = store;
            LoginCommand = new LoginCommand(new NavigationService<AccountViewModel>(store, () => new AccountViewModel(store)));
        }
    }
}
