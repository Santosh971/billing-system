using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public  class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProductName {  get; set; }   
        public Decimal? Price { get; set; }
        public int? StockQuantity { get; set; }
        public string? Description {  get; set; }   


    }
}
