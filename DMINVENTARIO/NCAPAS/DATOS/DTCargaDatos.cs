using DMINVENTARIO.Modelo.Context;
using DMINVENTARIO.NCAPAS.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace DMINVENTARIO.NCAPAS.DATOS
{
	public class DTCargaDatos
	{
		public static string Conexion = System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString;

		public List<Bodega> ListarBodega(string Compani)
		{
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					string sql = string.Format("SELECT BODEGA,NOMBRE FROM {0}.BODEGA", Compani);
					var Lista = context.Database.SqlQuery<Bodega>(sql).ToList();
					return Lista;
				}
			}
			catch (Exception Ex)
			{
				throw Ex;
			}
		}

		public List<Cliente> listarCliente(string Compani)
		{
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					string sql = string.Format("SELECT CLIENTE,NOMBRE,MONEDA_NIVEL,NIVEL_PRECIO FROM {0}.CLIENTE", Compani);
					var Lista = context.Database.SqlQuery<Cliente>(sql).ToList();
					return Lista;
				}
			}
			catch (Exception EX)
			{
				throw EX;
			}
		}

		public List<Articulo> listarArticulo(string Compani)
		{
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					string sql = string.Format("SELECT ARTICULO AS IdArticulo ,DESCRIPCION AS Descripcion FROM {0}.ARTICULO", Compani);
					var Lista = context.Database.SqlQuery<Articulo>(sql).ToList();
					return Lista;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public string ObtenerConsecutivo(string Compani)
		{
			try
			{
				string Resultado = "";
				using (var context = new ApiContext(Conexion))
				{
					//Buscamos el consecutivo
					var Consecutivo = context.consecutivo.Find(1);
					if (Consecutivo != null)
					{
						string[] Nuevo = { };
						int Numero = 0;
						Nuevo = Consecutivo.CONSECUTIVO.Split('-');
						Numero = Convert.ToInt32(Nuevo[1].ToString()) + 1;
						Resultado = Nuevo[0].ToString() + "-" + Numero;
					}
					else
					{
						Resultado = "Sin Consecutivo";
					}
				}
				return Resultado;
			}
			catch (Exception Ex)
			{
				throw Ex;
			}
		}

		public Articulo ObtenerCostos(string Articulo,string Compani)
		{
			using (var context = new ApiContext(Conexion))
			{
				var respuesta = context.Database.SqlQuery<Articulo>(
								string.Format(@"SELECT
							   DESCRIPCION AS Descripcion,
							   CONVERT(decimal(28,4),COSTO_PROM_LOC) AS CostoLocalFiscal,
							   CONVERT(decimal(28,4),COSTO_PROM_DOL) AS CostoDolarFiscal 
							   FROM {0}.ARTICULO WHERE ARTICULO = '{1}'", Compani,Articulo)).FirstOrDefault();
				if (respuesta==null)
				{
					throw new FaultException("El Articulo no Existe");
				}
				return respuesta;
			}
		}

		public double ObtenerTipoCambio(string Compani)
		{
			string data = DateTime.Now.ToString("MM/dd/yyyy 00:00:00");
			double respuesta = 0;
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					var tipo = context.Database.SqlQuery<double>(
						string.Format(@"SELECT MONTO 
										FROM {0}.TIPO_CAMBIO_HIST 
										WHERE FECHA =@FECHA",Compani),
										new SqlParameter("@FECHA", data)).FirstOrDefault();
					respuesta = tipo;
				}
				//using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL"].ConnectionString))
				//{
				//	con.Open();
				//	using (var cmd = con.CreateCommand())
				//	{
				//		cmd.CommandType = System.Data.CommandType.Text;
				//		cmd.CommandText = "SELECT MONTO FROM ALINSA.TIPO_CAMBIO_HIST WHERE FECHA =@FECHA";
				//		cmd.Parameters.AddWithValue("@FECHA", data);

				//		using (var reader = cmd.ExecuteReader())
				//		{
				//			if (reader.HasRows)
				//			{
				//				reader.Read();
				//				respuesta = Convert.ToDouble(reader["MONTO"].ToString());
				//			}
				//		}

				//	}
				//}
				return respuesta;
			}
			catch (Exception Ex)
			{

				throw Ex;
			}

		}
	}
}