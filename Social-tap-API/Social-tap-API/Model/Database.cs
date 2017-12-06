using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using SocialtapAPI;

namespace Social_tap_API
{
    public class DatabaseContext:DbContext 
    {
        public DbSet<Dictionary<string,IBarData>> BarDataSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BarData>()
                .Ignore(b => b.Tags);
            modelBuilder.Entity<BarData>()
                .Ignore(b => b.Comparison);
            modelBuilder.Entity<Dictionary<string, IBarData>>()
            .HasKey(c => new { c.Keys });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=bardata.db");
        }

    }



}
