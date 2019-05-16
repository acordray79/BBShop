using BBShop.Data;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBShop.Model.Admin
{
    public class AdminDetail
    {
        public int AdminID { get; set; }
        public string UserID { get; set; }
        public ICollection<IdentityUserRole> RoleID { get; set; }
        public string RoleName { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
