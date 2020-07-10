using DMINVENTARIO.NCAPAS.DATOS;
using DMINVENTARIO.NCAPAS.MODELO.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DMINVENTARIO
{
	public partial class Login : System.Web.UI.Page
	{
		DTUsuario dt = new DTUsuario();
		USUARIO_WEB UsuarioWeb = new USUARIO_WEB();
		protected void Page_Load(object sender, EventArgs e)
		{

		}
		protected void BtnLogin_Click(object sender, EventArgs e)
		{
			try
			{
				if (dt.Login(Usuario.Text, Contraseña.Text,ref UsuarioWeb))
				{
					Session["Usuario"] = UsuarioWeb.USUARIO;
					Session["Rol"] = UsuarioWeb.ID_ROL;
					Response.Redirect("~/Views/Default.aspx");

				}
				else
				{
					ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: Validacion(); ", true);
				}
			}
			catch (Exception Ex)
			{
				Error.Text = Ex.Message;
				Error.Visible = true;
				string script = string.Format(@"alert('{0}');", Ex.Message);
				ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
				return;
			}

		}
	}
}