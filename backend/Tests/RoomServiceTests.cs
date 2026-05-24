using Data;
using DTOs;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    public class RoomServiceTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public void AddRoom_ShouldReturnSuccess_WhenRoomIsValid()
        {
            var context = GetDbContext();
            var service = new RoomService(context);

            var dto = new RoomDto
            {
                RoomNumber = "101",
                RoomType = "Deluxe",
                PricePerNight = 3500,
                Capacity = 2,
                Amenities = "WiFi, AC"
            };

            var result = service.AddRoom(dto);

            result.Should().Be("Room added successfully");
        }

        [Fact]
        public void AddRoom_ShouldFail_WhenRoomNumberAlreadyExists()
        {
            var context = GetDbContext();
            var service = new RoomService(context);

            var dto = new RoomDto
            {
                RoomNumber = "101",
                RoomType = "Deluxe",
                PricePerNight = 3500,
                Capacity = 2,
                Amenities = "WiFi, AC"
            };

            service.AddRoom(dto);
            var result = service.AddRoom(dto);

            result.Should().Be("Room number already exists");
        }
    }
}
