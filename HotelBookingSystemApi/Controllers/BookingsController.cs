using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace HotelBookingSystemApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/bookings")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _service;

        public BookingsController(IBookingService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult CreateBooking(BookingDto dto)
        {
            return Ok(_service.CreateBooking(dto));
        }

        [HttpGet("{id}")]
        public IActionResult GetBooking(int id)
        {
            var booking = _service.GetBookingById(id);

            if (booking == null)
                return NotFound("Booking not found");

            return Ok(booking);
        }

        [HttpGet("customer/{customerId}")]
        public IActionResult GetCustomerBookings(int customerId)
        {
            return Ok(_service.GetBookingsByCustomer(customerId));
        }

        [HttpPut("cancel")]
        public IActionResult CancelBooking(CancellationDto dto)
        {
            return Ok(_service.CancelBooking(dto));
        }
    }
}
