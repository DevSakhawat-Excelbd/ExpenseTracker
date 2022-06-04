using ExpenseTracker.Utilities.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Domain.Entities
{
    public class ExpenseDetail:BaseModel
    {
        [Key]
       
        public Guid ExpenseDetaisId { get; set; }
     
   
        public DateTime ExpenseDate { get; set; }
     

        public decimal ExpenseAmount { get; set; }
    
        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
