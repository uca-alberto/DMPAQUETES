using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DMINVENTARIO.NCAPAS.MODELO.Entidades
{
	[Table("CONSECUTIVO_PEDIDO_WEB")]
	public class CONSECUTIVO_WEB
	{
		[Key]
		public int ID { get; set; }
		public string CONSECUTIVO { get; set; }
	}
}