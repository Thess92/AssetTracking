using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracking
{
    internal class MyDbContext : DbContext
    {
        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Assets;Trusted_Connection=True;MultipleActiveResultSets=true";

        public DbSet<Asset> Assets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            ModelBuilder.Entity<Asset>().HasData(new Asset { Id = 1, Type = "Phone", Brand = "Apple", Model = "Iphone 13", PurchaseDate = new DateTime(2022, 06, 30), Price = 1300, Country = "USA" });
            ModelBuilder.Entity<Asset>().HasData(new Asset { Id = 2, Type = "Computer", Brand = "Lenovo", Model = "Legion", PurchaseDate = new DateTime(2019, 02, 20), Price = 2000, Country = "Sweden" });
            ModelBuilder.Entity<Asset>().HasData(new Asset { Id = 3, Type = "Phone", Brand = "Samsung", Model = "Galaxy S20", PurchaseDate = new DateTime(2020, 04, 05), Price = 1200, Country = "Spain" });
        }
    }
}