using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMINVENTARIO.NCAPAS.ENTIDADES
{
	public class Cliente
	{
		public string CLIENTE { get; set; }
		public string NOMBRE { get; set; }
		public string MONEDA_NIVEL { get; set; }
		public string NIVEL_PRECIO { get; set; }
		public string CONDICION_PAGO { get; set; }
		public string PAIS { get; set; }
		public string ALIAS { get; set; }
		public string DIRECCION { get; set; }
	}
}