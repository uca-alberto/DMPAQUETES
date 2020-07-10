using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMINVENTARIO.NCAPAS.ENTIDADES
{
	public class Articulo
	{
		public string IdArticulo { get; set; }
		public string Descripcion { get; set; }
		public decimal CostoLocalFiscal { get; set; }
		public decimal CostoLocalComparativo { get; set; }
		public decimal CostoDolarFiscal { get; set; }
		public decimal CostoDolarComparativo { get; set; }
		public List<TransaccionInv> TransaccionInv { get; set; }
		public List<ExistenciaBodega> ExistenciaBodega { get; set; }
		public ArticuloPrecio ArticuloPrecio { get; set; }
	}
}