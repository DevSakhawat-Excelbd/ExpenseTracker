using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ExpenseTracker.Domain.Entities
{
   /// <summary>
   /// Contains common properties for all models
   /// </summary>
   public class BaseModel
   {
      /// <summary>
      /// Entry the created date
      /// </summary>
      [Column(TypeName = "smalldatetime")]
      [DataType(DataType.Date)]
      public DateTime? CreatedDate { get; set; }

      /// <summary>
      /// Modified Date of the row.
      /// </summary>
      [Column(TypeName = "smalldatetime")]
      [DataType(DataType.Date)]
      public DateTime? ModifiedDate { get; set; }

      /// <summary>
      /// Indicates the row is deleted or not.
      /// </summary>
      public bool? IsRowDeleted { get; set; }
   }
}
