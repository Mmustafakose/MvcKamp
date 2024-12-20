using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
	public class Content
	{
		[Key]
		public int contentId {  get; set; }
		[StringLength(1000)]
		public string convertValue { get; set; }
		public DateTime contentDAte { get; set; }
		public int headingId { get; set; }
		public virtual Heading Heading { get; set; }

		public int?  writerId { get; set; }
		public virtual Writer Writer { get; set; }
	}
}
