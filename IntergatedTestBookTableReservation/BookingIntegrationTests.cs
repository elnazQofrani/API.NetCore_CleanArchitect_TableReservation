using BookTableReservation.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using BusinessLayer.Repositories;
using BusinessLayer.Services;


namespace IntergatedTestBookTableReservation
{
    public class BookingIntegrationTests
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
                       
                        var mockCustomerService = new Mock<ICustomerService>();
                        mockCustomerService.Setup(service => service.GetCustomerByIdAsync(It.IsAny<int>()))
                            .ReturnsAsync(new Customer { Id = 1, FirstName = "John", LastName = "Doe" });

                        var mockSeatService = new Mock<ISeatService>();
                        mockSeatService.Setup(service => service.IsSeatsAvailable(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<TimeSpan>()))
                            .ReturnsAsync(true); 

                        var mockBookingRepository = new Mock<IBookingRepository>();
                        mockBookingRepository.Setup(repo => repo.CreatAsync(It.IsAny<Booking>()))
                            .ReturnsAsync(new Booking { Id = 1 });

                        services.AddScoped(_ => mockCustomerService.Object);
                        services.AddScoped(_ => mockSeatService.Object);
                        services.AddScoped(_ => mockBookingRepository.Object);
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
        public async Task Create_ReturnOk_WithValidParameter()
        {
        
            var bookingDto = new BookingDto
            {
                CustomerId = 1,
                SeatId = 1,
                BookingDateTime = DateTime.Now.AddHours(1),
                StartTime = TimeSpan.Parse( "10:00:00")
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(bookingDto), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/booking", jsonContent);

            Assert.Equals(HttpStatusCode.OK, response.StatusCode);
          
     
        }

    }
}
