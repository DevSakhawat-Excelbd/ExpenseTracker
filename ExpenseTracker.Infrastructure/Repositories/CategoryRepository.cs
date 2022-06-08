using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure.Contracts;
using ExpenseTracker.Infrastructure.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        protected readonly DataContext context;
        public CategoryRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IList<Category> LoadCategory()
        {
            var list = (from a in context.Categories
                        where a.IsRowDeleted.Equals(false)
                        select new Category
                        {
                            CategoryName = a.CategoryName,
                        }).ToList();
            return list;
        }
    }
}
