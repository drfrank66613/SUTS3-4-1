using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceProduct.Model;

namespace ServiceProduct.Misc
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }

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
        }

    }
}
