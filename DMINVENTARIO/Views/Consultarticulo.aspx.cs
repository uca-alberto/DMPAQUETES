using DMINVENTARIO.NCAPAS.DATOS;
using DMINVENTARIO.NCAPAS.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DMINVENTARIO.Views
{
	public partial class Consultarticulo : System.Web.UI.Page
	{
		DTArticulo dt = new DTArticulo();
		string compani = ConfigurationManager.AppSettings["Compani"];
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				Session["Trans"] = null;
				Session["Exis"] = null;
				DateInicial.Date = DateTime.Now;
				DateFinal.Date = DateTime.Now;

			}
		}

		protected void ASPxGridViewTransaccion_Init(object sender, EventArgs e)
		{
			if (Session["Trans"] != null)
			{
				ASPxGridViewTransaccion.DataSource = Session["Trans"];
				ASPxGridViewTransaccion.DataBind();
			}
		}

		protected void ASPxGridViewExistencia_Init(object sender, EventArgs e)
		{
			if (Session["Exis"] != null)
			{
				ASPxGridViewTransaccion.DataSource = Session["Trans"];
				ASPxGridViewTransaccion.DataBind();
			}
		}

		protected void BuscarArticulo_Click(object sender, EventArgs e)
		{
			Filtros filtro = new Filtros();
			if (string.IsNullOrEmpty(DateInicial.Text) && string.IsNullOrEmpty(DateFinal.Text))
			{
				filtro.FechaInicial = DateTime.Now.ToString("MM/dd/yyyy 00:00:00");
				filtro.FechaFinal = DateTime.Now.ToString("MM/dd/yyyy 23:59:00");
			}
			else
			{
				filtro.FechaInicial = DateInicial.Date.ToString("MM/dd/yyyy 00:00:00");
				filtro.FechaFinal = DateFinal.Date.ToString("MM/dd/yyyy 23:59:00");
			}
			Cargar(filtro);
		}

		protected void BtnFiltros_Click(object sender, EventArgs e)
		{
			Filtros filtro = new Filtros();
			if (string.IsNullOrEmpty(DateInicial.Text) && string.IsNullOrEmpty(DateFinal.Text))
			{
				filtro.FechaInicial = DateTime.Now.ToString("MM/dd/yyyy 00:00:00");
				filtro.FechaFinal = DateTime.Now.ToString("MM/dd/yyyy 23:59:00");
			}
			else
			{
				filtro.FechaInicial = DateInicial.Date.ToString("MM/dd/yyyy 00:00:00");
				filtro.FechaFinal = DateFinal.Date.ToString("MM/dd/yyyy 23:59:00");
			}
			Cargar(filtro);
		}

		public void Cargar(Filtros filtros)
		{
			try
			{
				var Articulo = dt.ObtenerArticulo(TextArticulo.Text, filtros,compani);
				TextDescripcion.Text = Articulo.Descripcion;
				TextCostoLocal.Text = Articulo.CostoLocalFiscal.ToString();
				TextCostoDolar.Text = Articulo.CostoDolarFiscal.ToString();
				//if (Articulo.TransaccionInv.Count > 0)
				//{
					Session["Trans"] = Articulo.TransaccionInv;
					ASPxGridViewTransaccion.DataSource = Articulo.TransaccionInv;
					ASPxGridViewTransaccion.DataBind();
				//}
				if (Articulo.ExistenciaBodega.Count > 0)
				{
					Session["Exis"] = Articulo.ExistenciaBodega;
					ASPxGridViewExistencia.DataSource = Articulo.ExistenciaBodega;
					ASPxGridViewExistencia.DataBind();
				}
				if (Articulo.ArticuloPrecio != null)
				{
					TextPrecio.Text = Articulo.ArticuloPrecio.Precio.ToString();
				}
			}
			catch (Exception Ex)
			{
				string script = string.Format(@"alert('{0}');", Ex.Message);
				ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
				return;
			}
		}
	}
}