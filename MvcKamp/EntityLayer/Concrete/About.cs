using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
	public class About
	{
		[Key]
		public int aboutId { get; set; }
		[StringLength(1000)]
		public string aboutDetails1 { get; set; }
		[StringLength(1000)]
		public string aboutDetails2 { get; set; }
		[StringLength(100)]
		public string aboutImage1 {  get; set; }
		[StringLength(100)]
		public string aboutImage2 {  get; set; }
	}
}
