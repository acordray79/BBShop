using BBShop.Model.Admin;
using BBShop.Service;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBShop.WebMVC.Controllers
{
    public class AdminController : Controller
    {
        AdminService _service = new AdminService();
        // GET: Admin
        public ActionResult Index()
        {
            var model = _service.GetCustomer();
            return View(model);
        }
        // GET: Create
        public ActionResult Create()
        {
            CustomerService custSvc = new CustomerService(Guid.Parse(User.Identity.GetUserId()));
            AdminService identSvc = new AdminService();
            ViewBag.CustomerID = new SelectList(custSvc.GetCustomer(), "CustomerID", "FullName");
            ViewBag.Id = new SelectList(identSvc.GetRole(), "Id", "Name");
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdminCreate model)
        {
            if (!ModelState.IsValid) return View(model);


            if (_service.CreateAdmin(model))
            {
                TempData["SaveResult"] = "Admin was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Admin could not be created.");

            return View(model);
        }
        public ActionResult Details(string id)
        {
            var model = _service.GetAdminByID(id);

            return View(model);
        }
        public ActionResult Edit(string id)
        {
            var detail = _service.GetAdminByID(id);
            var model =
                new AdminUpdate
                {
                    CustomerID = detail.UserID,
                    RoleID = detail.RoleID
                };
            CustomerService custSvc = new CustomerService(Guid.Parse(User.Identity.GetUserId()));
            AdminService identSvc = new AdminService();
            ViewBag.CustomerID = new SelectList(custSvc.GetCustomer(), "CustomerID", "FullName");
            ViewBag.Id = new SelectList(identSvc.GetRole(), "Id", "Name");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( AdminUpdate model)
        {
            if (!ModelState.IsValid) return View(model);

            //if (model.AdminID != id)
            //{
            //    ModelState.AddModelError("", "Id Mismatch");
            //    return View(model);
            //}
            if (_service.UpdateAdmin(model))
            {
                TempData["SaveResult"] = "Your customer was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your customer could not be updated.");
            return View(model);
        }
        [Authorize(Roles = "Manager")]
        [ActionName("Delete")]
        public ActionResult Delete(string id)
        {
            var model = _service.GetAdminByID(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {

            _service.DeleteAdmin(id);
            TempData["SaveResult"] = "Your note was deleted";
            return RedirectToAction("Index");
        }
        
    }
}