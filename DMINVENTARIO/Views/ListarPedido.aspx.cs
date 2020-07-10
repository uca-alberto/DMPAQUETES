using DMINVENTARIO.NCAPAS;
using DMINVENTARIO.NCAPAS.MODELO.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DMINVENTARIO.Views
{
	public partial class ListarPedido : System.Web.UI.Page
	{
		DTPedido dt = new DTPedido();
		string compani = ConfigurationManager.AppSettings["Compani"];
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				CargarGrid();
			}
			if (Session["Rol"] == null)
			{
				Response.Redirect("~/Login.aspx");
			}
		}

		#region Carga de Datos

		public void CargarGrid()
		{
			try
			{
				var Lista = dt.listarTransacciones(compani);
				Session["LISTA_PEDIDOS"] = Lista;
				GridListaPedido.DataSource = Session["LISTA_PEDIDOS"];
				GridListaPedido.DataBind();
			}
			catch (Exception EX)
			{

				throw EX;
			}
		}
		#endregion

		#region Init
		protected void GridListaPedido_Init(object sender, EventArgs e)
		{
			if (Session["LISTA_PEDIDOS"] != null)
			{
				GridListaPedido.DataSource = Session["LISTA_PEDIDOS"];
				GridListaPedido.DataBind();
			}
		}
		#endregion

		#region Eventos
		protected void GridListaPedido_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
		{
			if (e.ButtonID == "Edit")
			{
				if (IsPostBack)
				{
					dynamic keyValue = GridListaPedido.GetRowValues(e.VisibleIndex, GridListaPedido.KeyFieldName);
					//UpdateComponentCost(keyValue);
					DevExpress.Web.ASPxWebControl.RedirectOnCallback("EditarPedido.aspx?id=" + keyValue);
				}
			}
		}

		protected void GridListaPedido_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
		{
			CABECERA_PEDIDO_WEB Cabecera = new CABECERA_PEDIDO_WEB();
			Cabecera.CONSECUTIVO = e.Values["CONSECUTIVO"].ToString();
			if (dt.Eliminar(Cabecera))
			{
				ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: Eliminar(); ", true);
			}
			else
			{
				string script = $@"alert('Error al Eliminar');";
				ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
			}
			GridListaPedido.CancelEdit();
			e.Cancel = true;
			CargarGrid();
		}
		#endregion
	}
}