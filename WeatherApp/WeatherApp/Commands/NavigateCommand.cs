using WeatherApp.Services;
using WeatherApp.ViewModels;

namespace WeatherApp.Commands
{
    public class NavigateCommand<TViewModel, TParam> : CommandBase where TViewModel : ViewModelBase
    {
        private readonly NavigationService<TViewModel, TParam> navigationService;

        /// <summary>
        /// Constructor for NavigateCommand
        /// </summary>
        /// <param name="navigationService">The navigation service, navigate to TViewModel, with parameter TParam</param>
        public NavigateCommand(NavigationService<TViewModel, TParam> navigationService)
        {
            this.navigationService = navigationService;
        }

        /// <summary>
        /// Navigate to the specific viewmodel.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override void Execute(object? parameter)
        {
            TParam param = parameter == null ? default : (TParam)parameter;
            this.navigationService.Navigate(param);
        }
    }
}
