using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShopApp.Business.Abstract;
using ShopApp.Entities;
using ShopApp.WebUI.Models;

namespace ShopApp.WebUI.Controllers
{
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
        [HttpGet]
        public IActionResult CreateProduct(){
            return View();
        }
        [HttpPost]
        public IActionResult CreateProduct(ProductModel productModel){
              if(ModelState.IsValid==true){
                var entity=new Product(){
                Name=productModel.Name,
                Price=productModel.Price,
                Description=productModel.Description,
                ImageUrl=productModel.ImageUrl
            };
            _productService.Create(entity);
            return RedirectToAction("ProductList");
            }
            return View(productModel);
            
        }
        public IActionResult EditProduct(int? id){
            if(id==null){
                return NotFound();
            }
            var entity=_productService.GetByIdWithCategories((int)id);
            if(entity==null){
                return NotFound();
            }
           var model=new ProductModel(){
                Id=entity.Id,
                Name=entity.Name,
                Price=entity.Price,
                Description=entity.Description,
                ImageUrl=entity.ImageUrl,
                SelectedCategories=entity.ProductCategories.Select(i=>i.Category).ToList()
            };
            ViewBag.Categories=_categoryService.GetAll();
            return View(model);
        }
        [HttpPost]
        public IActionResult EditProduct(ProductModel productModel,int[] categoryIds){
            var entity=_productService.GetById(productModel.Id);
            if(entity==null){return NotFound();}
            entity.Name=productModel.Name;
            entity.Price=productModel.Price;
            entity.Description=productModel.Description;
            entity.ImageUrl=productModel.ImageUrl;
            _productService.Update(entity,categoryIds);
            return RedirectToAction("ProductList");
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

        
    }
}