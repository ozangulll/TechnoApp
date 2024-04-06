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

        public List<Product> GetPopularProduct()
        {
            return _ProductDAL.GetAll().ToList();
        }

        public void Update(Product entity)
        {
            _ProductDAL.Update(entity);
        }
    }
}