using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class InvoiceItem
    {
        [Key]
        public int InvoiceItemId {  get; set; }

        [ForeignKey("InvoiceId")]
        public int? InvoiceId { get; set; }

        public Invoice? Invoice { get; set; }

        [ForeignKey("ProductId")]
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public int ? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? SubTotal { get; set; } 
    }
}
