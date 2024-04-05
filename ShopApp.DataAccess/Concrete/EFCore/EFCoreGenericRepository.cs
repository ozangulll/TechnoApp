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
            using(var context=new TContext()){
                context.Set<T>().Remove(entity);
                context.SaveChanges();
            }
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter=null)
        {
           using(var context=new TContext()){
                return filter==null?context.Set<T>():context.Set<T>().Where(filter);
            }
        }

        public T GetById(int id)
        {
            using(var context=new TContext()){
                return context.Set<T>().Find(id);
                
            }
        }

        public T GetOne(Expression<Func<T, bool>> filter)
        {
           using(var context=new TContext()){
                return context.Set<T>().Where(filter).SingleOrDefault();

            }
        }

        public void Update(T entity)
        {
            using(var context=new TContext()){
                context.Entry(entity).State=EntityState.Modified;
                context.SaveChanges();
                
            }
        }
    }
}