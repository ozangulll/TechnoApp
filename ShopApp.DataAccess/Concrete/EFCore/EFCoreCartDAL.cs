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
        public override void Update(Cart entity)
        {
            using (var context=new ShopContext()){
                context.Carts.Update(entity);
                context.SaveChanges();
            }
        }
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
        public void DeleteFromCart(int cartId,int productId){
            using (var context=new ShopContext()){
                var cmd=@"delete from CartItem  where CartId=@p0 and ProductId=@p1";
                context.Database.ExecuteSqlRaw(cmd,cartId,productId);
            }
        }
    }
}