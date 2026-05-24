using Data;
using DTOs;
using Models;
using Services.Interfaces;

namespace Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        public string ProcessPayment(PaymentDto dto)
        {
            var booking = _context.Bookings.FirstOrDefault(x => x.Id == dto.BookingId);

            if (booking == null)
                return "Booking not found";

            if (booking.BookingStatus == "Confirmed")
                return "Payment already completed";

            var payment = new Payment
            {
                BookingId = dto.BookingId,
                PaymentMethod = dto.PaymentMethod,
                Amount = booking.TotalAmount,
                PaymentStatus = "Success",
                TransactionId = Guid.NewGuid().ToString(),
                PaidAt = DateTime.Now
            };

            booking.BookingStatus = "Confirmed";

            _context.Payments.Add(payment);
            _context.SaveChanges();

            return "Payment successful and booking confirmed";
        }
    }
}