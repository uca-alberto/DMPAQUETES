using DMINVENTARIO.NCAPAS.DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DMINVENTARIO
{
	public partial class _Default : Page
	{
		DTMenu dt = new DTMenu();
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["Rol"]!=null)
			{
				int rol = Convert.ToInt32(Session["Rol"]);
				dt.ObtenerMenuRol(rol);
			}
			else
			{
				Response.Redirect("~/Login.aspx");
			}
		}
	}
}