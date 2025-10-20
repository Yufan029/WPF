using Microsoft.EntityFrameworkCore;
using WeatherApp.Dtos;

namespace WeatherApp.Repository
{
    public class WeatherAppDbContext : DbContext
    {
        public WeatherAppDbContext(DbContextOptions<WeatherAppDbContext> options)
            : base(options) { }

        public DbSet<FavouriteLocation> FavouriteLocations { get; set; }
    }
}
