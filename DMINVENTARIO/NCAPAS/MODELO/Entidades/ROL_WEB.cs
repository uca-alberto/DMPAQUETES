using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DMINVENTARIO.NCAPAS.MODELO.Entidades
{
	[Table("ROL_WEB")]
	public class ROL_WEB
	{
		[Key]
		public int ID_ROL { get; set; }
		public string ROL { get; set; }
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public DateTime? FECHA_CREACION { get; set; }
		public DateTime? FECHA_MODIFICACION { get; set; }
	}
}