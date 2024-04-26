using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Business.Abstract
{
    public interface ICartService
    {
        void InitializeCart(string userid);
    }
}