
using WeatherForYou.Domain.Models;

namespace WeatherForYou.Domain.Abstract;
public interface IDataLoader
{
    IEnumerable<MeteorologyData> GetDataFromDirectory(string[] path);
    IEnumerable<MeteorologyData> GetDataFromFile(string path);
    IEnumerable<string> CheckForANewFiles();
}
