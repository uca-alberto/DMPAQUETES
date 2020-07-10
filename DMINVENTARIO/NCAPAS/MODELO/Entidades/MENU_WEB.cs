using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DMINVENTARIO.NCAPAS.MODELO.Entidades
{
	[Table("MENU_WEB")]
	public class MENU_WEB
	{
		[Key]
		public int ID_MENU { get; set; }
		public string DESCRIPCION { get; set; }
	}
}