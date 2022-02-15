namespace WeatherForYou.Domain.Utilities
{
    internal class TranslationHelper
    {
        public static Dictionary<string, string> Winds = new Dictionary<string, string> {
            { "северный", "Північний" },
            { "южный", "Південний" },
            { "западный", "Західний" },
            { "восточный", "Східний" },
            { "c-з", "Півн-З" },
            { "с-в", "Півн-С" },
            { "ю-в", "Півд-С" },
            { "ю-з", "Півд-З" }
        };

        public static string TranslateWindDirrectionName(string nameToTranslate)
        {
            if (nameToTranslate == null)
                return null;
            string result;
            if (Winds.TryGetValue(nameToTranslate.ToLower(), out result))
                return result;
            if (Winds.Values.Select(c => c.ToLower()).Contains(nameToTranslate.ToLower()))
                return nameToTranslate;
            return null;
        }
    }
}
