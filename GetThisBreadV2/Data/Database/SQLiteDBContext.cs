using GetThisBreadV2.Core.Currency;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GetThisBreadV2.Data.Database
{
    public class SQLiteDBContext : DbContext
    {
        public DbSet<Coin> BreadCoin { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder Options)
        {
            string DbLocation = Assembly.GetEntryAssembly().Location.Replace(@"bin\Debug\netcoreapp2.1", @"Data\");
            Options.UseSqlite($"Data Source={DbLocation}Database.sqlite");
        }
       

        
    } 
           
        
    
}
