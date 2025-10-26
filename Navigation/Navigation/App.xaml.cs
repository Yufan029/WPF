using Navigation.Stores;
using Navigation.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Navigation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationStore store = new NavigationStore();
            store.CurrentViewModel = new HomeViewModel(store);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(store)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }

}
