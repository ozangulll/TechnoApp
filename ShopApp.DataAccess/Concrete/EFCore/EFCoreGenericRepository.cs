using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;

namespace ShopApp.DataAccess.Concrete.EFCore
{
    public class EFCoreGenericRepository<T, TContext> : IRepository<T> where T : class where TContext : DbContext, new()
    {
        public void Create(T entity)
        {
            using(var context=new TContext()){
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public T GetOne(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}