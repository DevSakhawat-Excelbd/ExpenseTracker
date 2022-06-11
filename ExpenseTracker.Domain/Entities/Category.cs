using ExpenseTracker.Utilities.Constants;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Domain.Entities
{

   /// <summary>
   /// Types of category
   /// </summary>

   public class Category : BaseModel
   {
      /// 
      [Key]
      public int CategoryId { get; set; }


      public string CategoryName { get; set; }

      public virtual IEnumerable<ExpenseDetail> ExpenseDetails { get; set; }

   }
}
