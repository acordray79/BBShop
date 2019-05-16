using BBShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBShop.Model.Admin
{
    public class AdminCreate
    {
        public int AdminID { get; set; }
        public string CustomerID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}