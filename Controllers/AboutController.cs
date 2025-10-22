using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKamp.Controllers
{
    public class AboutController : Controller
    {
        // GET: About

        AboutManager abm= new AboutManager(new EfAboutDal());

        public ActionResult Index()
        {
            var aboutValue = abm.GetList();
            return View(aboutValue);
        }

        [HttpPost]
        public ActionResult AddAbout(About about) 
        {
            abm.AboutAdd(about);
            return RedirectToAction("Index");
        }

        public PartialViewResult AboutPartial()
        {
            return PartialView();
        }
    }
}