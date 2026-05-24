using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTOs
{

    public class RoomDto
    {
        [Required]
        public string RoomNumber { get; set; }

        [Required]
        public string RoomType { get; set; }

        [Required]
        public decimal PricePerNight { get; set; }

        [Required]
        public int Capacity { get; set; }

        public string? Amenities { get; set; }
    }
}
