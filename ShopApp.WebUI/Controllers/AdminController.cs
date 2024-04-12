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
            return Redirect("Index");
        }
    }
}