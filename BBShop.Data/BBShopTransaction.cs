using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBShop.Data
{
    public class BBShopTransaction
    {
        [Key]
        public int TransactionID { get; set; }
        [Required]
        public string CustomerID { get; set; }
        [Required]
        public int ProductID { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }

        [ForeignKey("CustomerID")]
        public virtual ApplicationUser Customer { get; set; }
        public virtual BBShopProduct Product { get; set; }

    }
}