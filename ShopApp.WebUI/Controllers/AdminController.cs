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

        public AdminController(IProductService productService)
        {
            _productService = productService;
        }


        public IActionResult Index()
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
        public IActionResult Edit(int id){
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
        public IActionResult Edit(ProductModel productModel){
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
        public IActionResult Delete(int productId){
            var entity=_productService.GetById(productId);
            if(entity!=null){
                _productService.Delete(entity);
            }
            return RedirectToAction("Index");
        }
    }
}