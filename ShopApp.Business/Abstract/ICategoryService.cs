using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Entities;

namespace ShopApp.Business.Abstract
{
    public interface ICategoryService
    {
        Category GetById(int id);
        Category GetByIdWithProducts(int id);
        List<Category> GetAll();
        void Create(Category entity);
        void Delete(Category entity);
        void Update(Category entity);
        void DeleteFromCategory(int categoryId, int productId);
    }
}