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
    public class EFCoreCategoryDAL : EFCoreGenericRepository<Category, ShopContext>, ICategoryDAL
    {
        public Category GetByIdWithProducts(int id)
        {
            using(var context=new ShopContext()){
                    return context.Categories.Where(i=>i.Id==id)
                    .Include(i=>i.ProductCategories)
                    .ThenInclude(i=>i.Product).FirstOrDefault(); 
            }
        }
    }
}