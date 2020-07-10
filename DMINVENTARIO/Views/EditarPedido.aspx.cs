using DevExpress.Web;
using DMINVENTARIO.NCAPAS;
using DMINVENTARIO.NCAPAS.DATOS;
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
	public partial class EditarPedido : System.Web.UI.Page
	{
		DTCargaDatos dt = new DTCargaDatos();
		DTPedido dtp = new DTPedido();
		DTArticulo art = new DTArticulo();

		string compani = ConfigurationManager.AppSettings["Compani"];
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string valor = Request.QueryString["id"];
				if (valor != null)
				{
					CargarDetalle(valor);
					CargaCabecera(valor);
					CargarArticulo();

				}
			}
			if (Session["Rol"] == null)
			{
				Response.Redirect("~/Login.aspx");
				//if (!Session["Rol"].ToString().Equals("1"))
				//{
				//	ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: Acceso(); ", true);
				//}
			}
		}
		#region Carga de datos
		public void CargarDetalle(string ID)
		{
			Session["DetalleUpdate"] = dtp.ListaFiltrada(ID);
			GridLineas.DataSource = Session["DetalleUpdate"];
			GridLineas.DataBind();
		}

		public void CargaCabecera(string ID)
		{
			CABECERA_PEDIDO_WEB C = new CABECERA_PEDIDO_WEB();
			C = dtp.ObtenerCabecera(ID);
			NextConsecutivo.Text = C.CONSECUTIVO;
			DropMoneda.SelectedValue = C.MONEDA;
			Total_Libras.Text = C.TOTAL_LIBRAS.ToString();
			Total_Pedido.Text = C.TOTAL_PEDIDO.ToString(); ;
			CargarCliente(C.CLIENTE);
			CargarBodega(C.BODEGA);
			if (C.ESTADO.Equals("FACTURADO"))
			{
				CheckMigracion.Checked = true;
				CheckMigracion.Enabled = false;
				BtnGuardarEncabezado.Enabled = false;
				AgregarProducto.Enabled = false;
			}

		}
		#endregion

		#region CargarCombo
		public void CargarArticulo()
		{
			try
			{
				var Lista = dt.listarArticulo(compani);
				Session["LISTA_ARTICULO"] = Lista;
				ASPxGridLookupArticulo.DataSource = Session["LISTA_ARTICULO"];
				ASPxGridLookupArticulo.DataBind(); ;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public void CargarCliente(string ID)
		{
			try
			{
				var Lista = dt.listarCliente(compani);
				Session["LISTA_CLIENTE"] = Lista;
				ASPxGridLookupCliente.DataSource = Session["LISTA_CLIENTE"];
				ASPxGridLookupCliente.DataBind();
				ASPxGridLookupCliente.Value = ASPxGridLookupCliente.GridView.GetRowValuesByKeyValue(ID, "CLIENTE");
			}
			catch (Exception ex)
			{

				throw ex;
			}

		}

		public void CargarBodega(string ID)
		{
			try
			{
				var Lista = dt.ListarBodega(compani);
				Session["LISTA_BODEGA"] = Lista;
				ASPxGridLookupBodega.DataSource = Session["LISTA_BODEGA"];
				ASPxGridLookupBodega.DataBind();
				ASPxGridLookupBodega.Value = ASPxGridLookupBodega.GridView.GetRowValuesByKeyValue(ID, "BODEGA");
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Oninit
		protected void ASPxGridLookupCliente_Init(object sender, EventArgs e)
		{
			if (Session["LISTA_CLIENTE"] != null)
			{
				ASPxGridLookupCliente.DataSource = Session["LISTA_CLIENTE"];
				ASPxGridLookupCliente.DataBind();
			}
		}

		protected void ASPxGridLookupBodega_Init(object sender, EventArgs e)
		{
			if (Session["LISTA_BODEGA"] != null)
			{
				ASPxGridLookupBodega.DataSource = Session["LISTA_BODEGA"];
				ASPxGridLookupBodega.DataBind();
			}
		}

		protected void ASPxGridLookupArticulo_Init(object sender, EventArgs e)
		{
			if (Session["LISTA_ARTICULO"] != null)
			{
				ASPxGridLookupArticulo.DataSource = Session["LISTA_ARTICULO"];
				ASPxGridLookupArticulo.DataBind();
			}
		}

		protected void GridLineas_Init(object sender, EventArgs e)
		{
			if (Session["DetalleUpdate"] != null)
			{
				GridLineas.DataSource = Session["DetalleUpdate"];
				GridLineas.DataBind();
			}
		}
		#endregion

		#region Botones
		protected void BtnSugerirPrecio_Click(object sender, EventArgs e)
		{
			try
			{
				ASPxGridView Cliente = ASPxGridLookupCliente.GridView;
				object ClienteNivel = Cliente.GetRowValues(Cliente.FocusedRowIndex, new string[] { "NIVEL_PRECIO" });
				object ClienteMoneda = Cliente.GetRowValues(Cliente.FocusedRowIndex, new string[] { "MONEDA_NIVEL" });
				ASPxGridView ArticuloGrid = ASPxGridLookupArticulo.GridView;
				object ArticuloID = ArticuloGrid.GetRowValues(ArticuloGrid.FocusedRowIndex, new string[] { "IdArticulo" });

				if (ClienteNivel == null)
				{
					string script = $@"alert('Seleccione un Cliente');";
					ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
					return;
				}
				if (ArticuloID == null)
				{
					string script = $@"alert('Seleccione un Articulo');";
					ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
					return;
				}
				//if (Articulo.ObtenerPrecio(ArticuloID.ToString(), ClienteNivel.ToString(), ClienteMoneda.ToString()) != 0)
				//{
					TxtPrecio.Text = art.ObtenerPrecio(ArticuloID.ToString(), ClienteNivel.ToString(), ClienteMoneda.ToString(),compani).ToString();
				//}
				//else
				//{
				//	string script = string.Format(@"El articulo :'{0}' No tiene lista de precio asignada para el nivel :'{1}'", ArticuloID.ToString(), ClienteNivel.ToString());
				//	Error.Text = script;
				//	return;
				//}
				DropMoneda.SelectedValue = ClienteMoneda.ToString();
			}
			catch (Exception Ex)
			{

				throw Ex;
			}
		}

		protected void DropMoneda_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (IsPostBack)
			{
				if (DropMoneda.SelectedValue.Equals("L"))
				{
					double PrecioTemporal = Convert.ToDouble(TxtPrecio.Text);
					double Conversion = 0;
					double Cambio = dt.ObtenerTipoCambio(compani);
					if (Cambio != 0)
					{
						Conversion = PrecioTemporal * Cambio;
						TxtPrecio.Text = Conversion.ToString();
					}
					else
					{
						string script = $@"alert('El dia de hoy no tiene tipo de cambio');";
						ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
						return;
					}

				}
				else if (DropMoneda.SelectedValue.Equals("D"))
				{
					double PrecioTemporal = Convert.ToDouble(TxtPrecio.Text);
					double Conversion = 0;
					double Cambio = dt.ObtenerTipoCambio(compani);
					if (Cambio != 0)
					{
						Conversion = PrecioTemporal / Cambio;
						TxtPrecio.Text = Conversion.ToString();
					}
					else
					{
						string script = $@"alert('El dia de hoy no tiene tipo de cambio');";
						ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
						return;
					}
				}
			}
		}

		protected void AgregarProducto_Click(object sender, EventArgs e)
		{
			ASPxGridView ArticuloGrid = ASPxGridLookupArticulo.GridView;
			object ArticuloID = ArticuloGrid.GetRowValues(ArticuloGrid.FocusedRowIndex, new string[] { "IdArticulo" });
			object ArticuloDescripcion = ArticuloGrid.GetRowValues(ArticuloGrid.FocusedRowIndex, new string[] { "Descripcion" });
			LINEA_PEDIDO_WEB Pedido = new LINEA_PEDIDO_WEB();
			Pedido.PEDIDO = NextConsecutivo.Text;
			Pedido.ARTICULO = ArticuloID.ToString();
			Pedido.DESCRIPCION = ArticuloDescripcion.ToString();
			if (TxtPrecio.Text == string.Empty)
			{
				string script = $@"alert('Digite Precio');";
				ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
				return;
			}
			else if (TxtCantidadSolicitada.Text == string.Empty)
			{
				string script = $@"alert('Digite Cantidad');";
				ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
				return;
			}
			Pedido.PRECIO = Convert.ToDecimal(TxtPrecio.Text);
			Pedido.CANTIDAD_FACTURAR = Convert.ToDecimal(TxtCantidadSolicitada.Text);
			Pedido.ESCANEADA = 0;
			if (dtp.InsertarLineaPedido(Pedido))
			{
				TxtPrecio.Text = "";
				TxtCantidadSolicitada.Text = "";
				Session["DetalleUpdate"] = dtp.ListaFiltrada(NextConsecutivo.Text);
				GridLineas.DataSource = Session["DetalleUpdate"];
				GridLineas.DataBind();
			}
			else
			{
				ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: Validacion(); ", true);
			}
		}

		protected void BtnGuardarEncabezado_Click(object sender, EventArgs e)
		{
			try
			{
				CABECERA_PEDIDO_WEB Cabecera = new CABECERA_PEDIDO_WEB();
				ASPxGridView Bodega = ASPxGridLookupBodega.GridView;
				object BodegaID = Bodega.GetRowValues(Bodega.FocusedRowIndex, new string[] { "BODEGA" });
				ASPxGridView Cliente = ASPxGridLookupCliente.GridView;
				object ClienteID = Cliente.GetRowValues(Cliente.FocusedRowIndex, new string[] { "CLIENTE" });
				if (BodegaID != null && ClienteID != null)
				{
					Cabecera.ID = 1;
					Cabecera.CONSECUTIVO = NextConsecutivo.Text;
					Cabecera.CLIENTE = ClienteID.ToString();
					Cabecera.MONEDA = DropMoneda.SelectedValue;
					Cabecera.BODEGA = BodegaID.ToString();
					Cabecera.USUARIO_MODIFICA = Session["Usuario"].ToString();
					Cabecera.FECHA_MODIFICA = DateTime.Now;
					Cabecera.ESTADO = "PENDIENTE";
					if (dtp.ModificarTransaccion(Cabecera, CheckMigracion.Checked,compani))
					{
						Session["Lineas"] = null;
						ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: Insertar(); ", true);
					}
				}
				else
				{
					string script = $@"alert('Seleccione Bodega y Cliente');";
					ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
					return;
				}
			}
			catch (Exception Ex)
			{
				string script = $@"alert('" + Ex.Message + "');";
				ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
			}
		}
		#endregion

		#region Row
		protected void GridLineas_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
		{
			LINEA_PEDIDO_WEB Linea = new LINEA_PEDIDO_WEB();
			Linea.PEDIDO = e.NewValues["PEDIDO"].ToString();
			Linea.ARTICULO = e.NewValues["ARTICULO"].ToString();
			Linea.CANTIDAD_FACTURAR = Convert.ToDecimal(e.NewValues["CANTIDAD_FACTURAR"].ToString());
			Linea.DESCUENTOPORCENTAJE = Convert.ToDecimal(e.NewValues["DESCUENTOPORCENTAJE"].ToString());
			if (dtp.ActualizarLineaPedido(Linea))
			{
				ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: Insertar(); ", true);
			}
			else
			{
				string script = $@"alert('Error al Insertar');";
				ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
			}
			GridLineas.CancelEdit();
			e.Cancel = true;
			Session["DetalleUpdate"] = dtp.ListaFiltrada(NextConsecutivo.Text);
			GridLineas.DataSource = Session["DetalleUpdate"];
			GridLineas.DataBind();
		}

		protected void GridLineas_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
		{
			LINEA_PEDIDO_WEB Linea = new LINEA_PEDIDO_WEB();
			Linea.PEDIDO = e.Values["PEDIDO"].ToString();
			Linea.ARTICULO = e.Values["ARTICULO"].ToString();

			dtp.EliminarLinea(Linea);

			Session["DetalleUpdate"] = dtp.ListaFiltrada(NextConsecutivo.Text);
			GridLineas.DataSource = Session["DetalleUpdate"];
			GridLineas.DataBind();
			GridLineas.CancelEdit();
			e.Cancel = true;
		}
		#endregion

		
	}
}