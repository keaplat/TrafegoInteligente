namespace TrafegoInteligenteFIAP.Models
{
    public class TrafficData
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int VehicleCount { get; set; }
        public string Intersection { get; set; }
    }

    public class WeatherCondition
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string WeatherType { get; set; }  // Ex: Climas tipo chuva, sol.
    }

    public class TrafficLight
    {
        public int Id { get; set; }
        public string Intersection { get; set; }
        public string Status { get; set; }  // Ex: Vermelho, verde e amarelo
    }

}
