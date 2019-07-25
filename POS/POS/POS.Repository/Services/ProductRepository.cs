using Microsoft.EntityFrameworkCore;
using POS.Db.Db;
using POS.Model.Models;
using POS.Repository.IServices;
using System;
using System.Collections.Generic;
using System.Text;


namespace POS.Repository.Services
{
    public class ProductRepository:Repository<Product>,IProductRepository
    {

        public ProductRepository(PosContext context):base(context)
        {

        }
    }
}
