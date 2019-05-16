using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBShop.Data
{
    public class BBShopAdmin
    {
        [Key]
        public int AdminID { get; set; }
        public string CustomerID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}