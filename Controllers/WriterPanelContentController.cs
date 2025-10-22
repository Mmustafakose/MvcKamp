using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKamp.Controllers
{
    public class WriterPanelContentController : Controller
    {
		ContentManager cm = new ContentManager(new EfContentDal());
		// GET: WriterPanelContent
		public ActionResult MyContent()
        {
			int id;

			id =(int) Session["writerId"];

			if (Session["writerId"] == null)
			{
				return RedirectToAction("WriterLogin", "Login");
			}

			var contentValue = cm.GetListByWriterId(id);
			return View(contentValue);

		}
		[HttpGet]
		public ActionResult AddContent(int id)
		{ 
			ViewBag.d = id;
			return View();
		}

		[HttpPost]
		public ActionResult AddContent(Content content)
		{
			content.contentDAte=DateTime.Parse(DateTime.Now.ToShortDateString());
			content.writerId= (int)Session["writerId"];
			content.contentStatus = true;
			cm.ContentAdd(content);
			return RedirectToAction("MyContent");
		}

		public ActionResult ToDoList()
		{
			return View();
		}
	}
}