using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMINVENTARIO.NCAPAS.ENTIDADES
{
	public class TransaccionInv
	{
		public string IdArticulo { get; set; }
		public string FechaHora { get; set; }
		public string DescripcionArticulo { get; set; }
		public string Bodega { get; set; }
		public decimal Cantidad { get; set; }
		public string Tipo { get; set; }
		public decimal CostoLocal { get; set; }
		public decimal CostoDolar { get; set; }
		public decimal CostoComparativoLocal { get; set; }
		public decimal CostoComparativoDolar { get; set; }
		public decimal PrecioLocal { get; set; }
		public decimal PrecioDolar { get; set; }

	}
}