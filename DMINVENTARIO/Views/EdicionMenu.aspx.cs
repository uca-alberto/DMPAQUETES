using DevExpress.Web;
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
	public partial class EdicionMenu : System.Web.UI.Page
	{
		DTMenu dt = new DTMenu();
		DTUsuario dtu = new DTUsuario();
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				Session["Dtmenu"] = dt.ObtenerMenuWeb();
				ASPxGridLookupMenu.DataSource = Session["Dtmenu"];
				ASPxGridLookupMenu.DataBind();

				Session["rolLista"] = dt.ObtenerRolWeb();
				ASPxGridLookupRol.DataSource = Session["rolLista"];
				ASPxGridLookupRol.DataBind();
			}
		}


		protected void ASPxGridLookupMenu_Init(object sender, EventArgs e)
		{
			if (Session["Dtmenu"]!=null)
			{
				ASPxGridLookupMenu.DataSource = Session["Dtmenu"];
				ASPxGridLookupMenu.DataBind();
			}
		}

		//protected void ASPxGridLookupSubMenu_Init(object sender, EventArgs e)
		//{
		//	if (Session["Dtsubmenu"] != null)
		//	{
		//		ASPxGridLookupSubMenu.DataSource = Session["Dtsubmenu"];
		//		ASPxGridLookupSubMenu.DataBind();
		//	}
		//}

		protected void BtnAgregar_Click(object sender, EventArgs e)
		{
			try
			{
				ASPxGridView RolGrid = ASPxGridLookupRol.GridView;
				object RolID = RolGrid.GetRowValues(RolGrid.FocusedRowIndex, new string[] { "ID_ROL" });
				ASPxGridView MenuGrid = ASPxGridLookupMenu.GridView;
				object MenuID = MenuGrid.GetRowValues(MenuGrid.FocusedRowIndex, new string[] { "ID_MENU" });
				if (RolID == null)
				{
					string script = $@"alert('Seleccione Rol');";
					ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
					return;
				}
				if (MenuID == null)
				{
					string script = $@"alert('Seleccione Menu');";
					ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
					return;
				}
				ROL_MENU_WEB menu = new ROL_MENU_WEB();
				menu.ID_ROL = Convert.ToInt32(RolID.ToString());
				menu.ID_MENU = Convert.ToInt32(MenuID.ToString());
				dt.Insertar(menu);
				CargarGrid(Convert.ToInt32(RolID.ToString()));
			}
			catch (Exception Ex)
			{
				string script = string.Format(@"alert('{0}');", Ex.Message);
				ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
				return;
			}
			
		}

		//protected void BtnObtenerSubmenu_Click(object sender, EventArgs e)
		//{
		//	try
		//	{
		//		ASPxGridView MenuGrid = ASPxGridLookupMenu.GridView;
		//		object MenuID = MenuGrid.GetRowValues(MenuGrid.FocusedRowIndex, new string[] { "ID_MENU" });
		//		if (MenuID != null)
		//		{
		//			Session["Dtsubmenu"] = dt.ObtenerSubMenuWeb(Convert.ToInt32(MenuID.ToString()));
		//			ASPxGridLookupSubMenu.DataSource = Session["Dtsubmenu"];
		//			ASPxGridLookupSubMenu.DataBind();
		//		}
		//	}
		//	catch (Exception)
		//	{
		//		throw;
		//	}

		//}

		protected void ASPxGridLookupRol_Init(object sender, EventArgs e)
		{
			if (Session["rolLista"] !=null)
			{
				ASPxGridLookupRol.DataSource = Session["rolLista"];
				ASPxGridLookupRol.DataBind();
			}
		}

		protected void ObtenerRolMenu_Click(object sender, EventArgs e)
		{
			ASPxGridView RolGrid = ASPxGridLookupRol.GridView;
			object RolID = RolGrid.GetRowValues(RolGrid.FocusedRowIndex, new string[] { "ID_ROL" });
			if (RolID != null)
			{
				Session["Dtdetallerol"] = dt.ObtenerMenuRol(Convert.ToInt32(RolID.ToString()));
				ASPxGridViewMenu.DataSource = Session["Dtdetallerol"];
				ASPxGridViewMenu.DataBind();
			}
		}

		protected void ASPxGridViewMenu_Init(object sender, EventArgs e)
		{
			if (Session["Dtdetallerol"]!=null)
			{
				ASPxGridViewMenu.DataSource = Session["Dtdetallerol"];
				ASPxGridViewMenu.DataBind();
			}
		}

		public void CargarGrid(int rol)
		{
			Session["Dtdetallerol"] = dt.ObtenerMenuRol(rol);
			ASPxGridViewMenu.DataSource = Session["Dtdetallerol"];
			ASPxGridViewMenu.DataBind();
		}

		protected void ASPxGridViewMenu_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
		{
			ASPxGridView RolGrid = ASPxGridLookupRol.GridView;
			object RolID = RolGrid.GetRowValues(RolGrid.FocusedRowIndex, new string[] { "ID_ROL" });
			ROL_MENU_WEB menu = new ROL_MENU_WEB();
			menu.ID_ROL = Convert.ToInt32(RolID.ToString());
			menu.ID_MENU = Convert.ToInt32(e.Values["IdMenu"].ToString());

			dt.Eliminar(menu);
			CargarGrid(Convert.ToInt32(RolID.ToString()));
			ASPxGridViewMenu.CancelEdit();
			e.Cancel = true;
		}
	}
}