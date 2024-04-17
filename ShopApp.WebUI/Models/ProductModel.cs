using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Entities;

namespace ShopApp.WebUI.Models
{
    public class ProductModel
    {
             public int Id { get; set; }
        public string ?Name { get; set; }
        public string ?ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<Category> SelectedCategories { get; set; }
    }
}