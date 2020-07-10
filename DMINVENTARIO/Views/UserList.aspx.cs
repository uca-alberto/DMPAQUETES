using DMINVENTARIO.NCAPAS.DATOS;
using DMINVENTARIO.NCAPAS.MODELO.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DMINVENTARIO.Views
{
	public partial class UserList : System.Web.UI.Page
	{
		DTUsuario dt = new DTUsuario();
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				cargarusuarios();
			}
		}
		public void cargarusuarios()
		{
			ASPxGridView1.DataSource = dt.listarTodo();
			ASPxGridView1.DataBind();
		}

		protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
		{
			USUARIO_WEB User = new USUARIO_WEB();
			User.ID_USUARIO = Convert.ToInt32(e.NewValues["ID_USUARIO"].ToString());
			User.USUARIO = e.NewValues["USUARIO"].ToString();
			User.PASS = e.NewValues["PASS"].ToString();
			User.ID_ROL = Convert.ToInt32(e.NewValues["ID_ROL"].ToString());
			if (dt.Modificar(User))
			{
				ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: Insertar(); ", true);
			}
			else
			{
				string script = $@"alert('Error al Insertar');";
				ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
			}
			ASPxGridView1.CancelEdit();
			e.Cancel = true;
			cargarusuarios();
		}

		protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
		{
			USUARIO_WEB User = new USUARIO_WEB();
			User.ID_USUARIO = Convert.ToInt32(e.Values["ID_USUARIO"].ToString());
			if (dt.Eliminar(User))
			{
				ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: Eliminar(); ", true);
			}
			else
			{
				string script = $@"alert('Error al Eliminar');";
				ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
			}
			ASPxGridView1.CancelEdit();
			e.Cancel = true;
			cargarusuarios();
		}

		protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
		{
			USUARIO_WEB User = new USUARIO_WEB();
			User.USUARIO = e.NewValues["USUARIO"].ToString();
			User.PASS = e.NewValues["PASS"].ToString();
			User.ID_ROL = Convert.ToInt32(e.NewValues["ID_ROL"].ToString());
			if (dt.Insertar(User))
			{
				ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: Insertar(); ", true);
			}
			else
			{
				string script = $@"alert('Error al Insertar');";
				ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
			}
			ASPxGridView1.CancelEdit();
			e.Cancel = true;
			cargarusuarios();
		}
	}
}