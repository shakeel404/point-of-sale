
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations; 

namespace POS.Model.Models
{
    [Table("Supplier")]
    public class Supplier
    {
        public Supplier()
        {
            PurchaseOrders = new List<PurchaseOrder>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Contact { get; set; }

        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
