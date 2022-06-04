using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.DTO
{
    public class ExpenseDetailDto
    {
        public Guid ExpenseDetaisId { get; set; }


        public DateTime ExpenseDate { get; set; }


        public decimal ExpenseAmount { get; set; }

        public Guid CategoryId { get; set; }
    }
}
