using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKamp.Controllers
{
    public class AutohorizationController : Controller
    {
        AdminManager am= new AdminManager(new EFAdminDal());
        // GET: Autohorization
        public ActionResult Index()
        {
            var adminValues = am.GetList();
            return View(adminValues);
        }


		[HttpGet]
		public ActionResult AddAdmin()
		{
			return View();
		}

		[HttpPost]
		public ActionResult AddAdmin(Admin admin)
		{
			am.AdminAdd(admin);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult EditAdmin(int id)
		{
			var value = am.GetByID(id);
			return View(value);
		}

		[HttpPost]
		public ActionResult EditAdmin(Admin admin)
		{
			am.AdminUpdate(admin);
			return RedirectToAction("Index");
		}
	}
}