using Microsoft.EntityFrameworkCore;
using WeatherForYou.Domain.Abstract;
using WeatherForYou.Domain.Contexts;
using WeatherForYou.Domain.Models;

namespace WeatherForYou.Domain.Concrete.Repositories;
public class MetereologyRepository : IRepository<MeteorologyData>
{
    private MeteorologyContext _metereologyContext;

    public MetereologyRepository(MeteorologyContext metereologyContext)
    {
        _metereologyContext = metereologyContext;
    }

    public async Task<List<MeteorologyData>> GetAllAsync()
        => await _metereologyContext.Meteorologies.ToListAsync();

    public async Task<MeteorologyData> GetAsync(int id) =>
        await _metereologyContext.Meteorologies.FirstOrDefaultAsync(m => m.Id.Equals(id));

    public async void Add(MeteorologyData entity)
    {
        if (entity == null)
            return;
        await _metereologyContext.Meteorologies.AddAsync(entity);
    }

    public async void AddRange(IEnumerable<MeteorologyData> entities)
    {
        if (entities is null)
            return;
        await _metereologyContext.AddRangeAsync(entities);
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
        _metereologyContext.Remove(temp);
    }

    public async void Save()
    {
        await _metereologyContext.SaveChangesAsync();
    }

    
}

