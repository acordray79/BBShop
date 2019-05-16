using BBShop.Data;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBShop.Model.Admin
{
    public class AdminUpdate
    {
        public int AdminID { get; set; }
        public string CustomerID { get; set; }
        public ICollection<IdentityUserRole> RoleID { get; set; }
        public string RoleName { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
