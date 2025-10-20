using System.Text.Json.Serialization;

namespace WeatherApp.Models
{
    public class Coord
    {
        [JsonPropertyName("lon")]
        public double Lon { get; set; }

        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        public override string ToString()
        {
            return $"Lat: {Lat}, Lon: {Lon}";
        }
    }
}
