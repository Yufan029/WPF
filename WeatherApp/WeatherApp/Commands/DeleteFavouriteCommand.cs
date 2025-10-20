using WeatherApp.ViewModels;

namespace WeatherApp.Commands
{
    public class DeleteFavouriteCommand : CommandBase
    {
        private readonly WeatherViewModel weatherViewModel;

        public DeleteFavouriteCommand(WeatherViewModel weatherViewModel)
        {
            this.weatherViewModel = weatherViewModel;
        }

        /// <summary>
        /// Delegate delete favourite location functionality to weather viewmodel.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object? parameter)
        {
            this.weatherViewModel.DeleteFavouriteLocation();
        }
    }
}
