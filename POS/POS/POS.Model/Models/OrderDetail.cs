using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace POS.Model.Models
{
     
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public double PurchasePrice { get; set; }

        public double SalePrice { get; set; } 

    }
}
