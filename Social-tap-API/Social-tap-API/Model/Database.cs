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
        public DbSet<Bar> BarSet { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
            .HasKey(c => c.Id);
            modelBuilder.Entity<Review>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();
            modelBuilder.Entity<Review>()
            .ToTable("Reviews");
            modelBuilder.Entity<Bar>()
            .ToTable("Bar");
            modelBuilder.Entity<Bar>()
            .HasKey(c => c.Id);

            modelBuilder.Entity<Review>()
            .HasOne(p => p.Bar)
            .WithMany(b => b.Reviews)
            .HasForeignKey(p => p.Id) //  
            .HasConstraintName("ForeignKey_Review_Bar");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source= Review.db");
        }

    }



}
