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
        public IEnumerable<Product> GetPopularProducts()
        {
            throw new NotImplementedException();
        }

        public Product GetProductDetails(int id )
        {
           using (var context=new ShopContext()){
                return context.Products.Where(i => i.Id == id).Include(i => i.ProductCategories).ThenInclude(i => i.Category).FirstOrDefault();
           }
        }

    }
}