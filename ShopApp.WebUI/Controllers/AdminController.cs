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
            var entity=new Product(){
                Name=productModel.Name,
                Price=productModel.Price,
                Description=productModel.Description,
                ImageUrl=productModel.ImageUrl
            };
            _productService.Create(entity);
            return RedirectToAction("Index");
        }
        public IActionResult EditProduct(int id){
            if(id==null){
                return NotFound();
            }
            var entity=_productService.GetById((int)id);
            if(entity==null){
                return NotFound();
            }
           var model=new ProductModel(){
                Id=entity.Id,
                Name=entity.Name,
                Price=entity.Price,
                Description=entity.Description,
                ImageUrl=entity.ImageUrl
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult EditProduct(ProductModel productModel){
            var entity=_productService.GetById(productModel.Id);
            if(entity==null){return NotFound();}
            entity.Name=productModel.Name;
            entity.Price=productModel.Price;
            entity.Description=productModel.Description;
            entity.ImageUrl=productModel.ImageUrl;
            _productService.Update(entity);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteProduct(int productId){
            var entity=_productService.GetById(productId);
            if(entity!=null){
                _productService.Delete(entity);
            }
            return RedirectToAction("Index");
        }
        public IActionResult CategoryList(){
            return View( new CategoryListModel(){
                Categories=_categoryService.GetAll()
            });
        }
    }
}