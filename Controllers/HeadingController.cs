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
	public class HeadingController : Controller
	{
		// GET: Heading
		HeadingManager hm = new HeadingManager(new EfHeadingDal());
		CategoryManager cm = new CategoryManager(new EfCategoryDal());
		WriterManager wm = new WriterManager(new EfWriterDal());
		public ActionResult Index()
		{
			var headingValues = hm.GetList();
			return View(headingValues);
		}

		public ActionResult HeadingReport()
		{
			var headingValues = hm.GetList();
			return View(headingValues);
		}

		[HttpGet]
		public ActionResult AddHeading()
		{
			List<SelectListItem> valueCategory = (from x in cm.GetList()
												  select new SelectListItem
												  {
													  Text = x.categoryname,
													  Value = x.categoryId.ToString()
												  }).ToList();
			ViewBag.vlc = valueCategory;

			List<SelectListItem> valueWriter = (from x in wm.GetList()
												select new SelectListItem
												{
													Text = x.writerName + " " + x.writerSurname,
													Value = x.writerId.ToString()
												}).ToList();
			ViewBag.writer = valueWriter;
			return View();
		}

		[HttpPost]
		public ActionResult AddHeading(Heading heading)
		{
			heading.headingDate = DateTime.Parse(DateTime.Now.ToShortTimeString()); //Bugünün kısa tarihi
			hm.HeadingAdd(heading);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult EditHeading(int id)
		{
			List<SelectListItem> valueCategory = (from x in cm.GetList()
												  select new SelectListItem
												  {
													  Text = x.categoryname,
													  Value = x.categoryId.ToString()
												  }).ToList();
			ViewBag.vlc = valueCategory;

			var headingValue= hm.GetByID(id);
			return View(headingValue);
		}

		[HttpPost]
		public ActionResult EditHeading(Heading heading)
		{
			hm.HeadingUpdate(heading);
			return RedirectToAction("Index");
		}

		public ActionResult DeleteHeading(int id)
		{
			var headingValue = hm.GetByID(id);
			headingValue.headingStatus = false;
			hm.HeadingDelete(headingValue);
			return RedirectToAction("Index");
		}
	}
}