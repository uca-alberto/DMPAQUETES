using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMINVENTARIO.NCAPAS.ENTIDADES
{
	public class TraspasoInv
	{
		public string PAQUETE_INVENTARIO { get; set; }
		public string DOCUMENTO_INV { get; set; }
		public string CONSECUTIVO { get; set; }
		public string REFERENCIA { get; set; }
		public string MENSAJE_SISTEMA { get; set; }
		public DateTime FECHA_DOCUMENTO { get; set; }
		public string TIPOTRAN { get; set; }
		public List<TraspasoInvLinea> traspasolinea { get; set; }
	}
}