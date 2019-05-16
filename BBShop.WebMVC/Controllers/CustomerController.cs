using BBShop.Model.Customer;
using BBShop.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBShop.WebMVC.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var service = CreateCustomerService();
            var model = service.GetCustomer();
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
        public ActionResult Create(CustomerCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateCustomerService();

            if (service.CreateCustomer(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index", "Product");
            };

            ModelState.AddModelError("", "Note could not be created.");

            return View(model);
        }

        // GET: Read
        public ActionResult Details(string id)
        {
            var svc = CreateCustomerService();
            var model = svc.GetCustomerByID(id);

            return View(model);
        }

        // GET: Update
        public ActionResult Edit(string id)
        {
            var service = CreateCustomerService();
            var detail = service.GetCustomerByID(id);
            var model =
                new CustomerUpdate
                {
                    CustomerID = detail.CustomerID,
                    FirstName = detail.FirstName,
                    LastName = detail.LastName,
                    StreetAddress = detail.StreetAddress,
                    City = detail.City,
                    State = detail.State,
                    ZipCode = detail.ZipCode,
                    Telephone = detail.Telephone,
                    Email = detail.Email,
                    CreditCard = detail.CreditCard
                };
            return View(model);
        }

        // Post: Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, CustomerUpdate model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.CustomerID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateCustomerService();
            if (service.UpdateCustomer(model))
            {
                TempData["SaveResult"] = "Your customer was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your customer could not be updated.");
            return View(model);
        }

        // GET: Delete
        [Authorize(Roles = "Admin")]
        [ActionName("Delete")]
        public ActionResult Delete(string id)
        {
            var svc = CreateCustomerService();
            var model = svc.GetCustomerByID(id);

            return View(model);
        }

        // Post: Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(string id)
        {
            var service = CreateCustomerService();
            service.DeleteCustomer(id);
            TempData["SaveResult"] = "Your note was deleted";
            return RedirectToAction("Index");
        }
        private CustomerService CreateCustomerService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CustomerService(userId);
            return service;
        }
    }
}