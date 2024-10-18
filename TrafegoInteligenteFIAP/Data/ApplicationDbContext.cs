using System.Collections.Generic;
using TrafegoInteligenteFIAP.Models;
using Microsoft.EntityFrameworkCore;

namespace TrafegoInteligenteFIAP.Data
{
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TrafficData> TrafficDatas { get; set; }
        public DbSet<WeatherCondition> WeatherConditions { get; set; }
        public DbSet<TrafficLight> TrafficLights { get; set; }

    }
}

