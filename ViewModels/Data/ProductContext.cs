using Microsoft.EntityFrameworkCore;
using PCPartPriceTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCPartPriceTracker.Data
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=C:\Users\vivek\source\repos\PCPartPriceTracker\ViewModels\products.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
