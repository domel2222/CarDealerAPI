﻿using CarDealerAPI.Models;
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
        // add factory context
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
        }

        protected   override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString)
        }
    }
}