using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class RestaurantContext : DbContext
    {
        private string connexionString { get; set; }
        //public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Note> Grades { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public RestaurantContext()
        {
            //string pat = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()).ToString()) + @"\WebApplication1\bin\Debug\netcoreapp3.0\myDb3.db";
            //this.connexionString = @"Data Source=D:\NET\WebApplication1\bin\Debug\netcoreapp3.0\myDb3.db"; //le fichier est dans D:\NET\TP\Hello world\App.data.test\bin\Debug\netcoreapp3.0\myDb3.db
        }
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlite(this.connexionString);

            base.OnConfiguring(optionsBuilder);
            var path = Directory.GetParent(Directory.GetCurrentDirectory());
            Assembly? assembly = Assembly.GetEntryAssembly();
            string assemblyMessage = assembly.ToString();

            if (assemblyMessage.Contains("testhost"))
            {
                optionsBuilder.UseSqlite(@"Data Source=" + path + @"\netcoreapp3.0\myDb3.db");
            }
            else
            {
                optionsBuilder.UseSqlite(@"Data Source=" + path + @"\WebApplication1\bin\Debug\netcoreapp3.0\myDb3.db");
            }
        }
    }
}
