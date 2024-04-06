using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApp.Entities;

namespace ShopApp.DataAccess.Concrete.EFCore
{
    public class SeedDatabase
    {
        public static void Seed(){
            var context =new ShopContext();
            if(context.Database.GetPendingMigrations().Count()==0){
                //if there is no migration do this process.
                if(context.Categories.Count()==0){
                    context.Categories.AddRange(Categories);
                }
                if(context.Products.Count()==0){
                    context.Products.AddRange(Products);
                }
            }
            context.SaveChanges();
        }
        private static Category[] Categories =  {
            new Category(){Name="Telefon"},
            new Category(){Name="Bilgisayar"},
            
        };
        private static Product[] Products =  {
            new Product(){Name="Samsung S5",Price=2000,ImageUrl="1.jpg"},
            new Product(){Name="Samsung S6",Price=3000,ImageUrl="1.jpg"},
            new Product(){Name="Samsung S7",Price=4000,ImageUrl="1.jpg"},
            new Product(){Name="Samsung S8",Price=5000,ImageUrl="1.jpg"},

            
        };
    }
}