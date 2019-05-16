using BBShop.Data;
using BBShop.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBShop.Service
{
    public class TransactionService
    {
        public bool CreateTrans(TransactionCreate model)
        {
            var entity =
                new BBShopTransaction()
                {
                    CustomerID = model.CustomerID,
                    ProductID = model.ProductID,
                    CreatedUtc = DateTimeOffset.Now
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.BBShopTransactions.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<TransactionList> GetTrans()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .BBShopTransactions
                        .Select(
                            e =>
                                new TransactionList
                                {
                                    TransactionID = e.TransactionID,
                                    CustomerID = e.CustomerID,
                                    ProductID = e.ProductID,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }
        public TransactionDetail GetTransByID(int transID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .BBShopTransactions
                        .Single(e => e.TransactionID == transID);
                return
                    new TransactionDetail
                    {
                        TransactionID = entity.TransactionID,
                        FullName = entity.Customer.FullName,
                        ProductName = entity.Product.ProductName,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }
        public bool UpdateTrans(TransactionUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .BBShopTransactions
                        .Single(e => e.TransactionID == model.TransactionID);
                entity.CustomerID = model.CustomerID;
                entity.ProductID = model.ProductID;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteTrans(int transId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .BBShopTransactions
                        .Single(e => e.TransactionID == transId);

                ctx.BBShopTransactions.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
