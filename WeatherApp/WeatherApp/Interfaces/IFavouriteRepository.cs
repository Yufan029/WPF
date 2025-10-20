using WeatherApp.Dtos;

namespace WeatherApp.Interfaces
{
    public interface IFavouriteRepository
    {

        public IEnumerable<FavouriteLocation> GetAllFavouriteLocations();

        public void AddFavouriteLocation(FavouriteLocation favouriteLocation);

        public void DeleteFavouriteLocation(int id);
    }
}
