using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKamp.Controllers
{
    public class WriterController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
		WriterValidator validator = new WriterValidator();
		public ActionResult Index()
        {
            var values = wm.GetList();
            return View(values);
        }

        [HttpGet]
        public ActionResult AddWriter()
        {
            return View();
        }

		[HttpPost]
		public ActionResult AddWriter(Writer writer)
		{
            
            ValidationResult validationResult = validator.Validate(writer);
            if(validationResult.IsValid)
            {
                wm.WriterAdd(writer);
                return RedirectToAction("Index");
            }
            else
            {
                foreach(var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
			return View();
		}
        [HttpGet]
        public ActionResult EditWriter(int id) 
        { 
            var writervalue= wm.GetById(id);
            return View(writervalue);
        }

        [HttpPost]
		public ActionResult EditWriter(Writer writer)
		{
			ValidationResult validationResult = validator.Validate(writer);
			if (validationResult.IsValid)
			{
				wm.WriterUpdate(writer);
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

        public ActionResult WriterContent(int id)
        {
			var contentValue = hm.GetListByWriter(id);
			return View(contentValue);
		}

	}
}