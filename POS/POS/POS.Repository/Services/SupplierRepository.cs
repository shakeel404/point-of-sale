using POS.Db.Db;
using POS.Model.Models;
using POS.Repository.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Repository.Services
{
    public class SupplierRepository:Repository<Supplier>,ISupplierRepository
    {
        public SupplierRepository(PosContext context):base(context)
        {

        }
    }
}
