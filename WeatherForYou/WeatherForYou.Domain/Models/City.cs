using Microsoft.EntityFrameworkCore;

namespace WeatherForYou.Domain.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }        
    }
}
