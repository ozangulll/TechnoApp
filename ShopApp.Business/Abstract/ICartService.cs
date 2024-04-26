using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Entities;

namespace ShopApp.Business.Abstract
{
    public interface ICartService
    {
        void InitializeCart(string userid);
        Cart GetCartByUserId(string userid);
        void AddToCart(string userid,int productId,int quantity);
    }
}