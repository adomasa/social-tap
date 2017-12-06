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

            modelBuilder.Entity<Bar>()
            .HasOne(p => p.Review)
            .WithOne(b => b.Bar)
            .HasForeignKey(p =>p.) // kodel čia nieko nesiūlo?? 
            .HasConstraintName("ForeignKey_Post_Blog");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source= Review.db");
        }

    }



}
