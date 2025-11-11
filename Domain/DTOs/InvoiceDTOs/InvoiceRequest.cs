using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.InvoiceDTOs
{
    public class InvoiceRequest
    {
        public int InvoiceId { get; set; }

        [ForeignKey("UserId")]
        public int? UserId { get; set; }

        public User? User { get; set; }

        public DateTime? CreatedAt { get; set; }

        public decimal? TotalAmount { get; set; }

        public string? PaymentMode { get; set; }

        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
    }
}
