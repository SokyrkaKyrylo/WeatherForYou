using WeatherForYou.Domain.Dtos;

namespace WeatherForYou.Domain.Abstract;
public interface IDataLoader
{
    IEnumerable<MeteorologyDataDto> GetDataFromDirectory(string[] path);
    IEnumerable<MeteorologyDataDto> GetDataFromFile(string path);
    IEnumerable<string> CheckForANewFiles();
}
