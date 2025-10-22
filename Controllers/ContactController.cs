using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKamp.Controllers
{
    public class ContactController : Controller
    {
		// GET: Contact
		MessageManager mm = new MessageManager(new EfMessageDal());
		ContactManager cm = new ContactManager(new EfContactDal());
        ContactValidator cv=new ContactValidator();

        
        public ActionResult Index()
        {
            var contactValue = cm.GetList();
            return View(contactValue);
        }

        public ActionResult GetContactDetails(int id) 
        {
            var contactValue= cm.GetByID(id);
            return View(contactValue);
        }

		public PartialViewResult MessageListMenu()
		{
			string p = (string)@Session["AdminUserName"];

			int contactValue = cm.GetList().Count;
            ViewBag.ContactValue = contactValue;

			int inboxCount =mm.GetListInBox(p).Count;
			ViewBag.inboxCount = inboxCount;

			int sendbox= mm.GetListSendBox(p).Count();
            ViewBag.sendbox = sendbox;
			return PartialView();
		}
	}
}