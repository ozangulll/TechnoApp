using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Business.Abstract;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;

namespace ShopApp.Business.Concrete
{
    public class CartManager : ICartService
    {
        private ICartDAL _cartDal;

        public CartManager(ICartDAL cartDal)
        {
            _cartDal = cartDal;
        }

        public void InitializeCart(string userid)
        {
           _cartDal.Create(new Cart(){userId=userid});
        }
    }
}