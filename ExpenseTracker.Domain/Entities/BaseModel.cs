using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [Column(TypeName ="smalldatetime")]
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Modified Date of the row.
        /// </summary>
        [Column(TypeName="smalldatetime")]
        public DateTime? ModifiedDate { get; set; }

    }
}
