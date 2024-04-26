using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Entities;

namespace ShopApp.DataAccess.Abstract
{
    public interface ICartDAL : IRepository<Cart>
    {
        Cart GetByUserId(string userid);
        void DeleteFromCart(int cardId, int productId);
    }
}