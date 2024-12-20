using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
	public class Category
	{
		[Key]
		public int categoryId { get; set; }
		[StringLength(50)]
		public string  categoryname { get; set; }
		[StringLength(200)]
		public string categoryDescription { get; set; }
		public bool categoryStatus { get; set; }//aktif ya da pasif yapmak için kullanılır

		public ICollection<Heading> headings { get; set; }
	}
}


