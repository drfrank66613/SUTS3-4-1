using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using P2Server.Misc;
using P2Server.Model;
using Microsoft.EntityFrameworkCore;

namespace P2Server.Misc
{
    // public interface IDatabase
    // {
    //     public List<Product> Get();
    // }

    public class Database : DbContext
    {
        // Change this according to previous task
        // Make sure you use services.AddSingleton<Database>(); in startup.cs
        // Singleton class / purely static class = not the best practise

        public Database(DbContextOptions<Database> options) : base(options) { }
        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(Get());
            base.OnModelCreating(modelBuilder);
        }

        public List<Product> Get()
        {
            return new List<Product>() {
                new Product{Id = 1, Name = "Car", Price = 100, Description = "The fastest car", Image = "Images/car.jpg" },
                new Product{Id = 2, Name = "PC Gaming", Price = 50.25, Description = "High quality PC gaming", Image = "Images/pc.jpg" },
                new Product{Id = 3, Name = "Smartphone", Price = 30.15, Description = "The smartest phone ever", Image = "Images/phone.jpg" } };
        }
    }
}
