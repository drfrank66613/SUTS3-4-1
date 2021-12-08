using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceFeedback.Model;

namespace ServiceFeedback.Misc
{
    public class FeedbackContext : DbContext
    {
        public DbSet<Feedback> Feedbacks { get; set; }

        public FeedbackContext(DbContextOptions<FeedbackContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Feedback>(mb =>
            {
                mb.HasKey(f => f.FeedId);
            });
        }

    }
}
