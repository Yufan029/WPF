namespace WeatherApp.Models
{
    public class WeatherCard
    {
        public int UtcTime { get; set; }
        public string TimeText { get; set; }
        public string IconUrl { get; set; }
        public string Temperature { get; set; }
        public string Description { get; set; }
    }
}
