using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ShopApp.Entities;

namespace ShopApp.DataAccess.Abstract
{
    public interface ICategoryDAL : IRepository<Category>
    {
        Category GetByIdWithProducts(int id);
    }
}
