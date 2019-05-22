using BBShop.Model.Transaction;
using BBShop.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBShop.WebMVC.Controllers
{
    public class TransactionController : Controller
    {
        CustomerService _custSvc;
        ProductService _prodSvc;
        TransactionService _tranSvc;
        // GET: Transaction
        [Authorize]
        public ActionResult Index()
        {
            var service = GetServices();

            var model = service.GetTrans();
            return View(model);
        }
        // GET: Transaction/Create
        [Authorize]
         public ActionResult Create()
        {
            _tranSvc = GetServices();
            _custSvc = GetCustomerService();
            _prodSvc = GetProductService();
            ViewBag.CustomerID = new SelectList(_custSvc.GetCustomer(), "CustomerID", "FullName");
            ViewBag.ProductID = new SelectList(_prodSvc.GetProducts(), "ProductID", "ProductName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            _tranSvc = GetServices();
            if (_tranSvc.CreateTrans(model))
            {
                TempData["SaveResult"] = "Your transaction was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Transaction could not be created.");

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            _tranSvc = GetServices();
            var model = _tranSvc.GetTransByID(id);

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            _tranSvc = GetServices();
            var detail = _tranSvc.GetTransByID(id);
            var model =
                new TransactionUpdate
                {
                    TransactionID = detail.TransactionID,
                    FullName = detail.FullName,
                    ProductName = detail.ProductName
                };
            _prodSvc = GetProductService();
            _custSvc = GetCustomerService();
            ViewBag.CustomerID = new SelectList(_custSvc.GetCustomer(), "CustomerID", "FullName");
            ViewBag.ProductID = new SelectList(_prodSvc.GetProducts(), "ProductID", "ProductName");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TransactionUpdate model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.TransactionID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            _tranSvc = GetServices();
            if (_tranSvc.UpdateTrans(model))
            {
                TempData["SaveResult"] = "Your transaction was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your transaction could not be updated.");
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            _tranSvc = GetServices();
            var model = _tranSvc.GetTransByID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            _tranSvc = GetServices();
            _tranSvc.DeleteTrans(id);
            TempData["SaveResult"] = "Your transaction was deleted";
            return RedirectToAction("Index");
        }

        private TransactionService GetServices()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var isAdmin = User.IsInRole("Admin");
            var _tranSvc = new TransactionService(userId, isAdmin);
            return _tranSvc;
        }
        private CustomerService GetCustomerService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            
            var serviceCust = new CustomerService(userId);
            return serviceCust;
        }
        private ProductService GetProductService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());

            var serviceProd = new ProductService(userId);
            return serviceProd;
        }
    }
}