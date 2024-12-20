using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
	public interface IRepository<T>
	{
		//Tümünü Getir
		List<T> List();

		//Şartlı Listeleme ALi olanları getir.
		List<T> List(Expression<Func<T,bool>>filter);

		//Ekleme
		void Insert(T p);

		//Güncelle
		void Update(T p);

		//Sil
		void Delete(T p);
	}
}
