using BBShop.Model.Product;
using BBShop.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BBShop.WebApi.Controllers
{
    [Authorize]
    public class ProductController : ApiController
    {
        public IHttpActionResult GetAllProducts()
        {
            ProductService productService = CreateProductService();
            var products = productService.GetProducts();
            return Ok(products);
        }
        public IHttpActionResult Get(int id)
        {
            ProductService productService = CreateProductService();
            var product = productService.GetProductByID(id);
            return Ok(product);
        }

        public IHttpActionResult Post(ProductCreate product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateProductService();

            if (!service.CreateProduct(product))
                return InternalServerError();
            return Ok();
        }

        public IHttpActionResult Put(ProductUpdate product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateProductService();

            if (!service.UpdateProduct(product))
                return InternalServerError();
            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateProductService();

            if (!service.DeleteProduct(id))
                return InternalServerError();
            return Ok();
        }
        private ProductService CreateProductService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var productService = new ProductService(userID);
            return productService;
        }
    }
}
