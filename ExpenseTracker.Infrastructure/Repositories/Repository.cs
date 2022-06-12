using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure.Contracts;
using ExpenseTracker.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Repositories
{
   public class Repository<T> : IRepository<T> where T : BaseModel
   {
      protected readonly DataContext context;

      public Repository(DataContext context)
      {
         this.context = context;
      }

      public T Add(T entity)
      {
         try
         {
            entity.IsRowDeleted = false;
            return context.Set<T>().Add(entity).Entity;
         }
         catch
         {
            throw;
         }
      }

      public void AddRange(IEnumerable<T> entities)
      {
         context.Set<T>().AddRange(entities);
      }

      public T Update(T entity)
      {
         try
         {
            entity.IsRowDeleted = false;
            return context.Set<T>().Update(entity).Entity;
         }
         catch
         {
            throw;
         }
      }

      public IEnumerable<T> GetAll()
      {
         try
         {
            return context.Set<T>()
                .AsQueryable()
                .AsNoTracking().Where(x => x.IsRowDeleted.Equals(false))
                .ToList();
         }
         catch
         {
            throw;
         }
      }

      IQueryable<T> IRepository<T>.GetAll()
      {
         var entity = context.Set<T>().AsNoTracking();
         return entity;
      }

      public void Delete(T entity)
      {
         throw new NotImplementedException();
      }

      public void RemoveRange(IEnumerable<T> entities)
      {
         context.Set<T>().RemoveRange(entities);
      }

      public T FirstOrDefault(Expression<Func<T, bool>> predicate)
      {
         try
         {
            return context.Set<T>().AsQueryable().AsNoTracking().FirstOrDefault(predicate);
         }
         catch (Exception)
         {

            throw;
         }
      }

      public async Task<T?> FirstOrDefaultAsync(Expression<Func<T?, bool>> predicate)
      {
         return await context.Set<T>().AsQueryable().AsNoTracking().FirstOrDefaultAsync(predicate);
      }

      public IEnumerable<T> Query(Expression<Func<T, bool>> predicate)
      {
         var entity = context.Set<T>().AsNoTracking().Where(predicate);
         return entity;
      }



      IQueryable<T> IRepository<T>.Query(Expression<Func<T, bool>> predicate)
      {
         throw new NotImplementedException();
      }

      public virtual T GetById(Guid id)
      {
         try
         {
            var entity = context.Set<T>().Find(id);
            context.Entry(entity).State = EntityState.Detached;
            return entity;
         }
         catch (Exception)
         {

            throw;
         }
      }

      public T GetById(int Key)
      {
         try
         {
            var entity = context.Set<T>().Find(Key);
            context.Entry(entity).State = EntityState.Detached;
            return entity;
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<T?> GetByIdAsync(Guid key)
      {
         var entity = await context.Set<T>().FindAsync(key);
         if (entity == null)
         {
            return null;
         }

         context.Entry(entity).State = EntityState.Detached;
         return entity;
      }

      public async Task<T?> GetByIdAsync(int key)
      {
         var entity = await context.Set<T>().FindAsync(key);
         if (entity == null)
         {
            return null;
         }

         context.Entry(entity).State = EntityState.Detached;
         return entity;
      }
   }
}
