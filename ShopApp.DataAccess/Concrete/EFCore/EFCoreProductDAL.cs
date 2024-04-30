using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;

namespace ShopApp.DataAccess.Concrete.EFCore
{
    public class EFCoreProductDAL : EFCoreGenericRepository<Product, ShopContext>, IProductDAL
    {
        public Product GetByIdWithCategories(int id)
        {
           using (var context=new ShopContext()){
            return context.Products.Where(i=>i.Id==id).Include(i=>i.ProductCategories).ThenInclude(i=>i.Category).FirstOrDefault();
           }
        }

        public int GetCountByCategory(string category)
        {
            using (var context=new ShopContext()){
            var products=context.Products.AsQueryable();
            if(!string.IsNullOrEmpty(category)){
                products=products.Include(i=>i.ProductCategories).
                ThenInclude(i=>i.Category).
                Where(i=>i.ProductCategories.Any(a=>a.Category.Name.ToLower()==category.ToLower()));
            }
            return products.Count();
        }
        }

        public IEnumerable<Product> GetPopularProducts()
        {
            throw new NotImplementedException();
        }

        public Product GetProductDetails(int id)
        {
           using (var context=new ShopContext()){
                return context.Products.Where(i => i.Id == id).Include(i => i.ProductCategories).ThenInclude(i => i.Category).FirstOrDefault();
           }
        }

        public List<Product> GetProductsByCategory(string Category,int page,int pageSize)
        {
           using (var context=new ShopContext()){
            var products=context.Products.AsQueryable();
            if(!string.IsNullOrEmpty(Category)){
                products=products.Include(i=>i.ProductCategories).
                ThenInclude(i=>i.Category).
                Where(i=>i.ProductCategories.Any(a=>a.Category.Name.ToLower()==Category.ToLower()));
            }
            return products.Skip((page-1)*pageSize).Take(pageSize).ToList();
           
           }
        }

        public void Update(Product entity, int[] categoryIds)
        {
            using (var context = new ShopContext())
            {
                var product = context.Products
                                   .Include(i => i.ProductCategories)
                                   .FirstOrDefault(i => i.Id == entity.Id);

                if (product != null)
                {
                    product.Name = entity.Name;
                    product.Description = entity.Description;
                    product.ImageUrl = entity.ImageUrl;
                    product.Price = entity.Price;

                    product.ProductCategories = categoryIds.Select(catid => new ProductCategory()
                    {
                        CategoryId = catid,
                        ProductId = entity.Id
                    }).ToList();

                    context.SaveChanges();
                }
            }
        }
        public void CreateWithCategories(Product entity, int[] categoryIds)
{
    using (var context = new ShopContext())
    {
        // Yeni ürünü ekle
        context.Products.Add(entity);
        context.SaveChanges();

        // Yeni ürünün kategorilerini eklemek
        foreach (var categoryId in categoryIds)
        {
            var category = context.Categories.Find(categoryId);
            if (category != null)
            {
                entity.ProductCategories = categoryIds.Select(catid => new ProductCategory()
                    {
                        CategoryId = catid,
                        ProductId = entity.Id
                    }).ToList();
            }
        }

        context.SaveChanges();
    }
}

        
        
        
        }
    }


