using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShopApp.Business.Abstract;
using ShopApp.Entities;
using ShopApp.WebUI.Models;

namespace ShopApp.WebUI.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;



        public AdminController(IProductService productService,ICategoryService categoryService)
        {
            _productService = productService;
             _categoryService = categoryService;
        }


        public IActionResult ProductList()
        {
       
            return View(new ProductListModel(){
                Products=_productService.GetAll()
            });
        }
        public IActionResult CreateProduct()
{
    var model = new ProductModel(); // ProductModel nesnesini oluştur
    ViewBag.Categories = _categoryService.GetAll(); // Kategorileri ViewBag'e ata
    return View(model); // View'e modeli gönder
}


   [HttpPost]
public async Task<IActionResult> CreateProduct(ProductModel model, int[] categoryIds, IFormFile file)
{
    if (ModelState.IsValid)
    {
        try
        {
            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var entity = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    ImageUrl = fileName,
                    Price = model.Price,
                    // Diğer özellikler
                };

                _productService.CreateWithCategories(entity, categoryIds);
                
                // Ürün oluşturulduktan sonra başka bir sayfaya yönlendir
                return RedirectToAction("ProductList", "Admin"); // Örnek bir yönlendirme
            }
            else
            {
                ModelState.AddModelError("", "Please select a file to upload.");
            }
        }
        catch (Exception ex)
        {
            // Log the exception
            ModelState.AddModelError("", $"An error occurred while processing your request: {ex.Message}");
        }
    }

    // If ModelState is invalid or an error occurs, reload the categories and return the view with the model
    ViewBag.Categories = _categoryService.GetAll();
    return View(model); 
}

   public IActionResult EditProduct(int? id)
{
    if (id == null)
    {
        return NotFound();
    }
    var entity = _productService.GetByIdWithCategories((int)id);

    if (entity == null)
    {
        return NotFound();
    }

    var model = new ProductModel()
    {
        Id = entity.Id,
        Name = entity.Name,
        Price = entity.Price,
        Description = entity.Description,
        ImageUrl = entity.ImageUrl,
        SelectedCategories = entity.ProductCategories.Select(i => i.Category).ToList()
    };

    ViewBag.Categories = _categoryService.GetAll();

    return View(model);
}

[HttpPost]
public async Task<IActionResult> EditProduct(ProductModel model, int[] categoryIds, IFormFile file)
{
    if (ModelState.IsValid)
    {
        var entity = _productService.GetById(model.Id);

        if (entity == null)
        {
            return NotFound();
        }

        entity.Name = model.Name;
        entity.Description = model.Description;               
        entity.Price = model.Price;

        if (file != null)
        {
            entity.ImageUrl = file.FileName;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        // Kategori ilişkilerini güncelle
        _productService.Update(entity, categoryIds);

        return RedirectToAction("ProductList");
    }

    ViewBag.Categories = _categoryService.GetAll();

    return View(model);
}


        [HttpPost]
        public IActionResult DeleteProduct(int productId){
            var entity=_productService.GetById(productId);
            if(entity!=null){
                _productService.Delete(entity);
            }
            return RedirectToAction("ProductList");
        }
        public IActionResult CategoryList(){
            return View( new CategoryListModel(){
                Categories=_categoryService.GetAll()
            });
            }
        [HttpGet]
        public IActionResult CreateCategory(){
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(CategoryModel model){
            var entity=new Category(){
                Name=model.Name
            };
            _categoryService.Create(entity);

           return  RedirectToAction("CategoryList");
        }
        [HttpGet]
        public IActionResult EditCategory(int id){
            var entity= _categoryService.GetByIdWithProducts(id);

            return View(new CategoryModel(){
                Id=entity.Id,
                Name=entity.Name,
                Products=entity.ProductCategories.Select(p=>p.Product).ToList(),
            });
        }
        [HttpPost]
        public IActionResult EditCategory(CategoryModel model){
            var entity=_categoryService.GetById(model.Id);
            if(entity==null) return NotFound();
            entity.Name=model.Name;
            _categoryService.Update(entity);
            return RedirectToAction("CategoryList");
        }
        [HttpPost]
        public IActionResult DeleteCategory(int categoryId){
            var entity=_categoryService.GetById(categoryId);
            if(entity!=null){
                _categoryService.Delete(entity);
            }

            return RedirectToAction("CategoryList");
        }
        [HttpPost]
        public IActionResult DeleteFromCategory(int categoryId,int productId){
                _categoryService.DeleteFromCategory(categoryId,productId);
                return Redirect("/admin/editcategory/"+categoryId);
        }

        public IActionResult AccessDenied(){
        return View();
                }

        
    }
}