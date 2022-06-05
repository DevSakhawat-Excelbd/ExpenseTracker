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
    public class UnitOfWorkIWin : IUnitOfWorkIWin
    {
        protected readonly DataContext dbContext;
        private readonly IConfiguration configuration;

        public UnitOfWorkIWin(DataContext context,IConfiguration configuration)
        {
            this.dbContext = context;
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

        #region CategoryRepository
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
        #endregion


        #region ExpenseDetailRepository
        private IExpenseDetailRepository? expenseDetailRepository;

        IExpenseDetailRepository IUnitOfWorkIWin.ExpenseDetailRepository
        {
            get
            {

                if (expenseDetailRepository == null)
                    expenseDetailRepository = new ExpenseDetailRepository(dbContext);

                return expenseDetailRepository;
            }
        }
        #endregion
    }
}
