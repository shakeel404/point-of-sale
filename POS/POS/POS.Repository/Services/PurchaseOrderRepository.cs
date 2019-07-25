using POS.Db.Db;
using POS.Model.Models;
using POS.Repository.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Repository.Services
{
    public class PurchaseOrderRepository: Repository<PurchaseOrder>, IPurchaseOrderRepository
    {
        public PurchaseOrderRepository(PosContext context):base(context)
        {

        }
    }
}
