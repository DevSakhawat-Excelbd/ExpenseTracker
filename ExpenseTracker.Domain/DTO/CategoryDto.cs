using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Dto
{
   /// <summary>
   /// Types of category
   /// </summary>

   public class CategoryDto
   {
      /// <summary>
      /// Category Id
      /// </summary>
      [Key]
      public int CategoryId { get; set; }
      /// <summary>
      /// Expense Category Name
      /// </summary>
      public string CategoryName { get; set; }

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
