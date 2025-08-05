using AutoMapper;
using BookTableReservation.Controllers;
using Domain.Entities;
using BookTableReservation.Models;
using BusinessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestingBookingTableReservation;

public class SeatControllerTest
{


    private Mock<ISeatRepository> _seatRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private SeatController _controller;

    [SetUp]
    public void SetUp()
    {
      
        _seatRepositoryMock = new Mock<ISeatRepository>();
        _mapperMock = new Mock<IMapper>();

        _controller = new SeatController(_seatRepositoryMock.Object, _mapperMock.Object);
    }


    [Test]
    public async Task GetAvailableSeats_ReturnNotFound_WhenNoSeatsAvailable()
    {

        var desiredDateTime = DateTime.Now;
        var desiredStartTime = "10:30";
        var startTime = TimeSpan.Parse(desiredStartTime);
        _seatRepositoryMock.Setup(repo => repo.GetAvailableSeats(desiredDateTime, startTime))
            .ReturnsAsync(new List<Seat>());

        var result = await _controller.GetAvailableSeats(desiredDateTime, desiredStartTime);

        Assert.IsInstanceOf<NotFoundResult>(result);
    }


    [Test]
    public async Task GetAvailableSeats_ReturnBadRequest_WhenStartTimeIsInvalid()
    {

        var desiredDateTime = DateTime.Now;
        var invalidStartTime = "Test";

        var result = await _controller.GetAvailableSeats(desiredDateTime, invalidStartTime);

        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        var badRequestResult = result as BadRequestObjectResult;
        Assert.AreEqual("Invalid start time format.", badRequestResult.Value);
    }


    [Test]
    public async Task GetAvailableSeats_ReturnOk_WhenSeatsAreAvailable()
    {
      
        var desiredDateTime = DateTime.Now;
        var desiredStartTime = "9:30";
        var startTime = TimeSpan.Parse(desiredStartTime);

        var availableSeats = new List<Seat>
        {
            new Seat { Id = 1, SeatNumber = 1 },
            new Seat { Id = 2, SeatNumber = 2 }
        };
        _seatRepositoryMock.Setup(repo => repo.GetAvailableSeats(desiredDateTime, startTime))
            .ReturnsAsync(availableSeats);

        var seatsDto = new List<SeatsDto>
        {
            new SeatsDto { Id = 1, SeatNumber = 1 },
            new SeatsDto { Id = 2, SeatNumber = 2}
        };
        _mapperMock.Setup(m => m.Map<List<SeatsDto>>(It.IsAny<List<Seat>>()))
            .Returns(seatsDto);

        var result = await _controller.GetAvailableSeats(desiredDateTime, desiredStartTime);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.AreEqual(seatsDto, okResult.Value);
    }
}
