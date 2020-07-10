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
	public partial class RolList : System.Web.UI.Page
	{
		DTRol dt = new DTRol();
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				cargarrol();
			}
		}

		public void cargarrol()
		{
			ASPxGridView1.DataSource = dt.listarTodo();
			ASPxGridView1.DataBind();
		}
		protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
		{
			ROL_WEB Rol = new ROL_WEB();
			Rol.ID_ROL = Convert.ToInt32(e.NewValues["ID_ROL"].ToString());
			Rol.ROL = e.NewValues["ROL"].ToString();
			Rol.FECHA_MODIFICACION = DateTime.Now;
			if (dt.Modificar(Rol))
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
			cargarrol();
		}

		protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
		{
			ROL_WEB Rol = new ROL_WEB();
			Rol.ID_ROL = Convert.ToInt32(e.Values["ID_ROL"].ToString());
			if (dt.Eliminar(Rol))
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
			cargarrol();
		}

		protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
		{
			ROL_WEB Rol = new ROL_WEB();
			Rol.ROL = e.NewValues["ROL"].ToString();
			Rol.FECHA_CREACION = DateTime.Now;
			if (dt.Insertar(Rol))
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
			cargarrol();
		}
	}
}