using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMINVENTARIO.NCAPAS.ENTIDADES
{
	public class TraspasoInvLinea
	{
		public string PAQUETE_INVENTARIO { get; set; }
		public string DOCUMENTO_INV { get; set; }
		public int LINEA_DOC_INV { get; set; }
		public string AJUSTE_CONFIG { get; set; }
		public string ARTICULO { get; set; }
		public string ARTICULODESCRIPCION { get; set; }
		public string BODEGA { get; set; }
		public string TIPO { get; set; }
		public string SUBTIPO { get; set; }
		public string SUBSUBTIPO { get; set; }
		public decimal CANTIDAD { get; set; }
		public decimal COSTO_TOTAL_LOCAL { get; set; }
		public decimal COSTO_TOTAL_DOLAR { get; set; }
		public decimal PRECIO_TOTAL_LOCAL { get; set; }
		public decimal PRECIO_TOTAL_DOLAR { get; set; }
		public string BODEGA_DESTINO { get; set; }
	}
}