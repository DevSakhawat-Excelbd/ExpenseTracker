using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Dto
{
   public class ExpenseDetailDto
   {
      /// <summary>
      /// Primary Key of Category Entity
      /// </summary>
      [Key]
      public Guid ExpenseDetailId { get; set; }
        /// <summary>
        /// Entry Expense date
        /// </summary>
        [Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Expenses")]
        public DateTime ExpenseDate { get; set; }
      /// <summary>
      /// Expense Amount
      /// </summary>
      public decimal ExpenseAmount { get; set; }
      /// <summary>
      /// Foreign key, Primary key of the Category table.
      /// </summary>
      public int CategoryId { get; set; }
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

      /// <summary>
      /// Indicates the row is deleted or not.
      /// </summary>
      //public bool? IsRowDeleted { get; set; }

        public CategoryDto Category { get; set; }
   }
}
