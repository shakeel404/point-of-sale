using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POS.Model.Models
{
    [Table("PurchasePayment")]
    public class PurchsePayment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(PurchaseOrder))]
        public int PurchaseOrderId { get; set; }

        public int Amount { get; set; }

        public PurchaseOrder PurchaseOrder { get; set; }

        public DateTime? Date { get; set; } = DateTime.Now; 
    }
}
