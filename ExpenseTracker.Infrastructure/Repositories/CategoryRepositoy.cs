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
    public class CategoryRepositoy : Repository<Category>, ICategoryRepositoy
    {
        private readonly DataContext context;
        public CategoryRepositoy(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
