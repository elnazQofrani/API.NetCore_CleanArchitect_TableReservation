using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Domain.Entities;
using BusinessLayer.Repositories;
using Moq;
using Newtonsoft.Json;
using BookTableReservation.Models;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IntergatedTestBookTableReservation
{
    public class CustomerControllerIntegrationTests
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
                      
                        var mockRepo = new Mock<ICustomerRepository>();
                        mockRepo.Setup(repo => repo.CreatAsync(It.IsAny<Customer>()))
                            .ReturnsAsync(new Customer { Id = 1 });

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
        public async Task Create_ReturnOk_WithValidCustomer()
        {
            
            var customerDto = new CustomerAddDto
            {
                FirstName = "Elnaz",
                LastName = "Test",
                Email = "elnaz@test.com"
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(customerDto), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/customer", jsonContent);

            Assert.Equals(HttpStatusCode.OK, response.StatusCode);
          
        }


    }
}
