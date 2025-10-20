using WeatherApp.Services;

namespace WeatherAppTests.ServicesTests
{
    [TestFixture]
    public class WeatherServiceTests
    {
        /// <summary>
        /// Test regular expression with different combinations.
        /// </summary>
        /// <param name="input">The lat and lon values pair.</param>
        /// <param name="expected">The expect result.</param>
        [TestCase("0,0", true)]
        [TestCase("       45.123, -93.456", true)]
        [TestCase("-90,180", true)]
        [TestCase("     90.0     ,       -180.0       ", true)]
        [TestCase("-27.54819194256934, 153.0862658954353", true)]
        [TestCase("27.548, 153.086", true)]
        [TestCase("91,0", false)]
        [TestCase("0,181", false)]
        [TestCase("abc,123", false)]
        [TestCase("45.0 -93.0", false)]
        [TestCase("", false)]
        [TestCase("s 37.7749, e 122.4194", false)]
        [TestCase("33.8688° S, 151.2093° E", false)]
        public void ValidLatLon_test(string input, bool expected)
        {
            bool isMatch = WeatherService.ValidLatLon(input);
            Assert.That(isMatch, Is.EqualTo(expected));
        }
    }
}
