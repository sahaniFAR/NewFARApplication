using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;

namespace FARApplication.Data
{
    public class FARMySqlContext : FARContext
    {
        
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
