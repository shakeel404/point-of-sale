using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using POS.Model.Models;

namespace POS.Db.Db
{
    public class PosContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=POSDataBase;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>(e => { e.HasIndex(i => i.UPC).IsUnique(); });

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        public DbSet<PurchsePayment> PurchsePayments { get; set; }

    }
}
