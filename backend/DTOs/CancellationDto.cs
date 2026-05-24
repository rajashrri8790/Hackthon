using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTOs
{
    public class CancellationDto
    {
        [Required]
        public int BookingId { get; set; }

        public string? Reason { get; set; }
    }

}
