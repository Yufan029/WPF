using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Repository
{
    public class WeatherAppDbContextFactory : IDesignTimeDbContextFactory<WeatherAppDbContext>
    {
        public WeatherAppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WeatherAppDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=.;Database=WeatherAppDB;Integrated Security=true;TrustServerCertificate=True");

            return new WeatherAppDbContext(optionsBuilder.Options);
        }
    }
}
