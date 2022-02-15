using Microsoft.EntityFrameworkCore;
using WeatherForYou.Domain.Abstract;
using WeatherForYou.Domain.Contexts;
using WeatherForYou.Domain.Models;

namespace WeatherForYou.Domain.Concrete.Repositories;
public class MetereologyRepository : IRepository<MeteorologyData>
{
    private DatabaseContext _metereologyContext;

    public MetereologyRepository(DatabaseContext metereologyContext)
    {
        _metereologyContext = metereologyContext;
    }

    public async Task<List<MeteorologyData>> GetAllAsync()
        => await _metereologyContext.Meteorologies
        .Include(c => c.City)
        .ToListAsync();

    public async Task<MeteorologyData> GetAsync(int id) =>
        await _metereologyContext.Meteorologies
        .FirstOrDefaultAsync(m => m.Id.Equals(id));

    public async void Add(MeteorologyData entity)
    {
        if (entity == null)
            return;

        var city = _metereologyContext.Cities.Find(entity.CityId);

        if (city == null)
            city = _metereologyContext.Cities.FirstOrDefault(c => c.Name == entity.City.Name);

        if (city == null)
        {
            _metereologyContext.Cities.Add(entity.City);
            _metereologyContext.SaveChanges();
        }
        else
        {
            entity.CityId = city.Id;
            entity.City = city;
        }

        await _metereologyContext.Meteorologies.AddAsync(entity);
    }

    public void AddRange(IEnumerable<MeteorologyData> entities)
    {
        if (entities is null || entities.Count() == 0)
            return;

        foreach (var entity in entities)
            Add(entity);
    }

    public void Update(MeteorologyData entity)
    {
        _metereologyContext.Entry(entity).State = EntityState.Modified;
    }

    public async void Delete(int id)
    {
        var temp = await _metereologyContext.Meteorologies.FirstOrDefaultAsync(m => m.Id.Equals(id));
        if (temp == null)
            return;
        _metereologyContext.Meteorologies.Remove(temp);
    }

    public async Task<Task> Save()
    {
        await _metereologyContext.SaveChangesAsync();
        return Task.CompletedTask;
    }

    public ValidationInformation CheckIfDataIsValid()
    {
        var invalidData = _metereologyContext.Meteorologies
            .Where(d => d.WindDirection == null
            || d.WindSpeed == null
            || d.Temperature == null);

        var result = new ValidationInformation() { InvalidMeteorologiesData = new() };
        
        foreach (var dataByTime in invalidData)
        {
            if (!result.InvalidMeteorologiesData.Any(d => 
                d.Date.Year.Equals(dataByTime.Time.Year) 
                && d.Date.Month.Equals(dataByTime.Time.Month)
                && d.Date.Day.Equals(dataByTime.Time.Day) 
                && d.CityId.Equals(dataByTime.CityId)))
            {
                result.InvalidMeteorologiesData.Add(new DateWithCity
                {
                    CityId = dataByTime.CityId,
                    Date = dataByTime.Time
                });
            }                
        }

        if (result.InvalidMeteorologiesData.Any())
        {
            result.Status = false;
            result.Message = "There are some invalid or missing data";
        }
        else
        {
            result.Status = true;
        }

        return result;
    }

    public class ValidationInformation
    {
        public bool Status { get; set; }

        public string? Message { get; set; }

        public List<DateWithCity>? InvalidMeteorologiesData { get; set; }
    }
}
