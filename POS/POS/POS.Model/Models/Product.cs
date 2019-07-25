 
using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POS.Model.Models
{
    [Table("Product")]
    public class Product 
    {

        public Product()
        {
            PurchaseOrderDetails = new List<PurchaseOrderDetail>();
        }
        [Key]  
        public int Id { get; set; }

        [StringLength(25)] 
        public string UPC { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int Quantity { get; set; }

        public double PurchasePrice { get; set; }

        public double SalePrice { get; set; }

        public ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
    }
}
