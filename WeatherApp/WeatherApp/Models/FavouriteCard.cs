namespace WeatherApp.Models
{
    public class FavouriteCard
    {
        public int LocationId { get; set; }
        public string Location { get; set; }
        public string IconUrl { get; set; }
        public Coord Coord { get; set; }
    }
}
