using DTOs;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IBookingService
    {
        string CreateBooking(BookingDto dto);
        Booking? GetBookingById(int id);
        List<Booking> GetBookingsByCustomer(int customerId);
        string CancelBooking(CancellationDto dto);
    }
}
