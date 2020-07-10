using DMINVENTARIO.Modelo.Context;
using DMINVENTARIO.NCAPAS.ENTIDADES;
using DMINVENTARIO.NCAPAS.MODELO.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Web;
using WEBBCFOODS.Ncapas;

namespace DMINVENTARIO.NCAPAS
{
	public class DTPedido : IGENERIC<CABECERA_PEDIDO_WEB>
	{
		public static string Conexion = System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString;

		string ConsecutivoFA = ConfigurationManager.AppSettings["ConsecutivoFA"];
		public bool Eliminar(CABECERA_PEDIDO_WEB obj)
		{
			using (var context = new ApiContext(Conexion))
			{
				var Cabecera = context.pedidocabecera.FirstOrDefault(x => x.CONSECUTIVO == obj.CONSECUTIVO);
				if (Cabecera != null)
				{
					var Linea = context.pedidolinea.Where(x => x.PEDIDO == Cabecera.CONSECUTIVO).ToList();
					if (Linea != null)
					{
						context.pedidolinea.RemoveRange(Linea);
						context.pedidocabecera.Remove(Cabecera);
						context.SaveChanges();
					}
					else
					{
						return false;
					}
				}
				else
				{
					return false;
				}
			}
			return true;
		}

		public bool Insertar(CABECERA_PEDIDO_WEB obj)
		{
			throw new NotImplementedException();
		}

		public List<CABECERA_PEDIDO_WEB> listarTodo()
		{
			throw new NotImplementedException();
		}

		public List<CabeceraDB> listarTransacciones(string Compani)
		{
			try
			{
				var ListaCabecera = new List<CabeceraDB>();
				using (var context = new ApiContext(Conexion))
				{
					var Lista = context.pedidocabecera.ToList();
					foreach (var item in Lista)
					{
						string sql = string.Format("SELECT NOMBRE FROM {0}.CLIENTE WHERE CLIENTE = '{1}'", Compani,item.CLIENTE);
						var Cliente = context.Database.SqlQuery<string>(sql).FirstOrDefault();
						ListaCabecera.Add(new CabeceraDB
						{
							ID = item.ID,
							CONSECUTIVO = item.CONSECUTIVO,
							BODEGA = item.BODEGA,
							CLIENTE = item.CLIENTE,
							NombreCliente = Cliente,
							MONEDA = item.MONEDA,
							USUARIO = item.USUARIO,
							FECHA_CREACION = item.FECHA_CREACION,
							USUARIO_MODIFICA = item.USUARIO_MODIFICA,
							FECHA_MODIFICA = item.FECHA_MODIFICA ?? DateTime.Today,
							ESTADO = item.ESTADO,
							TOTAL_LIBRAS = item.TOTAL_LIBRAS,
							TOTAL_PEDIDO = item.TOTAL_PEDIDO,
							COMPANI = item.COMPANI
						});
					}
				}
				return ListaCabecera;
			}
			catch (Exception Ex)
			{
				throw Ex;
			}
		}

		public bool Modificar(CABECERA_PEDIDO_WEB obj)
		{
			throw new NotImplementedException();
		}

