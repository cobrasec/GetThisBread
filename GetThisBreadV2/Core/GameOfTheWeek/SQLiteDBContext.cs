using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GetThisBreadV2.Core.GameOfTheWeek
{
    public class SQLiteDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder Options)
        {
            string DbLocation = Assembly.GetEntryAssembly().Location.Replace(@"bin\Debug\netcoreapp2.1", @"Data\Database.sqlite");

        }
       

        
    } 
           
        
    
}
