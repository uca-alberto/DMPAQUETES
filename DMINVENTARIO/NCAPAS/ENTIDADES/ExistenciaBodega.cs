using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMINVENTARIO.NCAPAS.ENTIDADES
{
	public class ExistenciaBodega
	{
		public string IdArticulo { get; set; }
		public string ArticuloDescripcion { get; set; }
		public string Bodega { get; set; }
		public decimal CantidadDisponible { get; set; }
	}
}