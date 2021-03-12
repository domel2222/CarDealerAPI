using CarDealerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealerAPI.Contexts
{
    public class DealerDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public DealerDbContext(DbContextOptions option, IConfiguration config) : base(option)
        {
            this._config = config;
        }
        
        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Address> Adresses {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dealer>()
                .Property(d => d.DealerName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Car>()
                .Property(c => c.NameMark)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Address>()
                .Property(a => a.City)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Address>()
                .Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(100);



            //No type was specified for the decimal property 'Price' on entity type 'Car'. 
            //This will cause values to be silently truncated if they do not fit in the default precision and scale. 
              //  Explicitly specify the SQL server column type that can accommodate all the values in 'OnModelCreating' using 'HasColumnType()', 
                //specify precision and scale using 'HasPrecision()' 
            //or configure a value converter using 'HasConversion()'
        }

        protected   override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("DealersCar"), builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
        }
    }
}
