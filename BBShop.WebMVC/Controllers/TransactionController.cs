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
        TransactionService _service = new TransactionService();
        // GET: Transaction
        public ActionResult Index()
        {
            var model = _service.GetTrans();
            return View(model);
        }
        // GET: Transaction/Create
        public ActionResult Create()
        {
            ProductService prodSvc;
            CustomerService custSvc;
            ViewInfo(out prodSvc, out custSvc);
            ViewBag.CustomerID = new SelectList(custSvc.GetCustomer(), "CustomerID", "FullName");
            ViewBag.ProductID = new SelectList(prodSvc.GetProducts(), "ProductID", "ProductName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionCreate model)
        {
            if (!ModelState.IsValid) return View(model);


            if (_service.CreateTrans(model))
            {
                TempData["SaveResult"] = "Your transaction was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Transaction could not be created.");

            return View(model);
        }
        public ActionResult Details(int id)
        {
            var model = _service.GetTransByID(id);

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var detail = _service.GetTransByID(id);
            var model =
                new TransactionUpdate
                {
                    TransactionID = detail.TransactionID,
                    FullName = detail.FullName,
                    ProductName = detail.ProductName
                };
            ProductService prodSvc;
            CustomerService custSvc;
            ViewInfo(out prodSvc, out custSvc);
            ViewBag.CustomerID = new SelectList(custSvc.GetCustomer(), "CustomerID", "FullName");
            ViewBag.ProductID = new SelectList(prodSvc.GetProducts(), "ProductID", "ProductName");
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
            if (_service.UpdateTrans(model))
            {
                TempData["SaveResult"] = "Your transaction was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your transaction could not be updated.");
            return View(model);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var model = _service.GetTransByID(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            _service.DeleteTrans(id);
            TempData["SaveResult"] = "Your transaction was deleted";
            return RedirectToAction("Index");
        }
        private void ViewInfo(out ProductService prodSvc, out CustomerService custSvc)
        {
            prodSvc = new ProductService(Guid.Parse(User.Identity.GetUserId()));
            custSvc = new CustomerService(Guid.Parse(User.Identity.GetUserId()));
        }
    }
}