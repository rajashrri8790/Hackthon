using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTOs
{
    public class PaymentDto
    {
        [Required]
        public int BookingId { get; set; }

        [Required]
        public string PaymentMethod { get; set; }
    }


}
