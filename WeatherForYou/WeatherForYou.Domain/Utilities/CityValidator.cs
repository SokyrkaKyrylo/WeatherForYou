namespace WeatherForYou.Domain.Utilities
{
    internal class CityValidator
    {
        static IEnumerable<string> _city = new List<string>
        {
            "київ",
            "дніпропетровськ",
            "харків"
        };

        public static bool ValidateCityName(string name)
            => _city.Contains(name.ToLower());
    }
}
