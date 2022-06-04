﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Contracts
{
   public interface IUnitOfWorkIWin
   {
        void SaveChanges();

        Task SaveChangesAsync();
        IExpenseDetailRepository ExpenseDetailRepository { get; }
        ICategoryRepository CategoryRepository { get; }
   }
}
