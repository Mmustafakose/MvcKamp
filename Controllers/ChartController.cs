using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using MvcKamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKamp.Controllers
{
    public class ChartController : Controller
    {
		Context c = new Context();
        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult CategoryChart()
		{
			return Json(BlogList(), JsonRequestBehavior.AllowGet);
		}

		public ActionResult KategoriChart()
		{
			return Json(CategoryList(), JsonRequestBehavior.AllowGet);
		}

		public List<CategoryClass> CategoryList()
		{
			List<CategoryClass> categories = new List<CategoryClass>();

			categories = c.Categories.Select(x => new CategoryClass
			{
				CategoryName = x.categoryname,
				CategoryCount = x.headings.Count() // Her kategorideki başlık sayısı
			}).ToList();

			return categories;
		}

		public List<CategoryClass> BlogList()
        {
            List<CategoryClass> categories = new List<CategoryClass>();
            categories.Add(new CategoryClass()
            {
                CategoryName = "Yazılım",
                CategoryCount = 8
            });

			categories.Add(new CategoryClass()
			{
				CategoryName = "Seyehat",
				CategoryCount = 4
			});

			categories.Add(new CategoryClass()
			{
				CategoryName = "Teknoloji",
				CategoryCount = 7
			});

			categories.Add(new CategoryClass()
			{
				CategoryName = "Spor",
				CategoryCount = 1
			});

			return categories; 
		}


    }
}