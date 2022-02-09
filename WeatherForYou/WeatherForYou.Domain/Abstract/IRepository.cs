namespace WeatherForYou.Domain.Abstract;
public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T> GetAsync(int id);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Update(T entity);
    void Delete(int id);
    void Save();
}
