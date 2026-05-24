using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Cancellation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BookingId { get; set; }

        public string? Reason { get; set; }

        public decimal RefundAmount { get; set; }

        public string CancellationStatus { get; set; } = "Requested";

        public DateTime CancelledAt { get; set; } = DateTime.Now;

        [ForeignKey("BookingId")]
        public Booking? Booking { get; set; }
    }
}
