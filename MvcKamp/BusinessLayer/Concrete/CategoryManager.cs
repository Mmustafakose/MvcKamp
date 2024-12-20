using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class CategoryManager
	{
		GenericRepository<Category> repo= new GenericRepository<Category> ();
		//Tüm kategorileri Listeleme
		public List<Category> GetAllBl()
		{
			return repo.List();
		}

		//Kategori Ekleme
		public void CategoryAddBl(Category p)
		{
			if(p.categoryname==""|| p.categoryname.Length<=3|| p.categoryDescription==""|| p.categoryname.Length>= 51)
			{
				//Hata Mesajı
			}
			else
			{
				repo.Insert(p);
			}
		}
	}
}
