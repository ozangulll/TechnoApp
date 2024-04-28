using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Business.Abstract;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;

namespace ShopApp.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private IOrderDAL _orderDAL;

        public OrderManager(IOrderDAL orderDAL)
        {
            _orderDAL = orderDAL;
        }

        public void Create(Order entity)
        {
            
        }
    }
}