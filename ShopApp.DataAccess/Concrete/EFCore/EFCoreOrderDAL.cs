using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;

namespace ShopApp.DataAccess.Concrete.EFCore
{
    public class EFCoreOrderDAL : EFCoreGenericRepository<Order, ShopContext>, IOrderDAL
    {
        public List<Order> GetOrders(string userId)
        {
          using (var context=new ShopContext()){
            var orders=context.Orders
                                .Include(i=>i.OrderItems)
                                .ThenInclude(i=>i.Product)
                                .AsQueryable();
          
          if(!string.IsNullOrEmpty(userId))
          {
            orders=orders.Where(i=>i.UserId==userId);
          }
          return orders.ToList();
          }
        }
    }
}