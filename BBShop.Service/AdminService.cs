using BBShop.Data;
using BBShop.Model;
using BBShop.Model.Admin;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBShop.Service
{
    public class AdminService
    {
        public bool CreateAdmin(AdminCreate model)
        {
            var entity =
                new BBShopAdmin()
                {
                    CustomerID = model.CustomerID
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.BBShopAdmins.Add(entity);
                return ctx.SaveChanges() == 1;

            }
        }
        public IEnumerable<AdminList> GetCustomer()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Users
                        .Select(
                            e =>
                                new AdminList
                                {
                                    UserID = e.Id,
                                    RoleID = e.Roles
                                }
                        );

                return query.ToArray();
            }
        }
        public AdminDetail GetAdminByID(string adminID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Users
                        .Single(e => e.Id == adminID);
                return
                    new AdminDetail
                    {
                        RoleID = entity.Roles,
                        UserID = entity.Id
                    };
            }
        }
        public bool UpdateAdmin(AdminUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Users
                        .Single(e => e.Id == model.CustomerID);
                //entity.Id = model.CustomerID;
                //var roleEntity = 
                //    ctx 
                //    .
                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteAdmin(int adminId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .BBShopAdmins
                        .Single(e => e.AdminID == adminId);

                ctx.BBShopAdmins.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<IdentityRoleDTO> GetRole()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Roles
                        .Select(
                            e =>
                                new IdentityRoleDTO
                                {
                                    Id = e.Id,
                                    Name = e.Name
                                }
                        );

                return query.ToArray();
            }
        }
        public IdentityRole GetIdentityRoleByID(string roleID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Roles
                    .FirstOrDefault(x => x.Id == roleID);
            return
                new IdentityRole
                {
                    Id = entity.Id,
                    Name = entity.Name
                };
            }
        }
    }
}
