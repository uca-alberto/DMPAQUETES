using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMINVENTARIO.NCAPAS.ENTIDADES
{
	public class MENU
	{
		public int IdMenu { get; set; }
		public string Descripcion { get; set; }
		public List<SUBMENU> ListSubMenu { get; set; }
	}
}