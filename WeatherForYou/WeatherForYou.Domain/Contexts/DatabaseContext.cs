using Microsoft.EntityFrameworkCore;
using WeatherForYou.Domain.Models;

namespace WeatherForYou.Domain.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DbSet<MeteorologyData> Meteorologies { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;

        public DatabaseContext(DbContextOptions<DatabaseContext> contextOptions) 
            : base(contextOptions)
        {
            Database.EnsureCreated();
        }      
    }
}
