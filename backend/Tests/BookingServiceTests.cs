using Castle.Core.Resource;
using Data;
using DTOs;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    public class BookingServiceTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            context.Customers.Add(new Customer
            {
                Id = 1,
                FullName = "Anusha",
                Email = "anusha@gmail.com",
                PhoneNumber = "9876543210",
                PasswordHash = "test",
                Role = "Customer"
            });

            context.Rooms.Add(new Room
            {
                Id = 1,
                RoomNumber = "101",
                RoomType = "Deluxe",
                PricePerNight = 3500,
                Capacity = 2,
                Amenities = "WiFi, AC",
                IsAvailable = true
            });

            context.SaveChanges();
            return context;
        }

        [Fact]
        public void CreateBooking_ShouldReturnSuccess_WhenValidDataGiven()
        {
            var context = GetDbContext();
            var service = new BookingService(context);

            var dto = new BookingDto
            {
                CustomerId = 1,
                RoomId = 1,
                CheckInDate = DateTime.Now.AddDays(1),
                CheckOutDate = DateTime.Now.AddDays(3),
                NumberOfGuests = 2
            };

            var result = service.CreateBooking(dto);

            result.Should().Be("Booking created successfully");
        }

        [Fact]
        public void CreateBooking_ShouldFail_WhenCheckoutBeforeCheckin()
        {
            var context = GetDbContext();
            var service = new BookingService(context);

            var dto = new BookingDto
            {
                CustomerId = 1,
                RoomId = 1,
                CheckInDate = DateTime.Now.AddDays(3),
                CheckOutDate = DateTime.Now.AddDays(1),
                NumberOfGuests = 2
            };

            var result = service.CreateBooking(dto);

            result.Should().Be("Check-out date must be after check-in date");
        }

        [Fact]
        public void CreateBooking_ShouldFail_WhenGuestCountExceedsCapacity()
        {
            var context = GetDbContext();
            var service = new BookingService(context);

            var dto = new BookingDto
            {
                CustomerId = 1,
                RoomId = 1,
                CheckInDate = DateTime.Now.AddDays(1),
                CheckOutDate = DateTime.Now.AddDays(2),
                NumberOfGuests = 5
            };

            var result = service.CreateBooking(dto);

            result.Should().Be("Guest count exceeds room capacity");
        }
    }

}
