﻿using FARApplication.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace FARApplication.Data
{
    public class FARContext : DbContext
    {
        public DbSet<FAR> FARs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FAREventLog> FAREventLogs { get; set; }
        public DbSet<FARApprover> FARApprovers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();
            var strConnectionString = configuration.GetConnectionString("FARMySqlDatabase");
            optionsBuilder.UseMySQL(strConnectionString);
            
        }


    }
}