		#region Lineas Web Pedido
		public bool InsertarLineaPedido(LINEA_PEDIDO_WEB obj)
		{
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					var Registro = context.pedidolinea.FirstOrDefault(x => x.PEDIDO == obj.PEDIDO && x.ARTICULO == obj.ARTICULO);
					if (Registro == null)
					{
						context.pedidolinea.Add(obj);
						context.SaveChanges();
						return true;
					}
					else
					{
						return false;
					}
				}
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public bool ActualizarLineaPedido(LINEA_PEDIDO_WEB obj)
		{
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					var Registro = context.pedidolinea.FirstOrDefault(x => x.PEDIDO == obj.PEDIDO && x.ARTICULO == obj.ARTICULO);
					if (Registro != null)
					{
						Registro.DESCUENTOPORCENTAJE = obj.DESCUENTOPORCENTAJE;
						Registro.CANTIDAD_FACTURAR = obj.CANTIDAD_FACTURAR;
						Registro.DESCUENTO = (((Registro.PRECIO * obj.CANTIDAD_FACTURAR) * obj.DESCUENTOPORCENTAJE)/100);
						context.SaveChanges();
						return true;
					}
					else
					{
						return false;
					}
				}
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		public List<LINEA_PEDIDO_WEB> ListaFiltrada(string Consecutivo)
		{
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					var Lista = context.pedidolinea.Where(X => X.PEDIDO == Consecutivo).ToList();
					return Lista;
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public bool EliminarLinea(LINEA_PEDIDO_WEB obj)
		{
			using (var context = new ApiContext(Conexion))
			{
				var Linea = context.pedidolinea.FirstOrDefault(x => x.PEDIDO == obj.PEDIDO && x.ARTICULO == obj.ARTICULO);
				if (Linea != null)
				{
					context.pedidolinea.Remove(Linea);
					context.SaveChanges();
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public bool InsertarTransaccion(CABECERA_PEDIDO_WEB obj, bool Migracion,string Compani)
		{
			string CodigoNuevo = "";
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					//using (var scope = context.Database.BeginTransaction())
					//{
						var Linea = context.pedidolinea.Where(x => x.PEDIDO == obj.CONSECUTIVO);
						decimal TotalLibras = 0;
						decimal TotalPedido = 0;

						foreach (var item in Linea)
						{
							TotalLibras = TotalLibras + item.CANTIDAD_FACTURAR;
							TotalPedido = TotalPedido + (item.CANTIDAD_FACTURAR * item.PRECIO)-item.DESCUENTO;
						}
						obj.TOTAL_LIBRAS = TotalLibras;
						obj.TOTAL_PEDIDO = TotalPedido;
						obj.COMPANI = Compani;
						context.pedidocabecera.Add(obj);
						context.SaveChanges();
						if (Migracion)
						{
							//Insertar en Cabecera de Pedido de Softland
							var sqlcli = string.Format("SELECT CLIENTE,NOMBRE,MONEDA_NIVEL,NIVEL_PRECIO,PAIS,CONDICION_PAGO,ALIAS,DIRECCION FROM {0}.CLIENTE WHERE CLIENTE = '{1}'",Compani,obj.CLIENTE);
							var Cliente = context.Database.SqlQuery<Cliente>(sqlcli).FirstOrDefault();
							string Consecutivo = context.Database.SqlQuery<string>(string.Format("SELECT VALOR_CONSECUTIVO FROM {0}.CONSECUTIVO_FA WHERE CODIGO_CONSECUTIVO = '{1}'",Compani,ConsecutivoFA)).FirstOrDefault();
							CodigoNuevo = ProximoCodigo(Consecutivo, Consecutivo.Length);

							//Ejecutamos el procedimiento almacenado para la creacion de la cabecera del pedido.
							context.Database.ExecuteSqlCommand(string.Format("EXEC {0}.INSERTAR_PEDIDO_CABECERA @PEDIDO,@ESTADO,@FECHA_PEDIDO,@FECHA_PROMETIDA,@FECHA_PROX_EMBARQU,@EMBARCAR_A,@DIREC_EMBARQUE,@DIRECCION_FACTURA,@TOTAL_MERCADERIA,@MONTO_ANTICIPO,@MONTO_FLETE,@MONTO_SEGURO,@MONTO_DOCUMENTACIO,@TIPO_DESCUENTO1,@TIPO_DESCUENTO2,@MONTO_DESCUENTO1,@MONTO_DESCUENTO2,@PORC_DESCUENTO1,@PORC_DESCUENTO2,@TOTAL_IMPUESTO1,@TOTAL_IMPUESTO2,@TOTAL_A_FACTURAR,@PORC_COMI_VENDEDOR,@PORC_COMI_COBRADOR,@TOTAL_CANCELADO,@TOTAL_UNIDADES,@IMPRESO,@FECHA_HORA,@DESCUENTO_VOLUMEN,@TIPO_PEDIDO,@MONEDA_PEDIDO,@VERSION_NP,@AUTORIZADO,@DOC_A_GENERAR,@CLASE_PEDIDO,@MONEDA,@NIVEL_PRECIO,@COBRADOR,@RUTA,@USUARIO,@CONDICION_PAGO,@BODEGA,@ZONA,@VENDEDOR,@CLIENTE,@CLIENTE_DIRECCION,@CLIENTE_CORPORAC,@CLIENTE_ORIGEN,@PAIS,@SUBTIPO_DOC_CXC,@TIPO_DOC_CXC,@BACKORDER,@PORC_INTCTE,@DESCUENTO_CASCADA,@FIJAR_TIPO_CAMBIO,@ORIGEN_PEDIDO,@CreatedBy,@UpdatedBy",Compani),
											new SqlParameter("@PEDIDO", CodigoNuevo),
											new SqlParameter("@ESTADO", "N"),
											new SqlParameter("@FECHA_PEDIDO", DateTime.Now),
											new SqlParameter("@FECHA_PROMETIDA", DateTime.Now),
											new SqlParameter("@FECHA_PROX_EMBARQU", DateTime.Now),
											new SqlParameter("@EMBARCAR_A", Cliente.ALIAS),
											new SqlParameter("@DIREC_EMBARQUE", "ND"),
											new SqlParameter("@DIRECCION_FACTURA", Cliente.DIRECCION),
											new SqlParameter("@TOTAL_MERCADERIA", TotalPedido),
											new SqlParameter("@MONTO_ANTICIPO", '0'),
											new SqlParameter("@MONTO_FLETE", '0'),
											new SqlParameter("@MONTO_SEGURO", '0'),
											new SqlParameter("@MONTO_DOCUMENTACIO", '0'),
											new SqlParameter("@TIPO_DESCUENTO1", "P"),
											new SqlParameter("@TIPO_DESCUENTO2", "P"),
											new SqlParameter("@MONTO_DESCUENTO1", "0"),
											new SqlParameter("@MONTO_DESCUENTO2", "0"),
											new SqlParameter("@PORC_DESCUENTO1", "0"),
											new SqlParameter("@PORC_DESCUENTO2", "0"),
											new SqlParameter("@TOTAL_IMPUESTO1", "0"),
											new SqlParameter("@TOTAL_IMPUESTO2", "0"),
											new SqlParameter("@TOTAL_A_FACTURAR", TotalPedido),
											new SqlParameter("@PORC_COMI_VENDEDOR", "0"),
											new SqlParameter("@PORC_COMI_COBRADOR", "0"),
											new SqlParameter("@TOTAL_CANCELADO", "0"),
											new SqlParameter("@TOTAL_UNIDADES", "0"),
											new SqlParameter("@IMPRESO", "N"),
											new SqlParameter("@FECHA_HORA", DateTime.Now),
											new SqlParameter("@DESCUENTO_VOLUMEN", "0"),
											new SqlParameter("@TIPO_PEDIDO", "N"),
											new SqlParameter("@MONEDA_PEDIDO", obj.MONEDA),
											new SqlParameter("@VERSION_NP", "0"),
											new SqlParameter("@AUTORIZADO", "N"),
											new SqlParameter("@DOC_A_GENERAR", "F"),
											new SqlParameter("@CLASE_PEDIDO", "N"),
											new SqlParameter("@MONEDA", Cliente.MONEDA_NIVEL),
											new SqlParameter("@NIVEL_PRECIO", Cliente.NIVEL_PRECIO),
											new SqlParameter("@COBRADOR", "ND"),
											new SqlParameter("@RUTA", "ND"),
											new SqlParameter("@USUARIO", "ERPADMIN"),
											new SqlParameter("@CONDICION_PAGO", Cliente.CONDICION_PAGO),
											new SqlParameter("@BODEGA", obj.BODEGA),
											new SqlParameter("@ZONA", "ND"),
											new SqlParameter("@VENDEDOR", "ND"),
											new SqlParameter("@CLIENTE", obj.CLIENTE),
											new SqlParameter("@CLIENTE_DIRECCION", obj.CLIENTE),
											new SqlParameter("@CLIENTE_CORPORAC", obj.CLIENTE),
											new SqlParameter("@CLIENTE_ORIGEN", obj.CLIENTE),
											new SqlParameter("@PAIS", Cliente.PAIS),
											new SqlParameter("@SUBTIPO_DOC_CXC", "0"),
											new SqlParameter("@TIPO_DOC_CXC", "FAC"),
											new SqlParameter("@BACKORDER", "N"),
											new SqlParameter("@PORC_INTCTE", "0"),
											new SqlParameter("@DESCUENTO_CASCADA", "N"),
											new SqlParameter("@FIJAR_TIPO_CAMBIO", "N"),
											new SqlParameter("@ORIGEN_PEDIDO", "F"),
											new SqlParameter("@CreatedBy", "SA"),
											new SqlParameter("@UpdatedBy", "SA"));
							context.Database.Connection.Close();
							//Procedimiento para la Inserta las lineas del Pedido en Softland
							var lineaPedido = context.pedidolinea.Where(x => x.PEDIDO == obj.CONSECUTIVO).ToList();
							foreach (var item in lineaPedido)
							{
								string sql = string.Format(@"EXEC {0}.INSERTAR_PEDIDO_LINEA																			 @PEDIDO,@BODEGA,
														   @ARTICULO,@FECHA_ENTREGA,
									                       @PRECIO_UNITARIO,@CANTIDAD_PEDIDA,
														   @CANTIDAD_A_FACTURA,@DESCUENTOMONTO,
														   @DESCUENTOPORCENTAJE,
									                       @FECHA_PROMETIDA,@CreatedBy,@UpdatedBy", Compani);
								context.Database.ExecuteSqlCommand(sql,
									new SqlParameter("@PEDIDO", CodigoNuevo),
									new SqlParameter("@BODEGA", obj.BODEGA),
									new SqlParameter("@ARTICULO", item.ARTICULO),
									new SqlParameter("@FECHA_ENTREGA", DateTime.Now),
									new SqlParameter("@PRECIO_UNITARIO", item.PRECIO),
									new SqlParameter("@CANTIDAD_PEDIDA", item.CANTIDAD_FACTURAR),
									new SqlParameter("@CANTIDAD_A_FACTURA", item.CANTIDAD_FACTURAR),
									new SqlParameter("@DESCUENTOMONTO", item.DESCUENTO),
									new SqlParameter("@DESCUENTOPORCENTAJE", item.DESCUENTOPORCENTAJE),
									new SqlParameter("@FECHA_PROMETIDA", DateTime.Now),
									new SqlParameter("@CreatedBy", "ERPADMIN"),
									new SqlParameter("@UpdatedBy", "ERPADMIN"));
							}
							//Actualizacion de Consecutivo en softland
							string sqlactualizacion = string.Format("UPDATE {0}.CONSECUTIVO_FA SET VALOR_CONSECUTIVO =@Consecutivo WHERE CODIGO_CONSECUTIVO = '{1}'", Compani,ConsecutivoFA);
							context.Database.ExecuteSqlCommand(sqlactualizacion,
								new SqlParameter("@Consecutivo", CodigoNuevo));
							var Estado = context.pedidocabecera.FirstOrDefault(x => x.CONSECUTIVO == obj.CONSECUTIVO);
							if (Estado != null)
							{
								Estado.ESTADO = "FACTURADO";
								context.SaveChanges();
							}
							else
							{
								return false;
							}
						}

					//	scope.Commit();
					//}
				}
				
				return true;
			}
			catch (Exception Ex)
			{
				using (var context = new ApiContext(Conexion))
				{
					//Eliminar cabecera
					string sqlactualizacion = string.Format("DELETE {0}.PEDIDO WHERE PEDIDO = @Consecutivo", Compani);
					context.Database.ExecuteSqlCommand(sqlactualizacion,
						new SqlParameter("@Consecutivo", CodigoNuevo));
				}
				throw new FaultException(Ex.Message);
			}
		}

		public bool ModificarTransaccion(CABECERA_PEDIDO_WEB obj, bool Migracion,string Compani)
		{
			string CodigoNuevo = "";
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					var Cabecera = context.pedidocabecera.FirstOrDefault(x => x.CONSECUTIVO == obj.CONSECUTIVO);

					if (Cabecera != null)
					{
						var Linea = context.pedidolinea.Where(x => x.PEDIDO == obj.CONSECUTIVO).ToList();
						decimal TotalLibras = 0;
						decimal TotalPedido = 0;

						foreach (var item in Linea)
						{
							TotalLibras = TotalLibras + item.CANTIDAD_FACTURAR;
							TotalPedido = TotalPedido + (item.CANTIDAD_FACTURAR * item.PRECIO)-item.DESCUENTO;
						}
						Cabecera.TOTAL_LIBRAS = TotalLibras;
						Cabecera.TOTAL_PEDIDO = TotalPedido;
						Cabecera.BODEGA = obj.BODEGA;
						Cabecera.CLIENTE = obj.CLIENTE;
						Cabecera.MONEDA = obj.MONEDA;
						Cabecera.USUARIO_MODIFICA = obj.USUARIO_MODIFICA;
						Cabecera.FECHA_MODIFICA = obj.FECHA_MODIFICA;
						context.SaveChanges();
						if (Migracion)
						{
							//Insertar en Cabecera de Pedido de Softland
							var sqlcli = string.Format(@"SELECT																										CLIENTE,NOMBRE,
														MONEDA_NIVEL,NIVEL_PRECIO,
														PAIS,CONDICION_PAGO,
														ALIAS,DIRECCION 
														FROM 
														{0}.CLIENTE 
														WHERE CLIENTE = '{1}'", Compani, obj.CLIENTE);
							var Cliente = context.Database.SqlQuery<Cliente>(sqlcli).FirstOrDefault();

							//Se Obtiene el ultimo consecutivo
							string Consecutivo = context.Database.SqlQuery<string>(
								string.Format(@"SELECT VALOR_CONSECUTIVO 
												FROM {0}.CONSECUTIVO_FA 
												WHERE CODIGO_CONSECUTIVO = '{1}'", Compani, ConsecutivoFA)).FirstOrDefault();

							//Se asigna el Nuevo valor
							CodigoNuevo = ProximoCodigo(Consecutivo, Consecutivo.Length);

							//Ejecutamos el procedimiento almacenado para la creacion de la cabecera del pedido.
							context.Database.ExecuteSqlCommand(
								string.Format(@"EXEC {0}.INSERTAR_PEDIDO_CABECERA				
												@PEDIDO,@ESTADO,@FECHA_PEDIDO,
												@FECHA_PROMETIDA,@FECHA_PROX_EMBARQU,
												@EMBARCAR_A,@DIREC_EMBARQUE,@DIRECCION_FACTURA,
												@TOTAL_MERCADERIA,@MONTO_ANTICIPO,@MONTO_FLETE,
												@MONTO_SEGURO,@MONTO_DOCUMENTACIO,@TIPO_DESCUENTO1,
												@TIPO_DESCUENTO2,@MONTO_DESCUENTO1,@MONTO_DESCUENTO2,
												@PORC_DESCUENTO1,@PORC_DESCUENTO2,@TOTAL_IMPUESTO1,
												@TOTAL_IMPUESTO2,@TOTAL_A_FACTURAR,@PORC_COMI_VENDEDOR,
												@PORC_COMI_COBRADOR,@TOTAL_CANCELADO,@TOTAL_UNIDADES,
												@IMPRESO,@FECHA_HORA,@DESCUENTO_VOLUMEN,@TIPO_PEDIDO,
												@MONEDA_PEDIDO,@VERSION_NP,@AUTORIZADO,@DOC_A_GENERAR,
												@CLASE_PEDIDO,@MONEDA,@NIVEL_PRECIO,@COBRADOR,@RUTA,
												@USUARIO,@CONDICION_PAGO,@BODEGA,@ZONA,@VENDEDOR,@CLIENTE,
												@CLIENTE_DIRECCION,@CLIENTE_CORPORAC,@CLIENTE_ORIGEN,
												@PAIS,@SUBTIPO_DOC_CXC,@TIPO_DOC_CXC,@BACKORDER,@PORC_INTCTE,
												@DESCUENTO_CASCADA,@FIJAR_TIPO_CAMBIO,@ORIGEN_PEDIDO,
												@CreatedBy,@UpdatedBy",Compani),
												new SqlParameter("@PEDIDO", CodigoNuevo),
												new SqlParameter("@ESTADO", "N"),
												new SqlParameter("@FECHA_PEDIDO", DateTime.Now),
												new SqlParameter("@FECHA_PROMETIDA", DateTime.Now),
												new SqlParameter("@FECHA_PROX_EMBARQU", DateTime.Now),
												new SqlParameter("@EMBARCAR_A", Cliente.ALIAS),
												new SqlParameter("@DIREC_EMBARQUE", "ND"),
												new SqlParameter("@DIRECCION_FACTURA", Cliente.DIRECCION),
												new SqlParameter("@TOTAL_MERCADERIA", TotalPedido),
												new SqlParameter("@MONTO_ANTICIPO", '0'),
												new SqlParameter("@MONTO_FLETE", '0'),
												new SqlParameter("@MONTO_SEGURO", '0'),
												new SqlParameter("@MONTO_DOCUMENTACIO", '0'),
												new SqlParameter("@TIPO_DESCUENTO1", "P"),
												new SqlParameter("@TIPO_DESCUENTO2", "P"),
												new SqlParameter("@MONTO_DESCUENTO1", "0"),
												new SqlParameter("@MONTO_DESCUENTO2", "0"),
												new SqlParameter("@PORC_DESCUENTO1", "0"),
												new SqlParameter("@PORC_DESCUENTO2", "0"),
												new SqlParameter("@TOTAL_IMPUESTO1", "0"),
												new SqlParameter("@TOTAL_IMPUESTO2", "0"),
												new SqlParameter("@TOTAL_A_FACTURAR", TotalPedido),
												new SqlParameter("@PORC_COMI_VENDEDOR", "0"),
												new SqlParameter("@PORC_COMI_COBRADOR", "0"),
												new SqlParameter("@TOTAL_CANCELADO", "0"),
												new SqlParameter("@TOTAL_UNIDADES", "0"),
												new SqlParameter("@IMPRESO", "N"),
												new SqlParameter("@FECHA_HORA", DateTime.Now),
												new SqlParameter("@DESCUENTO_VOLUMEN", "0"),
												new SqlParameter("@TIPO_PEDIDO", "N"),
												new SqlParameter("@MONEDA_PEDIDO", obj.MONEDA),
												new SqlParameter("@VERSION_NP", "0"),
												new SqlParameter("@AUTORIZADO", "N"),
												new SqlParameter("@DOC_A_GENERAR", "F"),
												new SqlParameter("@CLASE_PEDIDO", "N"),
												new SqlParameter("@MONEDA", Cliente.MONEDA_NIVEL),
												new SqlParameter("@NIVEL_PRECIO", Cliente.NIVEL_PRECIO),
												new SqlParameter("@COBRADOR", "ND"),
												new SqlParameter("@RUTA", "ND"),
												new SqlParameter("@USUARIO", "ERPADMIN"),
												new SqlParameter("@CONDICION_PAGO", Cliente.CONDICION_PAGO),
												new SqlParameter("@BODEGA", obj.BODEGA),
												new SqlParameter("@ZONA", "ND"),
												new SqlParameter("@VENDEDOR", "ND"),
												new SqlParameter("@CLIENTE", obj.CLIENTE),
												new SqlParameter("@CLIENTE_DIRECCION", obj.CLIENTE),
												new SqlParameter("@CLIENTE_CORPORAC", obj.CLIENTE),
												new SqlParameter("@CLIENTE_ORIGEN", obj.CLIENTE),
												new SqlParameter("@PAIS", Cliente.PAIS),
												new SqlParameter("@SUBTIPO_DOC_CXC", "0"),
												new SqlParameter("@TIPO_DOC_CXC", "FAC"),
												new SqlParameter("@BACKORDER", "N"),
												new SqlParameter("@PORC_INTCTE", "0"),
												new SqlParameter("@DESCUENTO_CASCADA", "N"),
												new SqlParameter("@FIJAR_TIPO_CAMBIO", "N"),
												new SqlParameter("@ORIGEN_PEDIDO", "F"),
												new SqlParameter("@CreatedBy", "SA"),
												new SqlParameter("@UpdatedBy", "SA"));
							context.Database.Connection.Close();
							//Procedimiento para la Inserta las lineas del Pedido en Softland
							var lineaPedido = context.pedidolinea.Where(x => x.PEDIDO == obj.CONSECUTIVO).ToList();
							foreach (var item in lineaPedido)
							{
								context.Database.ExecuteSqlCommand(
									string.Format(@"EXEC {0}.INSERTAR_PEDIDO_LINEA	
													@PEDIDO,@BODEGA,@ARTICULO,
													@FECHA_ENTREGA,@PRECIO_UNITARIO,@CANTIDAD_PEDIDA,
													@CANTIDAD_A_FACTURA,@DESCUENTOMONTO,@DESCUENTOPORCENTAJE,
													@FECHA_PROMETIDA,
													@CreatedBy,@UpdatedBy", Compani),
													new SqlParameter("@PEDIDO", CodigoNuevo),
													new SqlParameter("@BODEGA", obj.BODEGA),
													new SqlParameter("@ARTICULO", item.ARTICULO),
													new SqlParameter("@FECHA_ENTREGA", DateTime.Now),
													new SqlParameter("@PRECIO_UNITARIO", item.PRECIO),
													new SqlParameter("@CANTIDAD_PEDIDA", item.CANTIDAD_FACTURAR),
													new SqlParameter("@CANTIDAD_A_FACTURA", item.CANTIDAD_FACTURAR),
													new SqlParameter("@DESCUENTOMONTO", item.DESCUENTO),
													new SqlParameter("@DESCUENTOPORCENTAJE", item.DESCUENTOPORCENTAJE),
													new SqlParameter("@FECHA_PROMETIDA", DateTime.Now),
													new SqlParameter("@CreatedBy", "ERPADMIN"),
													new SqlParameter("@UpdatedBy", "ERPADMIN"));
							}
							//Actualizacion de Consecutivo en softland
							context.Database.ExecuteSqlCommand(
								string.Format(@"UPDATE {0}.CONSECUTIVO_FA 
												SET VALOR_CONSECUTIVO =@Consecutivo 
												WHERE CODIGO_CONSECUTIVO = '{1}'",Compani,ConsecutivoFA),
												new SqlParameter("@Consecutivo", CodigoNuevo));

							//Cambio de Estado cabecera.
							var Estado = context.pedidocabecera.FirstOrDefault(x => x.CONSECUTIVO == obj.CONSECUTIVO);
							if (Estado != null)
							{
								Estado.ESTADO = "FACTURADO";
								context.SaveChanges();
							}
							else
							{
								return false;
							}
						}
						return true;
					}
					else
					{
						return false;
					}
				}
				
			}
			catch (Exception Ex)
			{
				using (var context = new ApiContext(Conexion))
				{
					//Eliminar cabecera
					string sqlactualizacion = string.Format("DELETE {0}.PEDIDO WHERE PEDIDO = @Consecutivo", Compani);
					context.Database.ExecuteSqlCommand(sqlactualizacion,
						new SqlParameter("@Consecutivo", CodigoNuevo));
				}
				throw new FaultException(Ex.Message);
			}

		}

		public static string ProximoCodigo(string psCodigoOriginal, int pnLongitudMaxima)
		{
			char[] chArray = new char[50];
			string s = "";
			if (string.IsNullOrEmpty(psCodigoOriginal))
			{
				chArray = "1".ToCharArray();
			}
			else
			{
				try
				{
					char ch;
					bool flag2 = false;
					bool flag = true;
					chArray = psCodigoOriginal.ToCharArray();
					int length = psCodigoOriginal.Length;
					int index = length - 1;
					if (pnLongitudMaxima >= length)
					{
						goto Label_01DC;
					}
					chArray = psCodigoOriginal.ToCharArray();
					goto Label_01EC;
					Label_0052:
					ch = psCodigoOriginal[index];
					string str = ch.ToString()[0].ToString();
					if (char.IsDigit(str[0]))
					{
						flag2 = false;
						if (str.Equals("9"))
						{
							s = "0";
							flag = true;
						}
						else
						{
							s = (Convert.ToInt32(str) + 1).ToString();
							flag = false;
						}
						chArray[index] = char.Parse(s);
					}
					else if (char.IsLetter(str[0]))
					{
						flag2 = false;
						if (str.Equals("Z"))
						{
							chArray[index] = 'A';
							flag = true;
						}
						else if (str.Equals("z"))
						{
							chArray[index] = 'a';
							flag = true;
						}
						else
						{
							flag = false;
							chArray[index] = (char)(chArray[index] + '\x0001');
						}
					}
					else if (str.Equals(Environment.NewLine))
					{
						flag2 = true;
						flag = false;
						if (char.IsLower(str[0]))
						{
							s = "A";
						}
						else
						{
							s = "a";
						}
						chArray[index] = char.Parse(s);
					}
					else if (index == (length - 1))
					{
						flag = true;
					}
					if ((index == 0) && flag)
					{
						if (chArray.Length == pnLongitudMaxima)
						{
							chArray = psCodigoOriginal.ToCharArray();
						}
						else if (!flag2)
						{
							string str3;
							if (s.Equals("0"))
							{
								str3 = "1" + new string(chArray);
							}
							else if (s.Equals("A"))
							{
								str3 = "A" + new string(chArray);
							}
							else
							{
								str3 = "a" + new string(chArray);
							}
							chArray = str3.ToCharArray();
						}
					}
					index--;
					Label_01DC:
					if ((index >= 0) && flag)
					{
						goto Label_0052;
					}
				}
				catch
				{
				}
			}
			Label_01EC:
			return new string(chArray);
		}

		public void ActualizarConsecutivo(string Consecutivo)
		{
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					var Consecutivo_Anterior = context.consecutivo.Find(1);
					if (Consecutivo_Anterior != null)
					{
						Consecutivo_Anterior.CONSECUTIVO = Consecutivo;
						context.SaveChanges();
					}
				}
				
			}
			catch (Exception Ex)
			{

				throw Ex;
			}
		}
		#endregion

		#region Cabecera consulta
		public CABECERA_PEDIDO_WEB ObtenerCabecera(string CODIGO)
		{
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					var Cabecera = context.pedidocabecera.FirstOrDefault(x => x.CONSECUTIVO == CODIGO);
					return Cabecera;
				}
			}
			catch (Exception Ex)
			{
				throw Ex;
			}

		}
		#endregion
	}
}