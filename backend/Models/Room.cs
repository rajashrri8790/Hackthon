using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RoomNumber { get; set; }

        [Required]
        public string RoomType { get; set; }

        [Required]
        public decimal PricePerNight { get; set; }

        [Required]
        public int Capacity { get; set; }

        public string? Amenities { get; set; }

        public bool IsAvailable { get; set; } = true;

        public ICollection<Booking>? Bookings { get; set; }
    }
}
