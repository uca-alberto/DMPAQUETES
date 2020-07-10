using DevExpress.Web;
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
	public partial class CrearPaquete : System.Web.UI.Page
	{
		DTPaquetes dtp = new DTPaquetes();
		DTCargaDatos dt = new DTCargaDatos();
		string compani = ConfigurationManager.AppSettings["Compani"];
		List<TraspasoInvLinea> linea;
		//Variables de transacciones
		string ConsecutivoCompra = ConfigurationManager.AppSettings["ConsecutivoCompra"];
		string ConsecutivoAjuste = ConfigurationManager.AppSettings["ConsecutivoAjuste"];
		string ConsecutivoTraspaso = ConfigurationManager.AppSettings["ConsecutivoTraspaso"];

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				DateDocumento.Date = DateTime.Now;
				//CargarArticulo();
			}

		}
		
		#region Carga Datos
		public void CargarBodega()
		{
			try
			{
				var Lista = dt.ListarBodega(compani);
				Session["LISTA_BODEGA"] = Lista;
				ASPxGridLookupBodega.DataSource = Session["LISTA_BODEGA"];
				ASPxGridLookupBodegaDestino.DataSource = Session["LISTA_BODEGA"];
				ASPxGridLookupBodega.DataBind();
				ASPxGridLookupBodegaDestino.DataBind();
			}
			catch (Exception)
			{

				throw;
			}
		}
		public void CargarArticulo()
		{
			try
			{
				if (!IsPostBack)
				{
					var Lista = dt.listarArticulo(compani);
					Session["LISTA_ARTICULO"] = Lista;
					//ASPxGridLookupArticulo.DataSource = Session["LISTA_ARTICULO"];
					//ASPxGridLookupArticulo.DataBind();
				}

			}
			catch (Exception)
			{
				throw;
			}
		}
		#endregion

		#region OnInit
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
				//ASPxGridLookupArticulo.DataSource = Session["LISTA_ARTICULO"];
				//ASPxGridLookupArticulo.DataBind();
			}
		}

		protected void ASPxGridViewDetalle_Init(object sender, EventArgs e)
		{

		}

		#endregion

		#region Botones
		protected void BuscarCodigo_Click(object sender, EventArgs e)
		{
			//ASPxGridView ArticuloGrid = ASPxGridLookupArticulo.GridView;
			//object ArticuloID = ArticuloGrid.GetRowValues(ArticuloGrid.FocusedRowIndex, new string[] { "IdArticulo" });
			//if (ArticuloID != null)
			//{
			try
			{
				var articulo = dt.ObtenerCostos(TextArticulo.Text, compani);
				if (DropTransaccion.SelectedValue.Equals("COMPRA") || DropTransaccion.SelectedValue.Equals("AJUSTE"))
				{
					Textcostolocal.Text = articulo.CostoLocalFiscal.ToString();
					Textcostodolar.Text = articulo.CostoDolarFiscal.ToString();
					TextCantidad.Focus();
				}
				TextArticuloDescripcion.Text = articulo.Descripcion;
			}
			catch (Exception Ex)
			{
				string script = string.Format(@"alert('{0}');", Ex.Message);
				ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
				return;
			}
				
			//}
		}

		protected void GuardarDocumento_Click(object sender, EventArgs e)
		{
			//Variables globales
			string PAQUETE_INVENTARIO = "";
			string AJUSTE_CONFIG = "";
			string TIPO = "";
			string SUBTIPO = "";
			try
			{
				TraspasoInv tran = new TraspasoInv();
				tran.FECHA_DOCUMENTO = Convert.ToDateTime(DateDocumento.Text);
				tran.DOCUMENTO_INV = TextConsecutivo.Text;
				tran.REFERENCIA = TextReferencia.Text;
				tran.MENSAJE_SISTEMA = " ";
				List<TraspasoInvLinea> LineasNuevas = new List<TraspasoInvLinea>();
				//Validamos el tipo de transaccion
				if (DropTransaccion.SelectedValue.Equals("COMPRA"))
				{
					PAQUETE_INVENTARIO = ConsecutivoCompra;
					tran.PAQUETE_INVENTARIO = ConsecutivoCompra;
					tran.TIPOTRAN = ConsecutivoCompra;
					tran.CONSECUTIVO = ConsecutivoCompra;
					AJUSTE_CONFIG = "~OO~";
					TIPO = "O";
					SUBTIPO = "D";
				}
				else if (DropTransaccion.SelectedValue.Equals("AJUSTE"))
				{
					PAQUETE_INVENTARIO = ConsecutivoAjuste;
					tran.PAQUETE_INVENTARIO = ConsecutivoAjuste;
					tran.TIPOTRAN = ConsecutivoAjuste;
					tran.CONSECUTIVO = ConsecutivoAjuste;
					AJUSTE_CONFIG = "~FF~";
					TIPO = "F";
					SUBTIPO = "D";
				}
				else if (DropTransaccion.SelectedValue.Equals("TRASPASO"))
				{
					PAQUETE_INVENTARIO = ConsecutivoTraspaso;
					tran.PAQUETE_INVENTARIO = ConsecutivoTraspaso;
					tran.TIPOTRAN = ConsecutivoTraspaso;
					tran.CONSECUTIVO = ConsecutivoTraspaso;
					AJUSTE_CONFIG = "~TT~";
					TIPO = "T";
					SUBTIPO = "D";
				}
				//Recorremos las lineas del Gridview
				string[] fields = { "ARTICULO", "BODEGA", "CANTIDAD", "COSTO_TOTAL_LOCAL", "COSTO_TOTAL_DOLAR", "PRECIO_TOTAL_LOCAL", "PRECIO_TOTAL_DOLAR", "BODEGA_DESTINO" };
				for (int i = 0; i < ASPxGridViewDetalle.VisibleRowCount; i++)
				{
					object[] values = ASPxGridViewDetalle.GetRowValues(i, fields) as object[];
					LineasNuevas.Add(new TraspasoInvLinea
					{
						PAQUETE_INVENTARIO = PAQUETE_INVENTARIO,
						DOCUMENTO_INV = TextConsecutivo.Text,
						AJUSTE_CONFIG = AJUSTE_CONFIG,
						TIPO = TIPO,
						SUBTIPO = SUBTIPO,
						SUBSUBTIPO = " ",
						ARTICULO = values.GetValue(0).ToString(),
						BODEGA = values.GetValue(1).ToString(),
						CANTIDAD = Convert.ToDecimal(values.GetValue(2).ToString()),
						COSTO_TOTAL_LOCAL = Convert.ToDecimal(values.GetValue(3).ToString()),
						COSTO_TOTAL_DOLAR = Convert.ToDecimal(values.GetValue(4).ToString()),
						PRECIO_TOTAL_LOCAL = Convert.ToDecimal(values.GetValue(5).ToString()),
						PRECIO_TOTAL_DOLAR = Convert.ToDecimal(values.GetValue(6).ToString()),
						BODEGA_DESTINO = values.GetValue(7).ToString(),
					});
				}
				//Ejecutamos el metodo
				tran.traspasolinea = LineasNuevas;
				dtp.InsertarTransaccion(tran, compani);
				ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: Insertar(); ", true);
			}
			catch (Exception Ex)
			{
				//Error.Text = Ex.Message;
				string script = string.Format(@"alert('{0}');", Ex.Message);
				ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
				return;
			}

		}

		protected void ASPxGridViewDetalle_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
		{
			Session["Gridetalle"] = null;
			linea = new List<TraspasoInvLinea>();
			string[] fields = { "ARTICULO", "ARTICULODESCRIPCION", "BODEGA", "CANTIDAD", "COSTO_TOTAL_LOCAL", "COSTO_TOTAL_DOLAR", "PRECIO_TOTAL_LOCAL", "PRECIO_TOTAL_DOLAR", "BODEGA_DESTINO" };
			for (int i = 0; i < ASPxGridViewDetalle.VisibleRowCount; i++)
			{
				object[] values = ASPxGridViewDetalle.GetRowValues(i, fields) as object[];
				if (!e.Values["ARTICULO"].ToString().Equals(values.GetValue(0).ToString()))
				{
					linea.Add(new TraspasoInvLinea
					{
						ARTICULO = values.GetValue(0).ToString(),
						ARTICULODESCRIPCION = values.GetValue(1).ToString(),
						BODEGA = values.GetValue(2).ToString(),
						CANTIDAD = Convert.ToDecimal(values.GetValue(3).ToString()),
						COSTO_TOTAL_LOCAL = Convert.ToDecimal(values.GetValue(4).ToString()),
						COSTO_TOTAL_DOLAR = Convert.ToDecimal(values.GetValue(5).ToString()),
						PRECIO_TOTAL_LOCAL = Convert.ToDecimal(values.GetValue(6).ToString()),
						PRECIO_TOTAL_DOLAR = Convert.ToDecimal(values.GetValue(7).ToString()),
						BODEGA_DESTINO = values.GetValue(8).ToString(),
					});
				}
			}
			Session["Gridetalle"] = linea;
			ASPxGridViewDetalle.DataSource = Session["Gridetalle"];
			ASPxGridViewDetalle.DataBind();
			ASPxGridViewDetalle.CancelEdit();
			e.Cancel = true;
		}

		protected void BtnAgregarArticulo_Click(object sender, EventArgs e)
		{
			Session["Gridetalle"] = null;
			linea = new List<TraspasoInvLinea>();
			bool bandera = false;
			string[] fields = { "ARTICULO", "ARTICULODESCRIPCION", "BODEGA", "CANTIDAD", "COSTO_TOTAL_LOCAL", "COSTO_TOTAL_DOLAR", "PRECIO_TOTAL_LOCAL", "PRECIO_TOTAL_DOLAR", "BODEGA_DESTINO" };
			for (int i = 0; i < ASPxGridViewDetalle.VisibleRowCount; i++)
			{
				object[] values = ASPxGridViewDetalle.GetRowValues(i, fields) as object[];
				linea.Add(new TraspasoInvLinea
				{
					ARTICULO = values.GetValue(0).ToString(),
					ARTICULODESCRIPCION = values.GetValue(1).ToString(),
					BODEGA = values.GetValue(2).ToString(),
					CANTIDAD = Convert.ToDecimal(values.GetValue(3).ToString()),
					COSTO_TOTAL_LOCAL = Convert.ToDecimal(values.GetValue(4).ToString()),
					COSTO_TOTAL_DOLAR = Convert.ToDecimal(values.GetValue(5).ToString()),
					PRECIO_TOTAL_LOCAL = Convert.ToDecimal(values.GetValue(6).ToString()),
					PRECIO_TOTAL_DOLAR = Convert.ToDecimal(values.GetValue(7).ToString()),
					BODEGA_DESTINO = values.GetValue(8).ToString(),
				});
			}

			//Validamos que el articulo no este 2 veces
			//ASPxGridView ArticuloGrid = ASPxGridLookupArticulo.GridView;
			//object ArticuloID = ArticuloGrid.GetRowValues(ArticuloGrid.FocusedRowIndex, new string[] { "IdArticulo" });
			if (!string.IsNullOrEmpty(TextArticulo.Text))
			{
				foreach (var item in linea)
				{
					if (item.ARTICULO.Equals(TextArticulo.Text))
					{
						bandera = true;
					}
				}

				if (!bandera)
				{
					string Destino = " ";
					ASPxGridView BodegaGrid = ASPxGridLookupBodega.GridView;
					object BodegaID = BodegaGrid.GetRowValues(BodegaGrid.FocusedRowIndex, new string[] { "BODEGA" });

					//Bodega destino
					ASPxGridView BodegaDestinoGrid = ASPxGridLookupBodegaDestino.GridView;
					object BodegaDestinoID = BodegaDestinoGrid.GetRowValues(BodegaDestinoGrid.FocusedRowIndex, new string[] { "BODEGA" });
					if (BodegaDestinoID != null)
					{
						Destino = BodegaDestinoID.ToString();
					}
					linea.Add(new TraspasoInvLinea
					{
						ARTICULO = TextArticulo.Text,
						BODEGA = BodegaID.ToString(),
						CANTIDAD = Convert.ToDecimal(TextCantidad.Text),
						COSTO_TOTAL_LOCAL = Convert.ToDecimal(Textcostolocal.Text ?? "0"),
						COSTO_TOTAL_DOLAR = Convert.ToDecimal(Textcostodolar.Text ?? "0"),
						PRECIO_TOTAL_LOCAL = Convert.ToDecimal(0),
						PRECIO_TOTAL_DOLAR = Convert.ToDecimal(0),
						BODEGA_DESTINO = Destino,
						ARTICULODESCRIPCION = TextArticuloDescripcion.Text
					});
				}
				else
				{
					string script = string.Format(@"alert('{0}');", "Articulo: " + TextArticulo.Text.ToString() + " Repetido.");
					ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
					return;
				}
				Textcostolocal.Text = "0";
				Textcostodolar.Text = "0";
				TextCantidad.Text = "0";
				TextArticulo.Text = "";
				TextArticulo.Focus();
				Session["Gridetalle"] = linea;
				ASPxGridViewDetalle.DataSource = Session["Gridetalle"];
				ASPxGridViewDetalle.DataBind();
			}
			else
			{
				string script = string.Format(@"alert('{0}');", "SELECCIONE ARTICULO");
				ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
				return;
			}


		}
		#endregion

		#region Cambios select
		protected void DropTransaccion_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (IsPostBack)
			{
				if (DropTransaccion.SelectedIndex == 1)
				{
					CargarBodega();
					TextConsecutivo.Text = dtp.ObtenerConsecutivoCompra(compani);
					Textcostolocal.Enabled = true;
					Textcostodolar.Enabled = true;
					ASPxGridLookupBodegaDestino.Enabled = false;

				}
				else if (DropTransaccion.SelectedIndex == 2)
				{
					CargarBodega();
					TextConsecutivo.Text = dtp.ObtenerConsecutivoAjuste(compani);
					Textcostolocal.Enabled = true;
					Textcostodolar.Enabled = true;
					ASPxGridLookupBodegaDestino.Enabled = false;

				}
				else if (DropTransaccion.SelectedIndex == 3)
				{
					CargarBodega();
					TextConsecutivo.Text = dtp.ObtenerConsecutivoTraspaso(compani);
					Textcostolocal.Enabled = false;
					Textcostodolar.Enabled = false;
					ASPxGridLookupBodegaDestino.Enabled = true;

				}
				else
				{
					string script = string.Format(@"alert('{0}');", "SELECCIONE UNA TRANSACCION");
					ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, true);
					return;
				}
			}

		}

		#endregion
	}
}