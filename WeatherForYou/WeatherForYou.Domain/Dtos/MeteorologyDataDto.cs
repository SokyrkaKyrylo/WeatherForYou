namespace WeatherForYou.Domain.Dtos
{
    public class MeteorologyDataDto
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string? City { get; set; }
        public int Temperature { get; set; }
        public string? WindDirection { get; set; }
        public double WindSpeed { get; set; }
    }
}
