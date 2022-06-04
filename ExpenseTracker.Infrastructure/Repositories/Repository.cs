﻿using ExpenseTracker.Domain.Entities;
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
        protected readonly DataContext _context;

        public Repository(DataContext context)
        {
            this._context = context;

        }
        public T Add(T entity)
        {
            try
            {
                entity.IsRowDeleted = false;
                return _context.Set<T>().Add(entity).Entity;
            }
            catch
            {
                throw;
            }
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public T Update(T entity)
        {
            try
            {
                entity.IsRowDeleted = false;
                return _context.Set<T>().Update(entity).Entity;
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
                return _context.Set<T>()
                    .AsQueryable()
                    .AsNoTracking().Where(x => x.IsRowDeleted.Equals(false))
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public T FirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _context.Set<T>().AsQueryable().AsNoTracking().FirstOrDefault(predicate);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T?, bool>> predicate)
        {
            return await _context.Set<T>().AsQueryable().AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<T> Query(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var entity = _context.Set<T>().AsNoTracking().Where(predicate);
            return entity;
        }

        IQueryable<T> IRepository<T>.GetAll()
        {
            var entity = _context.Set<T>().AsNoTracking();
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
                var entity = _context.Set<T>().Find(id);
                _context.Entry(entity).State = EntityState.Detached;
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
                var entity = _context.Set<T>().Find(Key);
                _context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return null;
            }

            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<T?> GetByIdAsync(int key)
        {
            var entity = await _context.Set<T>().FindAsync(key);
            if (entity == null)
            {
                return null;
            }

            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

       
    }
}