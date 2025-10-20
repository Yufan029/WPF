using WeatherApp.Dtos;
using WeatherApp.Interfaces;

namespace WeatherApp.Repository
{
    public class FavouriteRepository : Repository<FavouriteLocation>, IFavouriteRepository
    {
        public FavouriteRepository(WeatherAppDbContext context)
            : base(context)
        {
        }

        public IEnumerable<FavouriteLocation> GetAllFavouriteLocations()
        {
            return this.GetAll();
        }

        public void AddFavouriteLocation(FavouriteLocation favouriteLocation)
        {
            this.Add(favouriteLocation);
            this.Save();
        }

        public void DeleteFavouriteLocation(int id)
        {
            this.Delete(id);
            this.Save();
        }
    }
}
