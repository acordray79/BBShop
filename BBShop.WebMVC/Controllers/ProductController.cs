using BBShop.Model.Product;
using BBShop.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBShop.WebMVC.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            var service = CreateProductService();
            var model = service.GetProducts();
            return View(model);
        }
        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateProductService();

            if (service.CreateProduct(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Note could not be created.");

            return View(model);
        }
        public ActionResult Details(int id)
        {
            var svc = CreateProductService();
            var model = svc.GetProductByID(id);

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateProductService();
            var detail = service.GetProductByID(id);
            var model =
                new ProductUpdate
                {
                    ProductID = detail.ProductID,
                    ProductName = detail.ProductName,
                    Price = detail.Price
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductUpdate model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ProductID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateProductService();
            if (service.UpdateProduct(model))
            {
                TempData["SaveResult"] = "Your note was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your note could not be updated.");
            return View(model);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateProductService();
            var model = svc.GetProductByID(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateProductService();
            service.DeleteProduct(id);
            TempData["SaveResult"] = "Your note was deleted";
            return RedirectToAction("Index");
        }
        private ProductService CreateProductService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ProductService(userId);
            return service;
        }
    }
}