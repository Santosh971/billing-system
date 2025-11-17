using Domain.DTOs.InvoiceItemDTOs;
using Domain.DTOs.UserDTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.InvoiceDTOs
{
    public class InvoiceResponse
    {
        public int InvoiceId { get; set; }

        [ForeignKey("UserId")]
        public int? UserId { get; set; }

        public UserResponse? UserResponse { get; set; }

        public DateTime? CreatedAt { get; set; }

        public decimal? TotalAmount { get; set; }

        public string? PaymentMode { get; set; }

        public List<InvoiceItemResponse>? InvoiceItemResponses { get; set; } 
    }
}
