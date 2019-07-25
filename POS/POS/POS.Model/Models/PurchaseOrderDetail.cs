using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace POS.Model.Models
{
    [Table("PurchaseOrderDetail")]
    public class PurchaseOrderDetail : OrderDetail
    {
        [ForeignKey(nameof(PurchaseOrder))]
        public int PurchaseOrderId { get; set; }

        public PurchaseOrder PurchaseOrder { get; set; }
    }
}
