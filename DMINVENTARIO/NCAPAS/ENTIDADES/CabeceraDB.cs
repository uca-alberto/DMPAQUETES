using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMINVENTARIO.NCAPAS.ENTIDADES
{
	public class CabeceraDB
	{
		public int ID { get; set; }
		public string CONSECUTIVO { get; set; }
		public string BODEGA { get; set; }
		public string CLIENTE { get; set; }
		public string NombreCliente { get; set; }
		public string MONEDA { get; set; }
		public string USUARIO { get; set; }
		public DateTime FECHA_CREACION { get; set; }
		public string USUARIO_MODIFICA { get; set; }
		public DateTime FECHA_MODIFICA { get; set; }
		public string ESTADO { get; set; }
		public decimal TOTAL_LIBRAS { get; set; }
		public decimal TOTAL_PEDIDO { get; set; }
		public string COMPANI { get; set; }

	}
}