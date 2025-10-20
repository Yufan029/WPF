using System.Text.Json.Serialization;

namespace WeatherApp.Models
{
    public class WeatherForecastInfo
    {
        [JsonPropertyName("dt")]
        public int UtcDatetime { get; set; }

        [JsonPropertyName("dt_txt")]
        public string TimeText { get; set; }

        [JsonPropertyName("main")]
        public WeatherDetails WeatherDetails { get; set; }

        [JsonPropertyName("weather")]
        public WeatherInfo[] Weather { get; set; }
    }
}
