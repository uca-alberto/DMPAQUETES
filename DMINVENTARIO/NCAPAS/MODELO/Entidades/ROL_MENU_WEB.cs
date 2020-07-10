using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DMINVENTARIO.NCAPAS.MODELO.Entidades
{
	[Table("ROL_MENU_WEB")]
	public class ROL_MENU_WEB
	{
		[Key]
		public int ID_ROL_MENU_WEB { get; set; }
		public int ID_ROL { get; set; }
		public int ID_MENU { get; set; }
	}
}