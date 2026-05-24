using Data;
using DTOs;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace BDDTests.StepDefinitions
{
    [Binding]
    public class BookingSteps
    {
        private AppDbContext _context;
        private BookingService _service;
        private BookingDto _bookingDto;
        private string _result;

        [Given(@"a customer exists")]
        public void GivenACustomerExists()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);

            _context.Customers.Add(new Customer
            {
                Id = 1,
                FullName = "Anusha",
                Email = "anusha@gmail.com",
                PhoneNumber = "9876543210",
                PasswordHash = "test",
                Role = "Customer"
            });

            _context.SaveChanges();

            _service = new BookingService(_context);
        }

        [Given(@"a deluxe room is available")]
        public void GivenADeluxeRoomIsAvailable()
        {
            _context.Rooms.Add(new Room
            {
                Id = 1,
                RoomNumber = "101",
                RoomType = "Deluxe",
                PricePerNight = 3500,
                Capacity = 2,
                Amenities = "WiFi, AC",
                IsAvailable = true
            });

            _context.SaveChanges();
        }

        [When(@"the customer books the room")]
        public void WhenTheCustomerBooksTheRoom()
        {
            _bookingDto = new BookingDto
            {
                CustomerId = 1,
                RoomId = 1,
                CheckInDate = DateTime.Now.AddDays(1),
                CheckOutDate = DateTime.Now.AddDays(3),
                NumberOfGuests = 2
            };

            _result = _service.CreateBooking(_bookingDto);
        }

        [Then(@"the booking should be created successfully")]
        public void ThenTheBookingShouldBeCreatedSuccessfully()
        {
            _result.Should().Be("Booking created successfully");
        }

        [When(@"the customer enters checkout date before checkin date")]
        public void WhenTheCustomerEntersCheckoutDateBeforeCheckinDate()
        {
            _bookingDto = new BookingDto
            {
                CustomerId = 1,
                RoomId = 1,
                CheckInDate = DateTime.Now.AddDays(3),
                CheckOutDate = DateTime.Now.AddDays(1),
                NumberOfGuests = 2
            };

            _result = _service.CreateBooking(_bookingDto);
        }

        [Then(@"the booking should fail with date validation message")]
        public void ThenTheBookingShouldFailWithDateValidationMessage()
        {
            _result.Should().Be("Check-out date must be after check-in date");
        }
    }
}
