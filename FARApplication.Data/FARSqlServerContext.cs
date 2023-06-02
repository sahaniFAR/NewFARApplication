using FARApplication.Data;
using FARApplication.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;


namespace FARApplication.Data
{
   public  class FARSqlServerContext : FARContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder OptonsBuilder)
        {
             
          IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
          .AddJsonFile("appsettings.json")
          .Build();
            var strConnectionString = configuration.GetConnectionString("FARSqlDatabase");
            OptonsBuilder.UseSqlServer(strConnectionString);
            
        }
    }
}
