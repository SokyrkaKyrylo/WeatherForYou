using Microsoft.EntityFrameworkCore;
using WeatherForYou.Domain.Abstract;
using WeatherForYou.Domain.Contexts;
using WeatherForYou.Domain.Models;
using WeatherForYou.Domain.Utilities;

namespace WeatherForYou.Domain.Concrete.Services;

public class DataProcessor : IDataProcessor
{
    public DataProcessor(DatabaseContext context)
    {
        _context = context;
    }
    private DatabaseContext _context;

    public async void RestoreDataLinearInterpolationAsync(IEnumerable<DateWithCity> incorrectDates)
    {
        if (incorrectDates == null)
            return;

        var winds = TranslationHelper.Winds.Values.ToList();

        foreach (var data in incorrectDates)
        {
            var dataToRestore = _context.Meteorologies.Include(x => x.City)
                .Where(d => d.Time.Year.Equals(data.Date.Year)
                && d.Time.Month.Equals(data.Date.Month)
                && d.Time.Day.Equals(data.Date.Day)
                && d.CityId.Equals(data.CityId)).ToList();

            for (int i = 0; i < dataToRestore.Count; i++)
            {
                if (dataToRestore.ElementAt(i).WindDirection is null)
                {
                    UpdateWindDirection(dataToRestore, dataToRestore.ElementAt(i), winds);
                }
                if (dataToRestore.ElementAt(i).Temperature is null)
                {
                    UpdateTemperature(dataToRestore, dataToRestore.ElementAt(i));
                }
                if (dataToRestore.ElementAt(i).WindSpeed is null)
                {
                    UpdateWindSpeed(dataToRestore, dataToRestore.ElementAt(i));
                }
                _context.Entry(dataToRestore.ElementAt(i)).State = EntityState.Modified;
            }
        }
        await _context.SaveChangesAsync();
    }

    private void UpdateWindSpeed(List<MeteorologyData> dataToRestore, MeteorologyData objectToRestore)
    {
        var i = dataToRestore.IndexOf(objectToRestore);
        if (i == 0)
        {
            var j = GetNotNullWindSpeed(dataToRestore, i);
            objectToRestore.Temperature = dataToRestore[j].Temperature;
        }
        else if (i + 1 < dataToRestore.Count)
        {
            var j = GetNotNullWindSpeed(dataToRestore, i);

            var funcBefore = dataToRestore.ElementAt(i - 1).Temperature;
            var funcAfter = dataToRestore.ElementAt(j).Temperature;

            var toFind = funcBefore + ((funcAfter - funcBefore) / (j - i)) * (i + 1 - i);

            objectToRestore.Temperature = toFind;
        }
        else
        {
            objectToRestore.Temperature = dataToRestore.ElementAt(i - 1).Temperature;
        }
    }

    private void UpdateWindDirection(List<MeteorologyData> dataToRestore, MeteorologyData objectToRestore, List<string> winds)
    {
        var i = dataToRestore.IndexOf(objectToRestore);
        if (i == 0)
        {
            var j = GetNotNullWindDirection(dataToRestore, i);
            objectToRestore.WindDirection = dataToRestore[j].WindDirection;
        }
        else if (i + 1 < dataToRestore.Count)
        {
            var j = GetNotNullWindDirection(dataToRestore, i);

            var funcBefore = winds.IndexOf(dataToRestore.ElementAt(i - 1).WindDirection);
            var funcAfter = winds.IndexOf(dataToRestore.ElementAt(j).WindDirection);

            var toFind = funcBefore + ((funcAfter - funcBefore) / (j - i)) * (i + 1 - i);

            objectToRestore.WindDirection = winds.ElementAt(toFind);
        }
        else
        {
            objectToRestore.WindDirection = dataToRestore.ElementAt(i - 1).WindDirection;
        }

    }

    private void UpdateTemperature(List<MeteorologyData> dataToRestore, MeteorologyData objectToRestore)
    {
        var i = dataToRestore.IndexOf(objectToRestore);
        if (i == 0)
        {
            var j = GetNotNullTemperature(dataToRestore, i);
            objectToRestore.Temperature = dataToRestore[j].Temperature;
        }
        else if (i + 1 < dataToRestore.Count)
        {
            var j = GetNotNullTemperature(dataToRestore, i);

            var funcBefore = dataToRestore.ElementAt(i - 1).Temperature;
            var funcAfter = dataToRestore.ElementAt(j).Temperature;

            var toFind = funcBefore + ((funcAfter - funcBefore) / (j - i)) * (i + 1 - i);

            objectToRestore.Temperature = toFind;
        }
        else
        {
            objectToRestore.Temperature = dataToRestore.ElementAt(i - 1).Temperature;
        }
    }

    private int GetNotNullWindSpeed(List<MeteorologyData> meteorologies, int startIndex)
    {
        var j = startIndex + 1;
        while (meteorologies.ElementAt(j).WindSpeed is null)
        {
            if (j + 1 >= meteorologies.Count)
            {
                return startIndex - 1;
            }
            j++;
        }
        return j;
    }

    private int GetNotNullTemperature(List<MeteorologyData> meteorologies, int startIndex)
    {
        var j = startIndex + 1;
        while (meteorologies.ElementAt(j).Temperature is null)
        {
            if (j + 1 >= meteorologies.Count)
            {
                return startIndex - 1;
            }
            j++;
        }
        return j;
    }

    private int GetNotNullWindDirection(List<MeteorologyData> meteorologies, int startIndex)
    {
        var j = startIndex + 1;
        while (meteorologies.ElementAt(j).WindDirection is null)
        {
            if (j + 1 >= meteorologies.Count)
            {
                return startIndex - 1;
            }
            j++;
        }
        return j;
    }
}
