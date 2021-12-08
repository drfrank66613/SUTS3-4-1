using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceBasket.Model;

namespace ServiceBasket.Misc
{
    public class BasketContext : DbContext
    {
        public DbSet<Basket> Baskets { get; set; }

        public BasketContext(DbContextOptions<BasketContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Basket>(mb =>
            {
                mb.HasKey(b => b.BasketId);
            });
        }

    }
}
