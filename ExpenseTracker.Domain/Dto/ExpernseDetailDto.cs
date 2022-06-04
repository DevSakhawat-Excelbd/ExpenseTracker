using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Dto
{
   public class ExpernseDetailDto
   {
      [Key]
      public Guid ExpenseDetaisId { get; set; }
      /// <summary>
      /// Entry Expense date
      /// </summary>
      public DateTime ExpenseDate { get; set; }
      /// <summary>
      /// Expense Amount
      /// </summary>
      public decimal ExpenseAmount { get; set; }
      /// <summary>
      /// Foreign key, Primary key of the Category table.
      /// </summary>
      public Guid CategoryId { get; set; }
      /// <summary>
      /// Entry the created date
      /// </summary>
      [Column(TypeName = "smalldatetime")]
      public DateTime? CreatedDate { get; set; }
      /// <summary>
      /// Modified Date of the row.
      /// </summary>
      [Column(TypeName = "smalldatetime")]
      public DateTime? ModifiedDate { get; set; }
   }
}
