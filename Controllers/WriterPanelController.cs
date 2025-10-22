using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using FluentValidation.Results;
using BusinessLayer.ValidationRules;

namespace MvcKamp.Controllers
{
	public class WriterPanelController : Controller
	{
		HeadingManager hm = new HeadingManager(new EfHeadingDal());
		CategoryManager cm = new CategoryManager(new EfCategoryDal());
		WriterManager wm = new WriterManager(new EfWriterDal());
		


		// GET: WriterPanel
		[HttpGet]
		public ActionResult WriterProfile()
		{
			int p;
			p = (int)Session["writerId"];
			ViewBag.d = p;
			var writerValue= wm.GetById(p);
			return View(writerValue);
		}
		[HttpPost]
		public ActionResult WriterProfile(Writer writer)
		{
			WriterValidator validationRules = new WriterValidator();
			ValidationResult validationResult = validationRules.Validate(writer);
			if (validationResult.IsValid)
			{
				wm.WriterUpdate(writer);
				return RedirectToAction("WriterProfile");
			}
			else
			{
				foreach (var item in validationResult.Errors)
				{
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}
			}
			return View();
		}

		public ActionResult MyHeading()
		{
			int id;
			id = (int)Session["writerId"];
			ViewBag.d = id;
			var values = hm.GetListByWriter(id);
			return View(values);
		}

		[HttpGet]
		public ActionResult NewHeading()
		{
			List<SelectListItem> valueCategory = (from x in cm.GetList()
												  select new SelectListItem
												  {
													  Text = x.categoryname,
													  Value = x.categoryId.ToString()
												  }).ToList();
			ViewBag.vlc = valueCategory;
			return View();
		}

		[HttpPost]
		public ActionResult NewHeading(Heading heading)
		{
			heading.headingDate = DateTime.Parse(DateTime.Now.ToShortTimeString()); //Bugünün kısa tarihi
			int id;
			id = (int)Session["writerId"];
			heading.writerId = id;
			heading.headingStatus = true;
			hm.HeadingAdd(heading);
			return RedirectToAction("MyHeading");
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

			var headingValue = hm.GetByID(id);
			return View(headingValue);
		}

		[HttpPost]
		public ActionResult EditHeading(Heading heading)
		{
			hm.HeadingUpdate(heading);
			return RedirectToAction("MyHeading");
		}

		public ActionResult DeleteHeading(int id) 
		{
			var headingValue = hm.GetByID(id);
			headingValue.headingStatus = false;
			hm.HeadingDelete(headingValue);
			return RedirectToAction("MyHeading");
		}
		public ActionResult AllHeading(int p=1)
		{
			var headings = hm.GetList().ToPagedList(p,10);
			return View(headings);
		}
	}
}