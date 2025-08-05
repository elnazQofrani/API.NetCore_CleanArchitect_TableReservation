
using AutoMapper;
using Moq;
using NUnit.Framework;
using BusinessLayer.Repositories;
using BookTableReservation.Controllers;
using Microsoft.AspNetCore.Mvc;
using BookTableReservation.Models;
using Domain.Entities;

namespace TestingBookingTableReservation
{
    public class CustomerControllerTest
    {

        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private CustomerController _controller;

        [SetUp]
        public void Setup()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _mapperMock = new Mock<IMapper>();
            _controller = new CustomerController(_customerRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task Create_ReturnBadRequest_WhenCustomerDtoIsNull()
        {
            
            var result = (ObjectResult)await _controller.Create(null);
           
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("Invalid Customer data.", result.Value);
        }

        [Test]
        public async Task Create_CallRepositoryMethod_WhenValidCustomerDtoIsProvided()
        {
           
            var customerDto = new CustomerAddDto();
            var customerEntity = new Customer { Id = 1 };
            _mapperMock.Setup(m => m.Map<Customer>(customerDto)).Returns(customerEntity);
            _customerRepositoryMock.Setup(r => r.CreatAsync(customerEntity)).ReturnsAsync(customerEntity);

       
            await _controller.Create(customerDto);

           
            _customerRepositoryMock.Verify(r => r.CreatAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Test]
        public async Task Create_ReturnOkWithCustomerId_WhenCustomerCreatedSuccessfully()
        {
        
            var customerDto = new CustomerAddDto();
            var customerEntity = new Customer { Id = 1 , FirstName="Elnaz" , LastName ="Ghofrani" , Email="g@gmail.com" , PhoneNumber="07760854171"};
            _mapperMock.Setup(m => m.Map<Customer>(customerDto)).Returns(customerEntity);
            _customerRepositoryMock.Setup(r => r.CreatAsync(customerEntity)).ReturnsAsync(customerEntity);

    
            var result = await _controller.Create(customerDto) as OkObjectResult;

 
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
     
        }

        [Test]
        public async Task Create_ReturnInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var customerDto = new CustomerAddDto();
              var customerEntity = new Customer { Id = 1, FirstName = "Elnaz", LastName = "Ghofrani", Email = "g@gmail.com", PhoneNumber = "07760854171" };

            _mapperMock.Setup(m => m.Map<Customer>(customerDto)).Returns(customerEntity);
            // Act
            var result = await _controller.Create(customerDto) as ObjectResult;
           
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
           
        }
    }


}
