using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
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
	public class WriterPanelMessageController : Controller
	{
		MessageManager mm = new MessageManager(new EfMessageDal());
		MessageValidator validator = new MessageValidator();

		// GET: WriterPanelMessage
		public ActionResult Inbox()
		{
			string p = (string)Session["WriterMail"];
			var messageValues = mm.GetListInBox(p);
			return View(messageValues);
		}

		public ActionResult SendBox()
		{
			string p = (string)Session["WriterMail"];
			var messageList = mm.GetListSendBox(p);
			return View(messageList);
		}

		public ActionResult GetInBoxMessageDetails(int id)
		{
			var value = mm.GetByID(id);
			return View(value);
		}
		public ActionResult GetSendBoxMessageDetails(int id)
		{
			var value = mm.GetByID(id);
			return View(value);
		}

		public PartialViewResult MessageListMenu()
		{
			return PartialView();
		}

		[HttpGet]
		public ActionResult NewMessage()
		{
			return View();
		}

		[HttpPost]
		public ActionResult NewMessage(Message p)
		{
			ValidationResult validationResult = validator.Validate(p);
			if (validationResult.IsValid)
			{
				string s = (string)Session["WriterMail"];
				p.senderMail =s;
				p.messageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
				mm.MessageAdd(p);
				return RedirectToAction("SendBox");
			}
			else
			{
				foreach (var item in validationResult.Errors)
				{
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}
			}
			return View(p);
		}
	}
}