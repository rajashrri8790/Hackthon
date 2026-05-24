using Data;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.Interfaces;

namespace Services
{
    public class BookingService : IBookingService
    {
        private readonly AppDbContext _context;

        public BookingService(AppDbContext context)
        {
            _context = context;
        }

        public string CreateBooking(BookingDto dto)
        {
            if (dto.CheckOutDate <= dto.CheckInDate)
                return "Check-out date must be after check-in date";

            var customer = _context.Customers.FirstOrDefault(x => x.Id == dto.CustomerId);
            if (customer == null)
                return "Customer not found";

            var room = _context.Rooms.FirstOrDefault(x => x.Id == dto.RoomId);
            if (room == null)
                return "Room not found";

            if (dto.NumberOfGuests > room.Capacity)
                return "Guest count exceeds room capacity";

            var isBooked = _context.Bookings.Any(b =>
                b.RoomId == dto.RoomId &&
                b.BookingStatus != "Cancelled" &&
                dto.CheckInDate < b.CheckOutDate &&
                dto.CheckOutDate > b.CheckInDate
            );

            if (isBooked)
                return "Room already booked for selected dates";

            int days = (dto.CheckOutDate - dto.CheckInDate).Days;

            var booking = new Booking
            {
                CustomerId = dto.CustomerId,
                RoomId = dto.RoomId,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                NumberOfGuests = dto.NumberOfGuests,
                TotalAmount = days * room.PricePerNight,
                BookingStatus = "Pending"
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return "Booking created successfully";
        }

        public Booking? GetBookingById(int id)
        {
            return _context.Bookings
                .Include(x => x.Customer)
                .Include(x => x.Room)
                .Include(x => x.Payment)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Booking> GetBookingsByCustomer(int customerId)
        {
            return _context.Bookings
                .Include(x => x.Room)
                .Where(x => x.CustomerId == customerId)
                .ToList();
        }

        public string CancelBooking(CancellationDto dto)
        {
            var booking = _context.Bookings.FirstOrDefault(x => x.Id == dto.BookingId);

            if (booking == null)
                return "Booking not found";

            if (booking.BookingStatus == "Cancelled")
                return "Booking already cancelled";

            booking.BookingStatus = "Cancelled";

            var refund = booking.TotalAmount * 0.8m;

            var cancellation = new Cancellation
            {
                BookingId = dto.BookingId,
                Reason = dto.Reason,
                RefundAmount = refund,
                CancellationStatus = "Approved"
            };

            _context.Cancellations.Add(cancellation);
            _context.SaveChanges();

            return "Booking cancelled successfully";
        }
    }
}