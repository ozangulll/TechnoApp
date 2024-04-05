using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApp.Entities;

namespace ShopApp.DataAccess.Concrete.EFCore
{
    public class ShopContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-30HS78PS\SQLEXPRESS;Database=eCommerceDB;integrated security=true;");
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}