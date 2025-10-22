using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKamp.Controllers
{
	[AllowAnonymous]
	public class DefaultController : Controller
	{
		// GET: Default
		HeadingManager hm= new HeadingManager(new EfHeadingDal());
		ContentManager cm= new ContentManager(new EfContentDal());

		public ActionResult Headings()
		{
			var headingList = hm.GetList();
			return View(headingList);
		}
		public PartialViewResult Index( int id = 0 )
		{
			//var contentList = cm.GetListByHeadingId(id);

			List<Content> contentList;

			if (id == 0)
			{
				// id yoksa rastgele içerikleri getir
				contentList = cm.GetListByHeadingId(0);
			}
			else
			{
				// id varsa ilgili başlığa ait içerikleri getir
				contentList = cm.GetListByHeadingId(id);
			}

			return PartialView(contentList);
		}
	}
}