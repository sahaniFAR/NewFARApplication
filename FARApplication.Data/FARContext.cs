using FARApplication.Data;
using FARApplication.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;


namespace FARApplication.Data
{
   public  class FARContext : DbContext
    {
        public DbSet<FAR> FARs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FAREventLog> FAREventLogs { get; set; }
        public DbSet<FARApprover> FARApprovers { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder OptonsBuilder)
        {
           
           // base.OnConfiguring(OptonsBuilder);
            //OptonsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FARDB;Integrated Security=True;Connect Timeout=30;MultipleActiveResultSets = true");
          IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
          .AddJsonFile("appsettings.json")
          .Build();
            var strConnectionString = configuration.GetConnectionString("FARDatabase");
            OptonsBuilder.UseSqlServer(strConnectionString);
        }
    }
}
