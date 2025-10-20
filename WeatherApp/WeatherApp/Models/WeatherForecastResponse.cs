using System.Text.Json.Serialization;

namespace WeatherApp.Models
{
    public class WeatherForecastResponse
    {
        [JsonPropertyName("list")]
        public WeatherForecastInfo[] WeatherForecasts { get; set; }
    }
}
