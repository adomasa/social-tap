using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using SocialtapAPI;
using Social_Tap_Api;

namespace Social_tap_API
{
    public class DatabaseContext:DbContext 
    {
        public DbSet<Review> ReviewSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
            .HasKey(c => c.BarId);

            modelBuilder.Entity<Review>()
            .Property(f => f.BarId)
            .ValueGeneratedOnAdd();
            modelBuilder.Entity<Review>()
            .ToTable("Reviews");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source= Review.db");
        }

    }



}
