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
	[Authorize]
	public class MessageController : Controller
	{
		// GET: Message
		MessageManager mm = new MessageManager(new EfMessageDal());
		MessageValidator validator = new MessageValidator();
		[HttpGet]
		public ActionResult Inbox()
		{
			string p = (string)@Session["AdminUserName"];
			var messageValues = mm.GetListInBox(p);
			return View(messageValues);
		}

		[HttpPost]
		public ActionResult Inbox(string p )
		{
			string a= (string)@Session["AdminUserName"];
			var messageValues = mm.GetListInBox(a);
			if (!string.IsNullOrEmpty(p))
			{
				string lowerSearch = p.ToLower();
				messageValues = messageValues
					.Where(x =>
						x.subject.ToLower().Contains(lowerSearch) ||
						x.senderMail.ToLower().Contains(lowerSearch) ||
						x.messageContent.ToLower().Contains(lowerSearch))
					.ToList();
			}
			return View(messageValues);
		}

		[HttpGet]	
		public ActionResult SendBox()
		{
			string p = (string)@Session["AdminUserName"];
			var messageList = mm.GetListSendBox(p);
			return View(messageList);
		}

		[HttpPost]
		public ActionResult Sendbox(string p)
		{
			string a = (string)@Session["AdminUserName"];
			var messageValues = mm.GetListSendBox(a);
			if (!string.IsNullOrEmpty(p))
			{
				string lowerSearch = p.ToLower();
				messageValues = messageValues
					.Where(x =>
						x.subject.ToLower().Contains(lowerSearch) ||
						x.senderMail.ToLower().Contains(lowerSearch) ||
						x.messageContent.ToLower().Contains(lowerSearch))
					.ToList();
			}
			return View(messageValues);
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
				p.senderMail= (string)@Session["AdminUserName"];
				p.messageDate =DateTime.Parse( DateTime.Now.ToShortDateString());
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