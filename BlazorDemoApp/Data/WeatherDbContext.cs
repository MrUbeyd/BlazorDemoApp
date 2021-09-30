using BlazorDemoApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorDemoApp.Data
{
    public class WeatherDbContext : DbContext
    {
        public virtual DbSet<WeatherData> weatherData { get; set; }
        public WeatherDbContext()
        {
            
        }

        public WeatherDbContext(DbContextOptions<WeatherDbContext> options)
            :base (options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(@"Data Source=MSI\\SQLEXPRESS;Initial Catalog=BlazorDemoDB;Integrated Security=True;Trusted_Connection=True;");

                optionsBuilder.UseSqlServer(@"Data Source=MSI\\SQLEXPRESS;Initial Catalog=BlazorDemoDB;Trusted_Connection=True;");

            }
            
        }
    }
}
