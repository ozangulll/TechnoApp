using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ShopApp.Entities;

namespace ShopApp.DataAccess.Abstract
{
    public interface IProductDAL:IRepository<Product>
    {
        //zaten fix olan methodları biz repositoryden alıcaz fakat üstüne burada mesela producta özel olan methodları ekleyebileceğiz.

        List<Product> GetProductsByCategory(string Category,int page,int pageSize);
        Product GetProductDetails(int id);

    }
}