using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Contracts
{
    /// <summary>
    /// Contains signature of all generic methods.
    /// </summary>
    /// <typeparam name="T"> T is a Model class</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Inserts information available in the given object.
        /// </summary>
        /// <param name="entity">Object name</param>
        /// <returns>Inserted object</returns>
        T Add(T entity);
        /// <summary>
        /// add multi object to database
        /// </summary>
        /// <param name="entities"></param>
        void AddRange(IEnumerable<T> entities);
        /// <summary>
        /// Upddats database table with the information available int the given object. 
        /// </summary>
        /// <param name="entity">object to be updated</param>
        /// <returns></returns>
        T Update(T entity);
        /// <summary>
        /// Deletes the given object
        /// </summary>
        /// <param name="entity">Delete to be removed</param>
        void Delete(T entity);
        /// <summary>
        /// delete multi objects
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRange(IEnumerable<T> entities);
        /// <summary>
        /// Searches using primay key.
        /// </summary>
        /// <param name="id">Primary Key of the Table</param>
        /// <returns>Retrived row in the form of model object</returns>
        T GetById(Guid id);
        T GetById(int Key);
        /// <summary>
        /// Searches using primary key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T?> GetByIdAsync(int key);
        /// <summary>
        /// Loads all rows from the database table.
        /// </summary>
        /// <returns>Object list</returns>
        IQueryable<T> GetAll();
        /// <summary>
        /// Seaches using the given criteria.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Retrived row in the form of model object</returns>
        IQueryable<T> Query(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Searches using the given criteria.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        
    }
}
