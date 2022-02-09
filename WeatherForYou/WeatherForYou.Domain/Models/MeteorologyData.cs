namespace WeatherForYou.Domain.Models;
public class MeteorologyData
{
    public int Id { get; set; }        
    public DateTime Time { get; set; }       
    public City City { get; set; }
    public int Temperature { get; set; }
    public string? WindDirection { get; set; }
    public double WindSpeed { get; set; }
}
