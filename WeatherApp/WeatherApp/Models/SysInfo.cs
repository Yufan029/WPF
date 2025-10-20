using System.Text.Json.Serialization;

namespace WeatherApp.Models
{
    public class SysInfo
    {
        [JsonPropertyName("country")]
        public string Country { get; set; }
    }
}
