using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FARApplication.Data
{
   public  class FARContext : DbContext
    {
        public DbSet<FAR> FARs { get; set; }

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
