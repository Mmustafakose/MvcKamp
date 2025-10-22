using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKamp.Controllers
{
    public class ContentController : Controller
    {

        ContentManager cm=new ContentManager(new EfContentDal());
        // GET: Content
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllContent(string p)
        {
            var values = cm.GetList(p);
            //var values = c.Contents.ToList();
            
			return View(values);
        }

		public ActionResult ContentByHeading(int id) //Başlığa göre içerik getir
		{
            var contentValue= cm.GetListByHeadingId(id);
			return View(contentValue);
		}
	}
}