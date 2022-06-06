﻿using ExpenseTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Contracts
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IList<Category> LoadCategory();
    }
}
