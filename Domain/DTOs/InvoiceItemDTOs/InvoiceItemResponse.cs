using Domain.DTOs.ProductDTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.InvoiceItemDTOs
{
    public class InvoiceItemResponse
    {
        public int? InvoiceItemId { get; set; }
        public int? InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }
        public int? ProductId { get; set; }
        public ProductResponse? ProductResponse { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? SubTotal { get; set; }
    }
}
