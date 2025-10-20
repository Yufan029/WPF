using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using WeatherApp.Interfaces;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    /// <summary>
    /// The weather service for getting the weather report results.
    /// </summary>
    public class WeatherService : IWeatherServices
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string ApiKey = "OpenWeather-API-KEY";

        /// <summary>
        /// The weather results async.
        /// </summary>
        /// <param name="location">The location from the input.</param>
        /// <returns>Async weather report response.</returns>
        public async Task<WeatherResponse> GetWeatherAsync(string location)
        {
            var url = string.Empty;
            if (ValidLatLon(location))
            {
                var latlon = location.Split(',', StringSplitOptions.RemoveEmptyEntries);
                var latValid = double.TryParse(latlon[0], NumberStyles.Float, CultureInfo.InvariantCulture, out double lat);
                var lonValid = double.TryParse(latlon[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double lon);
                if (latValid && lonValid)
                {
                    url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={ApiKey}";
                }
            }
            else
            {
                url = $"https://api.openweathermap.org/data/2.5/weather?q={location}&appid={ApiKey}";
            }

            return await GetWeatherInfoAsync(url);
        }

        /// <summary>
        /// Get the weather forecast results.
        /// </summary>
        /// <param name="lat">The latitude.</param>
        /// <param name="lon">The Longitude.</param>
        /// <returns>The weather forecast results.</returns>
        public async Task<WeatherForecastResponse> GetWeatherForecastAsync(double lat, double lon)
        {
            var url = $"https://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&appid={ApiKey}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<WeatherForecastResponse>(json);
        }

        /// <summary>
        /// Check if the location is a valid lat lon value pair. 
        /// </summary>
        /// <param name="location"></param>
        /// <returns>True if valid, otherwise false.</returns>
        public static bool ValidLatLon(string location)
        {
            string pattern = @"^\s*(-?([1-8]?\d(\.\d+)?|90(\.0+)?))\s*,\s*(-?(180(\.0+)?|((1[0-7]\d)|(\d{1,2}))(\.\d+)?))\s*$";
            return Regex.IsMatch(location, pattern);
        }

        private async Task<WeatherResponse> GetWeatherInfoAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<WeatherResponse>(json);
        }
    }
}
