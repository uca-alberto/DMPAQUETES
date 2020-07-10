using DMINVENTARIO.Modelo.Context;
using DMINVENTARIO.NCAPAS.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace DMINVENTARIO.NCAPAS.DATOS
{
	public class DTPaquetes
	{
		public static string Conexion = System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
		string ConsecutivoCompra = ConfigurationManager.AppSettings["ConsecutivoCompra"];
		string ConsecutivoAjuste = ConfigurationManager.AppSettings["ConsecutivoAjuste"];
		string ConsecutivoTraspaso = ConfigurationManager.AppSettings["ConsecutivoTraspaso"];

		public string ObtenerConsecutivoCompra(string Companie)
		{
			try
			{
				string consecutivoNuevo = "";

				using (var context = new ApiContext(Conexion))
				{
					string consecutivo = context.Database.SqlQuery<string>(string.Format("SELECT SIGUIENTE_CONSEC FROM {0}.CONSECUTIVO_CI WHERE CONSECUTIVO = '{1}'",Companie,ConsecutivoCompra)).FirstOrDefault();
					consecutivoNuevo = ProximoCodigo(consecutivo, consecutivo.Length);
				}
				return consecutivoNuevo;
			}
			catch (Exception)
			{
				throw;
			}
			
		}

		public string ObtenerConsecutivoAjuste(string Companie)
		{
			try
			{
				string consecutivoNuevo = "";

				using (var context = new ApiContext(Conexion))
				{
					string consecutivo = context.Database.SqlQuery<string>(string.Format("SELECT SIGUIENTE_CONSEC FROM {0}.CONSECUTIVO_CI WHERE CONSECUTIVO = '{1}'", Companie, ConsecutivoAjuste)).FirstOrDefault();
					consecutivoNuevo = ProximoCodigo(consecutivo, consecutivo.Length);
				}
				return consecutivoNuevo;
			}
			catch (Exception)
			{
				throw;
			}

		}

		public string ObtenerConsecutivoTraspaso(string Companie)
		{
			try
			{
				string consecutivoNuevo = "";

				using (var context = new ApiContext(Conexion))
				{
					string consecutivo = context.Database.SqlQuery<string>(
						string.Format(@"SELECT SIGUIENTE_CONSEC 
										FROM {0}.CONSECUTIVO_CI 
										WHERE CONSECUTIVO = '{1}'", Companie, ConsecutivoTraspaso)).FirstOrDefault();
					consecutivoNuevo = ProximoCodigo(consecutivo, consecutivo.Length);
				}
				return consecutivoNuevo;
			}
			catch (Exception)
			{
				throw;
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

		public void InsertarTransaccion(TraspasoInv traspaso,string Companie)
		{
			using (var context = new ApiContext(Conexion))
			{
				//Validamos referencia
				if (string.IsNullOrEmpty(traspaso.REFERENCIA))
				{
					traspaso.REFERENCIA = " ";
				}
				//Validacion de Fecha Contable
				var Fecha = context.Database.SqlQuery<DateTime>(
					string.Format(@"SELECT 
									FECHA_FINAL 
									FROM {0}.PERIODO_CONTABLE
									ORDER BY FECHA_FINAL DESC", Companie)).FirstOrDefault();
				if (Fecha!=null)
				{
					if (traspaso.FECHA_DOCUMENTO <= Fecha)
					{
						string sql = string.Format(@"INSERT INTO [{0}].[DOCUMENTO_INV]([PAQUETE_INVENTARIO]
									   ,[DOCUMENTO_INV]
									   ,[CONSECUTIVO]
									   ,[REFERENCIA]
									   ,[FECHA_HOR_CREACION]
									   ,[FECHA_DOCUMENTO]
									   ,[SELECCIONADO]
									   ,[USUARIO]
									   ,[MENSAJE_SISTEMA]
									   ,[NoteExistsFlag]
									   ,[RecordDate]
									   ,[CreatedBy]
									   ,[UpdatedBy]
									   ,[CreateDate])
										 VALUES
									   ('{1}'
									   , '{2}'
									   , '{3}'
									   , '{4}'
									   , @FechaDocumento
									   , GETDATE()
									   , 'N'
									   , 'sa'
									   , '{5}'
									   , 0
									   , GETDATE()
									   , 'sa'
									   , 'sa'
									   , GETDATE())", 
									   Companie, 
									   traspaso.PAQUETE_INVENTARIO, 
									   traspaso.DOCUMENTO_INV, 
									   traspaso.CONSECUTIVO, 
									   traspaso.REFERENCIA, 
									   traspaso.MENSAJE_SISTEMA);
						context.Database.ExecuteSqlCommand(sql,
							new SqlParameter("@FechaDocumento", traspaso.FECHA_DOCUMENTO));

						//Actualizamos consecutivo en CI
						context.Database.ExecuteSqlCommand(
							string.Format(@"UPDATE 
											{0}.CONSECUTIVO_CI 
											SET SIGUIENTE_CONSEC = '{1}' 
											WHERE CONSECUTIVO = '{2}'",
											Companie,
											traspaso.DOCUMENTO_INV,
											traspaso.TIPOTRAN));

						//Insertamos las Lineas
						int linea = 1;
						foreach (var item in traspaso.traspasolinea)
						{
							context.Database.ExecuteSqlCommand(
								string.Format(@"INSERT INTO [{0}].[LINEA_DOC_INV]
											   ([PAQUETE_INVENTARIO]
											   ,[DOCUMENTO_INV]
											   ,[LINEA_DOC_INV]
											   ,[AJUSTE_CONFIG]
											   ,[ARTICULO]
											   ,[BODEGA]
											   ,[TIPO]
											   ,[SUBTIPO]
											   ,[SUBSUBTIPO]
											   ,[CANTIDAD]
											   ,[COSTO_TOTAL_LOCAL]
											   ,[COSTO_TOTAL_DOLAR]
											   ,[PRECIO_TOTAL_LOCAL]
											   ,[PRECIO_TOTAL_DOLAR]
											   ,[BODEGA_DESTINO]
											   ,[NoteExistsFlag]
											   ,[RecordDate]
											   ,[CreatedBy]
											   ,[UpdatedBy]
											   ,[CreateDate])
										 VALUES
											   ('{1}'
											   ,'{2}'
											   , {3}
											   ,'{4}'
											   ,'{5}'
											   ,'{6}'
											   ,'{7}'
											   ,'{8}'
											   ,' '
											   ,{9}
											   ,@COSTO_TOTAL_LOCAL
											   ,@COSTO_TOTAL_DOLAR
											   ,@PRECIO_TOTAL_LOCAL
											   ,@PRECIO_TOTAL_DOLAR
											   ,'{10}'
											   ,0
											   ,GETDATE()
											   ,'sa'
											   ,'sa'
											   ,GETDATE())",
											   Companie,
											   item.PAQUETE_INVENTARIO,
											   item.DOCUMENTO_INV,
											   linea,
											   item.AJUSTE_CONFIG,
											   item.ARTICULO,
											   item.BODEGA,
											   item.TIPO,
											   item.SUBTIPO,
											   item.CANTIDAD,
											   item.BODEGA_DESTINO
											   ),
												new SqlParameter("@COSTO_TOTAL_LOCAL", item.COSTO_TOTAL_LOCAL),
												new SqlParameter("@COSTO_TOTAL_DOLAR", item.COSTO_TOTAL_DOLAR),
												new SqlParameter("@PRECIO_TOTAL_LOCAL", item.PRECIO_TOTAL_LOCAL),
												new SqlParameter("@PRECIO_TOTAL_DOLAR", item.PRECIO_TOTAL_DOLAR));
							linea++;
						}
					}
					else
					{
						throw new FaultException("La fecha indicada no corresponde a un Periodo Contable");
					}
				}
				
				//Ejecutamos el Insert
			}
		}
	}
}