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

        public void AddToCart(string userid, int productId, int quantity)
        {
            var cart=GetCartByUserId(userid);
            if(cart!=null){
                var index=cart.CartItems.FindIndex(i=>i.ProductId==productId);
                if(index<0){
                    cart.CartItems.Add(new CartItem(){
                        ProductId=productId,
                        Quantity=quantity,
                        CartId=cart.Id
                    });
                }else{
                   cart.CartItems[index].Quantity +=quantity; 
                }
                _cartDal.Update(cart);
            }
        }

        public void ClearCart(int cartId)
        {
            _cartDal.ClearCart(cartId);
        }

        public void DeleteFromCart(string? userid, int productId)
        {
            var cart=GetCartByUserId(userid);
            if(cart!=null){
                var cartId=cart.Id;
                _cartDal.DeleteFromCart(cartId,productId);
            }
        }

        public Cart GetCartByUserId(string userId)
        {
          return _cartDal.GetByUserId(userId);
        }

        public void InitializeCart(string userid)
        {
           _cartDal.Create(new Cart(){userId=userid});
        }
    }
}