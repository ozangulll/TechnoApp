using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;

namespace ShopApp.DataAccess.Concrete.EFCore
{
    public class EFCoreOrderDAL:EFCoreGenericRepository<Order,ShopContext>,IOrderDAL
    {
        
    }
}