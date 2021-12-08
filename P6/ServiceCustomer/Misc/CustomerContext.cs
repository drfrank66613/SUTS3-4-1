using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceCustomer.Model;

namespace ServiceCustomer.Misc
{
    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(mb =>
            {
                mb.HasKey(e => e.CustId);
                mb.HasData(new Customer { CustId = 1, CustName = "Customer1", CustPassword = "123123" },
                    new Customer { CustId = 2, CustName = "Customer2", CustPassword = "123123" },
                    new Customer { CustId = 3, CustName = "Customer3", CustPassword = "123123" });
            });
        }

    }
}
