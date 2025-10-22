using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace MvcKamp.Controllers
{
	
	public class AdmincategoryController : Controller
    {
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
		[Authorize(Roles ="B")]
		public ActionResult Index()
        {
            var categoryValues = cm.GetList();
            return View(categoryValues);
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            CategoryValidator validator = new CategoryValidator();
            ValidationResult validationResult = validator.Validate(category);
            if (validationResult.IsValid)
            {
                cm.CategoryAdd(category);
                return RedirectToAction("Index");
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
        public ActionResult DeleteCategory(int id)
        {
            var value= cm.GetByID(id);
            cm.CategoryDelete(value);
            return RedirectToAction("Index");
		}
        [HttpGet]
        public ActionResult EditCategory(int id) 
        {
            var value= cm.GetByID(id);
            return View(value);
        }

        [HttpPost]
		public ActionResult EditCategory(Category category)
		{
            cm.CategoryUpdate(category);
            return RedirectToAction("Index");
        }
	}
}