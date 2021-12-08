using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceSale.Model;

namespace ServiceSale.Misc
{
    public class SaleContext : DbContext
    {
        public DbSet<Sale> Sales { get; set; }

        public SaleContext(DbContextOptions<SaleContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sale>(mb =>
            {
                mb.HasKey(s => s.SaleId);
            });
        }

    }
}
