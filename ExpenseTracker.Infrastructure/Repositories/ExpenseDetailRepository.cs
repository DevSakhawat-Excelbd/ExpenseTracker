using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure.Contracts;
using ExpenseTracker.Infrastructure.Sql;

namespace ExpenseTracker.Infrastructure.Repositories
{
    public class ExpenseDetailRepository : Repository<ExpenseDetail>, IExpenseDetailRepository
    {
        private readonly DataContext context;
        public ExpenseDetailRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
