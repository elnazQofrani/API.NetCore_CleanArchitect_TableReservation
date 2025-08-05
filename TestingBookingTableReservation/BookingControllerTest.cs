using AutoMapper;
using BookTableReservation.Controllers;
using Domain.Entities;
using Domain.Enums;
using BookTableReservation.Models;
using BusinessLayer.Repositories;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestingBookingTableReservation;

public class BookingControllerTest
{
    private Mock<IBookingRepository> _bookingRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private Mock<ICustomerService> _customerServiceMock;
    private Mock<ISeatService> _seatServiceMock;

    private BookingController _bookingController;


    [SetUp]
    public void SetUp()
    {
   
        _bookingRepositoryMock = new Mock<IBookingRepository>();
        _mapperMock = new Mock<IMapper>();
        _customerServiceMock = new Mock<ICustomerService>();
        _seatServiceMock = new Mock<ISeatService>();
  

         _bookingController = new BookingController(_bookingRepositoryMock.Object, _mapperMock.Object, _customerServiceMock.Object , _seatServiceMock.Object);
    }

   

    [Test]
    public async Task CancelBooking_ShouldReturnNotFound_WhenBookingDoesNotExist()
    {
        // Arrange: Set up invalid booking ID
        _bookingRepositoryMock.Setup(repo => repo.GetById(1))
                              .ReturnsAsync((Booking)null);

        // Act
        var result = await _bookingController.CancelBooking(1);

        // Assert: Check if NotFound is returned
        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        var notFoundResult = result as NotFoundObjectResult;
        Assert.AreEqual("Booking not found", notFoundResult.Value);
    }

 

    [Test]
    public async Task CancelBooking_ReturnOk_WhenBookingIsCanceledSuccessfully()
    {
     
        var booking = new Booking { Id = 1, Status = BookingStatus.Confirmed };
        _bookingRepositoryMock.Setup(repo => repo.GetById(1))
                              .ReturnsAsync(booking);

        _bookingRepositoryMock.Setup(repo => repo.Update(booking))
                              .Returns(Task.CompletedTask);
     
        var result = await _bookingController.CancelBooking(1);

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
      
    }


    [Test]
    public async Task UpdateBooking_ReturnBadRequest_WhenBookingDtoIsNull()
    {
    
        var result = await _bookingController.UpdateBooking(1, null);

        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        var badRequestResult = result as BadRequestObjectResult;
        Assert.AreEqual("Invalid request.", badRequestResult.Value);
    }

    [Test]
    public async Task UpdateBooking_ReturnNotFound_WhenBookingNotFound()
    {
        
        var bookingDto = new BookingDto { CustomerId = 2 , SeatId=2 };
        _bookingRepositoryMock.Setup(repo => repo.GetById(2))
                              .ReturnsAsync((Booking)null);
       
        var result = await _bookingController.UpdateBooking(2, bookingDto);

        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        var notFoundResult = result as NotFoundObjectResult;
        Assert.AreEqual("Booking not found", notFoundResult.Value);
    }

    [Test]
    public async Task UpdateBooking_ReturnOk_WhenUpdatedSuccessfully()
    {
        
        var booking = new Booking { Id = 1, Status = BookingStatus.Confirmed };
        var bookingDto = new BookingDto { CustomerId =2 , SeatId= 2 };
        _bookingRepositoryMock.Setup(repo => repo.GetById(1))
                              .ReturnsAsync(booking);
        _bookingRepositoryMock.Setup(repo => repo.Update(It.IsAny<Booking>()))
                              .Returns(Task.CompletedTask); 

        var result = await _bookingController.UpdateBooking(1, bookingDto);

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
       
    }


    [Test]
    public async Task GetBookingById_ReturnNotFound_WhenBookingNotFound()
    {
      
        _bookingRepositoryMock.Setup(repo => repo.GetById(1))
                              .ReturnsAsync((Booking)null);

        var result = await _bookingController.GetBookingById(2);
   
        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
       
    }
    [Test]
    public async Task GetBookingById_ReturnOk_WhenBookingExists()
    {
      
        var booking = new Booking { Id = 1, Status = BookingStatus.Confirmed };
        var bookingDto = new BookingDto { CustomerId= 1 };
        _bookingRepositoryMock.Setup(repo => repo.GetById(1))
                              .ReturnsAsync(booking);
        _mapperMock.Setup(m => m.Map<BookingDto>(It.IsAny<Booking>()))
                   .Returns(bookingDto);

        var result = await _bookingController.GetBookingById(1);
     
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    
    }



    /////////////////
    ///

    [Test]
    public async Task Create_ReturnBadRequest_WhenBookingDataIsNull()
    {
       var result = await _bookingController.Create(null);

        Assert.IsInstanceOf<BadRequestObjectResult>(result);
      
    }

    [Test]
    public async Task Create_ReturnNotFound_WhenCustomerIsNotFound()
    {
        var bookingDto = new BookingDto { CustomerId = 1 };
        _customerServiceMock.Setup(service => service.GetCustomerByIdAsync(1)).ReturnsAsync((Customer)null);

        var result = await _bookingController.Create(bookingDto);

      
        Assert.IsInstanceOf<NotFoundObjectResult>(result);
     
    }

    [Test]
    public async Task Create_ReturnBadRequest_WhenSeatIsNotAvailable()
    {     
        var bookingDto = new BookingDto { SeatId = 1, BookingDateTime = DateTime.Now };
        _customerServiceMock.Setup(service => service.GetCustomerByIdAsync(It.IsAny<int>())).ReturnsAsync(new Customer());
        _seatServiceMock.Setup(service => service.IsSeatsAvailable(1, bookingDto.BookingDateTime, bookingDto.StartTime)).ReturnsAsync(false);
       
        var result = await _bookingController.Create(bookingDto);

        Assert.IsInstanceOf<BadRequestObjectResult>(result);
      
    }

    [Test]
    public async Task Create_ReturnOk_WhenBookingIsSuccessful()
    {
       
        var bookingDto = new BookingDto { SeatId = 1, CustomerId = 1 };
        var customer = new Customer { Id = 1 };
        var booking = new Booking { Id = 1 };
        var bookingDtoMapped = new Booking { Status = BookingStatus.Confirmed };

        _customerServiceMock.Setup(service => service.GetCustomerByIdAsync(1)).ReturnsAsync(customer);
        _seatServiceMock.Setup(service => service.IsSeatsAvailable(1, bookingDto.BookingDateTime, bookingDto.StartTime)).ReturnsAsync(true);
        _mapperMock.Setup(m => m.Map<Booking>(bookingDto)).Returns(bookingDtoMapped);
        _bookingRepositoryMock.Setup(repo => repo.CreatAsync(It.IsAny<Booking>())).ReturnsAsync(booking);

        var result = await _bookingController.Create(bookingDto);
  
        Assert.IsInstanceOf<OkObjectResult>(result);
       
    }

}




