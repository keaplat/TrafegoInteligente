using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrafegoInteligenteFIAP.Data;
using TrafegoInteligenteFIAP.Models;

namespace TrafegoInteligenteFIAP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrafficController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TrafficController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get Traffic and Weather Data for an intersection
        [HttpGet("traffic/{intersection}")]
        public async Task<IActionResult> GetTrafficData(string intersection)
        {
            var trafficData = await _context.TrafficDatas
                                .Where(t => t.Intersection == intersection)
                                .OrderByDescending(t => t.Timestamp)
                                .FirstOrDefaultAsync();
            var weatherData = await _context.WeatherConditions
                                .OrderByDescending(w => w.Timestamp)
                                .FirstOrDefaultAsync();

            if (trafficData == null || weatherData == null)
                return NotFound("Dados de transito e tempo indisponiveis.");

            return Ok(new { TrafficData = trafficData, Weather = weatherData });
        }

        // Update Traffic Light Based on Latest Conditions
        [HttpPost("updateTrafficLight/{intersection}")]
        public async Task<IActionResult> UpdateTrafficLightBasedOnConditions(string intersection)
        {
            var trafficData = await _context.TrafficDatas
                                .Where(t => t.Intersection == intersection)
                                .OrderByDescending(t => t.Timestamp)
                                .FirstOrDefaultAsync();
            var weatherData = await _context.WeatherConditions
                                .OrderByDescending(w => w.Timestamp)
                                .FirstOrDefaultAsync();

            if (trafficData == null || weatherData == null)
                return NotFound("Dados de transito e tempo indisponiveis.");

            var trafficLight = await _context.TrafficLights
                                 .FirstOrDefaultAsync(l => l.Intersection == intersection)
                                 ?? _context.TrafficLights.Add(new TrafficLight { Intersection = intersection, Status = "Green" }).Entity;

            // Decision logic for traffic light status
            if (weatherData.WeatherType.Contains("Chuva") && trafficData.VehicleCount > 100)
                trafficLight.Status = "Vermelho";
            else if (trafficData.VehicleCount < 50)
                trafficLight.Status = "Verde";
            else
                trafficLight.Status = "Amarelo"; 

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("allTrafficLights")]
        public async Task<IActionResult> GetAllTrafficLights()
        {
            var trafficLights = await _context.TrafficLights.ToListAsync();
            return Ok(trafficLights);
        }

        [HttpPost("overrideTrafficLight/{intersection}")]
        public async Task<IActionResult> OverrideTrafficLight(string intersection, [FromBody] string status)
        {
            var trafficLight = await _context.TrafficLights
                .FirstOrDefaultAsync(l => l.Intersection == intersection);

            if (trafficLight == null) return NotFound("Semaforo não encontrado.");

            trafficLight.Status = status;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
