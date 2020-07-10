using DMINVENTARIO.NCAPAS.DATOS;
using DMINVENTARIO.NCAPAS.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DMINVENTARIO
{
	public partial class SiteMaster : MasterPage
	{
		protected IList<MENU> registro;
		protected void Page_Load(object sender, EventArgs e)
		{
			DTMenu dt = new DTMenu();
			String activo = (string)Session["Usuario"];
			if (Session["Rol"] == null)
			{
				Response.Redirect("~/Login.aspx");
			}
			int Rol = (int)Session["Rol"];
			if (!IsPostBack)
			{
				if (activo == null)
				{
					Response.Redirect("~/Login.aspx");
				}
				else
				{
					try
					{
						this.registro = dt.ObtenerMenuRol(Rol);
					}
					catch (Exception ex)
					{
						string script = string.Format(@"alert({0});",ex.Message);
						ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
						return;
					}
				}
			}
			else
			{
				this.registro = dt.ObtenerMenuRol(Rol);
			}

		}

		protected void Unnamed_ServerClick1(object sender, EventArgs e)
		{
			Session["Usuario"] = null;
			Session["Rol"] = null;
			Response.Redirect("~/Login.aspx");
		}
	}
}