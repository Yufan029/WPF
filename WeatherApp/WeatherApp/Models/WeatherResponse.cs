using System.Text.Json.Serialization;

namespace WeatherApp.Models
{
    public class WeatherResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }


        [JsonPropertyName("main")]
        public WeatherDetails WeatherDetails { get; set; }


        [JsonPropertyName("weather")]
        public WeatherInfo[] Weather { get; set; }


        [JsonPropertyName("coord")]
        public Coord Coord { get; set; }

        [JsonPropertyName("wind")]
        public Wind Wind { get; set; }

        [JsonPropertyName("sys")]
        public SysInfo SysInfo { get; set; }
    }
}
