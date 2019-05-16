using BBShop.Data;
using BBShop.Model.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBShop.Service
{
    public class CustomerService
    {
        private readonly string _userIDStr;
        private readonly Guid _userID;

        public CustomerService(Guid userID)
        {
            _userID = userID;
            _userIDStr = userID.ToString();
        }
        public bool CreateCustomer(CustomerCreate model)
        {
            var entity =
                new ApplicationUser()
                {
                    Id = _userIDStr,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    StreetAddress = model.StreetAddress,
                    City = model.City,
                    State = model.State,
                    ZipCode = model.ZipCode,
                    Telephone = model.Telephone,
                    CreditCard = model.CreditCard,
                    Email = model.Email
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Users.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<CustomerList> GetCustomer()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Users
                        .Select(
                            e =>
                                new CustomerList
                                {
                                    CustomerID = e.Id,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
                                    StreetAddress = e.StreetAddress,
                                    City = e.City,
                                    State = e.State,
                                    ZipCode = e.ZipCode,
                                    Telephone = e.Telephone,
                                    CreditCard = e.CreditCard,
                                    Email = e.Email
                                }
                        );

                return query.ToArray();
            }
        }
        public CustomerDetail GetCustomerByID(string customerID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Users
                        .Single(e => e.Id == customerID);
                return
                    new CustomerDetail
                    {
                        CustomerID = entity.Id,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        StreetAddress = entity.StreetAddress,
                        City = entity.City,
                        State = entity.State,
                        ZipCode = entity.ZipCode,
                        Telephone = entity.Telephone,
                        CreditCard = entity.CreditCard,
                        Email = entity.Email
                    };
            }
        }
        public bool UpdateCustomer(CustomerUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Users
                        .Single(e => e.Id == model.CustomerID);
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.StreetAddress = model.StreetAddress;
                entity.City = model.City;
                entity.State = model.State;
                entity.ZipCode = model.ZipCode;
                entity.Telephone = model.Telephone;
                entity.CreditCard = model.CreditCard;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteCustomer(string customerId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Users
                        .Single(e => e.Id == customerId);

                ctx.Users.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
