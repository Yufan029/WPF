using WeatherApp.Models;

namespace WeatherApp.Interfaces
{
    /// <summary>
    /// Weather service interface.
    /// </summary>
    public interface IWeatherServices
    {
        /// <summary>
        /// Get weather results.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>The weather response result.</returns>
        Task<WeatherResponse> GetWeatherAsync(string location);

        /// <summary>
        /// Get weather forecast results.
        /// </summary>
        /// <param name="lat">The latitude of the location.</param>
        /// <param name="lon">The longitude of the location.</param>
        /// <returns>The weather forecast results.</returns>
        Task<WeatherForecastResponse> GetWeatherForecastAsync(double lat, double lon);
    }
}
