using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentValidation.Results;

namespace MvcKamp.Controllers
{
	public class CategoryController : Controller
	{
		CategoryManager cm = new CategoryManager(new EfCategoryDal());
		// GET: Category


		public ActionResult Index()
		{
			return View();
		}

		public ActionResult GetCatagoryList()
		{
			var sonuc = cm.GetList();
			return View( sonuc);
		}
		[HttpGet]
		public ActionResult AddCategory()
		{
			return View();
		}

		[HttpPost]
		public ActionResult AddCategory(Category p)
		{
			//cm.CategoryAddBl(p);
			CategoryValidator categoryValidator = new CategoryValidator();
			ValidationResult results= categoryValidator.Validate(p); //Doğruluğunu kotrol etmek için içerisine gönderilir.
			if (results.IsValid) 
			{
				cm.CategoryAdd(p);
				return RedirectToAction("GetCatagoryList");
			}
			else
			{
				foreach (var item in results.Errors)
				{
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}
				return View();
			}
		}
	}
}