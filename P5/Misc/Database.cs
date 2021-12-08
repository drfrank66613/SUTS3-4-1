using Microsoft.EntityFrameworkCore;
using P4.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P4.Misc
{
    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> options) : base(options) {}
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketWithProduct> BasketWithProducts { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleWithProduct> SaleWithProducts { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(mb =>
            {
                mb.HasKey(e => e.ProdId);
                mb.HasData(new Product { ProdId = 1, ProdName = "Car", ProdPrice = 300.00 },
                new Product { ProdId = 2, ProdName = "Smartphone", ProdPrice = 99.99 },
                new Product { ProdId = 3, ProdName = "Watch", ProdPrice = 30.50 });
            });

            modelBuilder.Entity<Customer>(mb =>
            {
                mb.HasKey(e => e.CustId);
                mb.HasData(new Customer { CustId = 1, CustName = "Customer1", CustPassword = "123123" },
                new Customer { CustId = 2, CustName = "Customer2", CustPassword = "123123" },
                new Customer { CustId = 3, CustName = "Customer3", CustPassword = "123123" });
            });

            modelBuilder.Entity<Basket>(mb =>
            {
                mb.HasKey(b => b.BasketId);
                mb.HasOne(b => b.Customer).WithMany(c => c.Baskets).HasForeignKey(b => b.CustId);
            });


            modelBuilder.Entity<Sale>(mb =>
            {
                mb.HasKey(s => s.SaleId);
                mb.HasOne(s => s.Customer).WithMany(c => c.Sales).HasForeignKey(s => s.CustId);
            });


            modelBuilder.Entity<BasketWithProduct>(mb =>
            {
                mb.HasKey(bwp => new { bwp.BasketId, bwp.ProdId });
                mb.HasOne(bwp => bwp.Basket).WithMany(b => b.BasketWithProducts).HasForeignKey(bwp => bwp.BasketId);
                mb.HasOne(bwp => bwp.Product).WithMany(p => p.BasketWithProducts).HasForeignKey(bwp => bwp.ProdId);
            });


            modelBuilder.Entity<SaleWithProduct>(mb =>
            {
                mb.HasKey(swp => new { swp.SaleId, swp.ProdId });
                mb.HasOne(swp => swp.Sale).WithMany(s => s.SaleWithProducts).HasForeignKey(swp => swp.SaleId);
                mb.HasOne(swp => swp.Product).WithMany(p => p.SaleWithProducts).HasForeignKey(swp => swp.ProdId);
            });


            modelBuilder.Entity<Feedback>(mb =>
            {
                mb.HasKey(f => f.FeedId);
                mb.HasOne(f => f.Customer).WithMany(c => c.Feedbacks).HasForeignKey(f => f.CustId);
            });
        }
    }
}