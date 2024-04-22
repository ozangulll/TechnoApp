using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Business.Abstract;
using ShopApp.DataAccess.Abstract;
using ShopApp.DataAccess.Concrete.EFCore;
using ShopApp.Entities;

namespace ShopApp.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDAL _ProductDAL;

        public ProductManager(IProductDAL productDAL)
        {
            _ProductDAL = productDAL;
        }

        public void Create(Product entity)
        {
           _ProductDAL.Create(entity);
        }

        public void Delete(Product entity)
        {
           _ProductDAL.Delete(entity);
        }

        public List<Product> GetAll()
        {
            return _ProductDAL.GetAll().ToList();
        }

        public Product GetById(int id)
        {
            return _ProductDAL.GetById(id);
        }

    

        public Product GetProductDetails(int id)
        {
          return _ProductDAL.GetProductDetails(id);
        }

        public void Update(Product entity)
        {
            _ProductDAL.Update(entity);
        }

        public List<Product> GetProductByCategory(string category,int page,int pageSize)
        {
           return _ProductDAL.GetProductsByCategory(category,page,pageSize);
        }

        public int GetCountByCategory(string category)
        {
           return _ProductDAL.GetCountByCategory(category);
        }

        public Product GetByIdWithCategories(int id)
        {
            return _ProductDAL.GetByIdWithCategories(id);
        }

        public void Update(Product entity, int[] categoryIds)
        {
            _ProductDAL.Update(entity,categoryIds);
        }

        public void CreateWithCategories(Product entity, int[] categoryIds)
        {
            _ProductDAL.CreateWithCategories(entity,categoryIds);
        }
    }
}