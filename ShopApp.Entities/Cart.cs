using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public string userId { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}