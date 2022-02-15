using WeatherForYou.Domain.Models;

namespace WeatherForYou.Domain.Abstract
{
    public interface IDataProcessor
    {
        void RestoreDataLinearInterpolationAsync(IEnumerable<DateWithCity> incorrectDates);
    }
}
