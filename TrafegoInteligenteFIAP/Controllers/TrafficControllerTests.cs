using Microsoft.AspNetCore.Mvc;
using TrafegoInteligenteFIAP.Data;
using TrafegoInteligenteFIAP.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;


namespace TrafegoInteligenteFIAP.Controllers
{
    public class TestFixture
    {
        public ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Ensure the DB is clean for each run
                .Options;

            var dbContext = new ApplicationDbContext(options);

            // Seed the database
            dbContext.TrafficDatas.Add(new TrafficData { Id = 1, Intersection = "A1", VehicleCount = 150, Timestamp = DateTime.Now });
            dbContext.WeatherConditions.Add(new WeatherCondition { Id = 1, WeatherType = "Rainy", Timestamp = DateTime.Now });
            dbContext.TrafficLights.Add(new TrafficLight { Id = 1, Intersection = "A1", Status = "Red" });

            dbContext.SaveChanges();

            return dbContext;
        }
    }
    public class TrafficControllerTests
    {
        [Fact]
        public async Task GetTrafficData_Returns200_WhenDataExists()
        {
            // Arrange
            var dbContext = new TestFixture().GetDbContext();
            var controller = new TrafficController(dbContext);

            // Act
            var result = await controller.GetTrafficData("A1");

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, actionResult.StatusCode);
        }

        [Fact]
        public async Task UpdateTrafficLightBasedOnConditions_Returns204_WhenUpdatedSuccessfully()
        {
            // Arrange
            var dbContext = new TestFixture().GetDbContext();
            var controller = new TrafficController(dbContext);

            // Act
            var result = await controller.UpdateTrafficLightBasedOnConditions("A1");

            // Assert
            var actionResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, actionResult.StatusCode); // 204 No Content for successful update with no return value
        }

    }

}
