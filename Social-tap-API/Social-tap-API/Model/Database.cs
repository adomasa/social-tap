﻿using Microsoft.EntityFrameworkCore;
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
            .ToTable("Review");


            modelBuilder.Entity<Bar>()
            .HasKey(c => c.Id);
            modelBuilder.Entity<Bar>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();
            modelBuilder.Entity<Bar>()
            .ToTable("Bar");

             modelBuilder.Entity<Review>()
             .HasOne(p => p.Bar)
             .WithMany(b => b.Reviews)
             .HasForeignKey(p => p.Id) //  
             .HasConstraintName("ForeignKey_Review_Bar"); 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source= Review.db");
            //optionsBuilder.UseSqlServer("Data Source = valentinas-pc.model.dbo");
        }

    }



}
