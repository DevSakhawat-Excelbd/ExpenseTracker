using ExpenseTracker.Infrastructure.Contracts;
using ExpenseTracker.Infrastructure.Sql;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Repositories
{
   public class UnitOfWork : IUnitOfWork
   {
      protected readonly DataContext dbContext;
      private readonly IConfiguration configuration;
      public UnitOfWork(DataContext dbContext, IConfiguration configuration)
      {
         this.dbContext = dbContext;
         this.configuration = configuration;
      }

      public void SaveChanges()
      {
         dbContext.SaveChanges();
      }

      public async Task SaveChangesAsync()
      {
        await dbContext.SaveChangesAsync();
      }
      #region Category
      private ICategoryRepository? categoryRepository;
      public ICategoryRepository CategoryRepository
      {
         get
         {
            if (categoryRepository == null)
               categoryRepository = new CategoryRepository(dbContext);

            return categoryRepository;
         }
         
      }
      #endregion Category

      #region ExpenseDetail
      private IExpenseDetailRepository expenseDetailRepository;
      public IExpenseDetailRepository ExpenseDetailRepository
      {
         get
         {
            if (expenseDetailRepository == null)
               expenseDetailRepository = new ExpenseDetailRepository(dbContext);
            return expenseDetailRepository;
         }
      }
      #endregion ExpenseDetail

   }
}
