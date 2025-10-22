using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcKamp.Controllers
{
	[AllowAnonymous]
    public class LoginController : Controller
    {
        AdminManager am = new AdminManager(new EFAdminDal());
		WriterLoginManager wm = new WriterLoginManager(new EfWriterDal());
		Context c = new Context();
		// GET: Login
		[HttpGet]
        public ActionResult Index()
        {
            //var adminValues = am.GetList();
            //return View(adminValues);

            return View();
        }

		[HttpPost]
		public ActionResult Index(Admin p)
		{
			var adminUserInfo = c.Admins
				.FirstOrDefault(x => x.adminUserName == p.adminUserName && x.adminPassword == p.adminPassword);

			if (adminUserInfo != null)
			{
				FormsAuthentication.SetAuthCookie(adminUserInfo.adminUserName, false);
				Session["AdminUserName"] = adminUserInfo.adminUserName;
				Session["WriterMail"] = adminUserInfo.adminUserName;

				return RedirectToAction("Index", "AdminCategory");
			}
			else
			{
				TempData["LoginError"] = "Kullanıcı adı veya şifre hatalı!";
				return RedirectToAction("Index", "Login");
			}
		}
		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			Session.Abandon();
			return RedirectToAction("Index", "Login");
		}

		[HttpGet]
        public ActionResult WriterLogin() 
        {
            return View();
        }

		[HttpPost]
		public ActionResult WriterLogin(Writer p)
		{

			//var writerUserInfo = c.Writers
			//	.FirstOrDefault(x => x.writerMail == p.writerMail && x.writerPassword == p.writerPassword);

			var writerUserInfo = wm.GetWriter(p.writerMail, p.writerPassword);


			if (writerUserInfo != null)
			{
				FormsAuthentication.SetAuthCookie(writerUserInfo.writerMail, false);

				// 🔥 writerId'yi Session'a kaydediyoruz
				Session["writerId"] = writerUserInfo.writerId;
				Session["writerMail"] = writerUserInfo.writerMail;
				
				return RedirectToAction("WriterProfile", "WriterPanel");
			}
			else
			{
				TempData["LoginError"] = "Kullanıcı adı veya şifre hatalı!";
				return View();
			}
		}
		public ActionResult WriterLogout()
		{
			FormsAuthentication.SignOut();
			Session.Abandon();
			return RedirectToAction("Headings", "Default");
		}
	}
}