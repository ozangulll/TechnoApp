using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;

namespace ShopApp.DataAccess.Concrete.EFCore
{
    public class EFCoreCartDAL : EFCoreGenericRepository<Cart, ShopContext>, ICartDAL
    {
        public Cart GetByUserId(string userid)
        {
            using (var context=new ShopContext()){
                return context
                            .Carts
                            .Include(i=>i.CartItems)
                            .ThenInclude(i=>i.Product)
                            .FirstOrDefault(i=>i.userId==userid);
            }
        }
    }
}