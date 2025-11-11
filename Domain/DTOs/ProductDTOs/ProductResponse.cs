using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ProductDTOs
{
    public class ProductResponse
    {
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public Decimal? Price { get; set; }
        public int? StockQuantity { get; set; }
        public string? Description { get; set; }

    }
}
