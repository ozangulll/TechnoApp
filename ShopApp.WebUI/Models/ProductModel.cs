using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Entities;

namespace ShopApp.WebUI.Models
{
    public class ProductModel
    {
    public int Id { get; set; }

[Required(ErrorMessage = "The Name field is required.")]
[StringLength(64, MinimumLength = 10, ErrorMessage = "The Name must be between 10 and 64 characters long.")]
public string? Name { get; set; }

[Required(ErrorMessage = "The ImageUrl field is required.")]
public string? ImageUrl { get; set; }   

[Required(ErrorMessage = "The Description field is required.")]
public string Description { get; set; }

[Required(ErrorMessage = "The Price field is required.")]
public decimal? Price { get; set; }

public List<ProductCategory> ProductCategories { get; set; }
public List<Category> SelectedCategories { get; set; }

    }
}