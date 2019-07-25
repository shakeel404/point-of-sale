using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace POS.Model.Models
{
    [Table("PurchaseOrder")]
    public class PurchaseOrder
    {
        public PurchaseOrder()
        {
            OrderDetails = new List<PurchaseOrderDetail>();
            Payments = new List<PurchsePayment>();
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Supplier))]
        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; }

        public DateTime? OrderDate { get; set; } = DateTime.Now; 

        public ICollection<PurchaseOrderDetail> OrderDetails { get; set; }
        public ICollection<PurchsePayment> Payments { get; set; }
    }
}
