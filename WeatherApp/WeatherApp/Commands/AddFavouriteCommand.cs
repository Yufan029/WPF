using WeatherApp.ViewModels;

namespace WeatherApp.Commands
{
    public class AddFavouriteCommand : CommandBase
    {
        private readonly WeatherViewModel weatherViewModel;

        public AddFavouriteCommand(WeatherViewModel weatherViewModel)
        {
            this.weatherViewModel = weatherViewModel;
        }

        public override void Execute(object? parameter)
        {
            this.weatherViewModel.AddFavourite();
        }
    }
}
