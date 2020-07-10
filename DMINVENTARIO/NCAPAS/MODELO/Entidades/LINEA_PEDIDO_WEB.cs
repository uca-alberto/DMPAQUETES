using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DMINVENTARIO.NCAPAS.MODELO.Entidades
{
	[Table("LINEA_PEDIDO_WEB")]
	public class LINEA_PEDIDO_WEB
	{
		[Key]
		public int ID { get; set; }
		public string PEDIDO { get; set; }
		public string ARTICULO { get; set; }
		public string DESCRIPCION { get; set; }
		public decimal PRECIO { get; set; }
		public decimal CANTIDAD_FACTURAR { get; set; }
		public Nullable<decimal> ESCANEADA { get; set; }
		public string LOTE { get; set; }
		public decimal DESCUENTO { get; set; }
		public decimal DESCUENTOPORCENTAJE { get; set; }
	}
}