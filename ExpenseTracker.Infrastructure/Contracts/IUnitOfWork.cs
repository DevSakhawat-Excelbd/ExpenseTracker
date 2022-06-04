﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Contracts
{
   public interface IUnitOfWork
   {
      /// <summary>
      /// Declare SaveChanges method.
      /// </summary>
      void SaveChanges();

      /// <summary>
      /// Declare SaveChangesAsync method.
      /// </summary>
      /// <returns></returns>
      Task SaveChangesAsync();

      ICategoryRepository CategoryRepository { get; }
      IExpenseDetailRepository ExpenseDetailRepository { get; }

   }
}
