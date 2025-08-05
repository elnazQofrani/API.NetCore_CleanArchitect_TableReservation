
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using Domain.Entities;
using BusinessLayer.Repositories;
using Moq;
using Newtonsoft.Json;
using BookTableReservation.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;



namespace IntergatedTestBookTableReservation
{
    public class SeatControllerIntegrationTests
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;


        [SetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        // Mock the ISeatRepository
                        var mockRepo = new Mock<ISeatRepository>();
                        mockRepo.Setup(repo => repo.GetAvailableSeats(It.IsAny<DateTime>(), It.IsAny<TimeSpan>()))
                            .ReturnsAsync(new List<Seat>
                            {
                                new Seat { Id = 1, SeatNumber = 1 },
                                new Seat { Id = 2, SeatNumber = 2 }
                            });

                        services.AddScoped(_ => mockRepo.Object);
                    });
                });

            _client = _factory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client?.Dispose();
            _factory?.Dispose();
        }


        [Test]
        public async Task GetAvailableSeats_ReturnOk_WithValidPArameters()
        {
          
            var desiredDateTime = DateTime.Now.ToString("yyyy-MM-dd");
            var desiredStartTime = "10:00:00"; 

  
            var response = await _client.GetAsync($"/api/seat/{desiredDateTime}/{desiredStartTime}");

            Assert.Equals(HttpStatusCode.OK, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            var seats = JsonConvert.DeserializeObject<List<SeatsDto>>(responseString);
           
            Assert.Equals(2, seats.Count);  
        }








    }
}
