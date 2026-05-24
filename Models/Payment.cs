using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BookingId { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        public decimal Amount { get; set; }

        public string PaymentStatus { get; set; } = "Pending";

        public string? TransactionId { get; set; }

        public DateTime? PaidAt { get; set; }

        [ForeignKey("BookingId")]
        public Booking? Booking { get; set; }
    }
}
