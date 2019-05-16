using BBShop.Data;
using BBShop.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBShop.Service
{
    public class ProductService
    {
        private readonly Guid _userID;

        public ProductService(Guid userID)
        {
            _userID = userID;
        }
        public bool CreateProduct(ProductCreate model)
        {
            var entity =
                new BBShopProduct()
                {
                    OwnerID = _userID,
                    ProductName = model.ProductName,
                    Price = model.Price,
                    ProductQuantity = model.ProductQuantity,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.BBShopProducts.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<ProductList> GetProducts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .BBShopProducts
                        .Where(e => e.OwnerID == _userID)
                        .Select(
                            e =>
                                new ProductList
                                {
                                    ProductID = e.ProductID,
                                    ProductName = e.ProductName,
                                    Price = e.Price,
                                    ProductQuantity = e.ProductQuantity,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }
        public ProductDetail GetProductByID(int productID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .BBShopProducts
                        .Single(e => e.ProductID == productID && e.OwnerID == _userID);
                return
                    new ProductDetail
                    {
                        ProductID = entity.ProductID,
                        ProductName = entity.ProductName,
                        Price = entity.Price,
                        ProductQuantity = entity.ProductQuantity,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }
        public bool UpdateProduct(ProductUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .BBShopProducts
                        .Single(e => e.ProductID == model.ProductID && e.OwnerID == _userID);
                entity.ProductName = model.ProductName;
                entity.Price = model.Price;
                entity.ProductQuantity = model.ProductQuantity;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteProduct(int noteId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .BBShopProducts
                        .Single(e => e.ProductID == noteId && e.OwnerID == _userID);

                ctx.BBShopProducts.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}