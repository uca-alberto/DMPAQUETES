using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DMINVENTARIO.NCAPAS.MODELO.Entidades
{
	[Table("USUARIO_WEB")]
	public class USUARIO_WEB
	{
		[Key]
		public int ID_USUARIO { get; set; }
		public string USUARIO { get; set; }
		public string PASS { get; set; }
		public string NOMBRE_USUARIO { get; set; }
		public int ID_ROL { get; set; }
	}
}