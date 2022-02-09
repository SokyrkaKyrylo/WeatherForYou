using Microsoft.EntityFrameworkCore;
using WeatherForYou.Domain.Models;

namespace WeatherForYou.Domain.Contexts
{
    public class MeteorologyContext : DbContext
    {
        public DbSet<MeteorologyData> Meteorologies { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;

        public MeteorologyContext(DbContextOptions<MeteorologyContext> contextOptions) 
            : base(contextOptions)
        {
            Database.EnsureCreated();
        }      
    }
}
