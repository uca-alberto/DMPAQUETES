using DMINVENTARIO.Modelo.Context;
using DMINVENTARIO.NCAPAS.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using WEBBCFOODS.Ncapas;

namespace DMINVENTARIO.NCAPAS.DATOS
{

	public class DTArticulo : IGENERIC<Articulo>
	{
		public static string Conexion = System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString;

		public bool Eliminar(Articulo obj)
		{
			throw new NotImplementedException();
		}

		public bool Insertar(Articulo obj)
		{
			throw new NotImplementedException();
		}

		public List<Articulo> listarTodo()
		{
			throw new NotImplementedException();
		}

		public bool Modificar(Articulo obj)
		{
			throw new NotImplementedException();
		}

		public Articulo ObtenerArticulo(string CodArticulo,Filtros filtro,string Compani)
		{
			var retorno = new Articulo();
			using (var context = new ApiContext(Conexion))
			{
				//Mandamos a buscar el articulo
				string sql = string.Format(@"SELECT 
											ARTICULO AS IdArticulo,
											DESCRIPCION AS Descripcion,
											Convert(decimal(28,2),COSTO_ULT_LOC) AS CostoLocalFiscal,
											Convert(decimal(28,2),COSTO_ULT_DOL) AS CostoDolarFiscal
											FROM {0}.ARTICULO 
											WHERE ARTICULO ='{1}'", Compani,CodArticulo);
				var Articulo = context.Database.SqlQuery<Articulo>(sql).FirstOrDefault();
				//Validamos que Exista
				if (Articulo!=null)
				{
					//Mandamos a buscar las transacciones Inv de ese articulo
					string sqlInv = string.Format(@"SELECT 
													Convert(varchar(50),inv.FECHA_HORA_TRANSAC,103) AS FechaHora,
													inv.ARTICULO AS IdArticulo,
													art.DESCRIPCION AS DescripcionArticulo,
													Convert(decimal(28,2),inv.CANTIDAD) AS Cantidad,
													inv.BODEGA AS Bodega,
													Convert(decimal(28,2),inv.COSTO_TOT_FISC_LOC) as CostoLocal,
													Convert(decimal(28,2),inv.COSTO_TOT_FISC_DOL) as CostoDolar,
													Convert(decimal(28,2),inv.COSTO_TOT_COMP_LOC) as CostoComparativoLocal,
													Convert(decimal(28,2),inv.COSTO_TOT_COMP_DOL) as CostoComparativoDolar,
													Convert(decimal(28,2),inv.PRECIO_TOTAL_LOCAL) as PrecioLocal,
													Convert(decimal(28,2),inv.PRECIO_TOTAL_DOLAR) as PrecioDolar,
													CASE	
													WHEN inv.AJUSTE_CONFIG = '~OO~' THEN 'COMPRA' 
													WHEN inv.AJUSTE_CONFIG = '~FF~' THEN 'AJUSTE' 
													WHEN inv.AJUSTE_CONFIG = '~VV~' THEN 'VENTA' 
													ELSE 'OTROS' 
													END AS Tipo 
													FROM {0}.TRANSACCION_INV inv 
													INNER JOIN {0}.ARTICULO art on inv.ARTICULO = art.ARTICULO												 WHERE inv.ARTICULO = '{1}' 
													AND inv.FECHA_HORA_TRANSAC 
													BETWEEN '{2}' AND '{3}'
													ORDER BY inv.FECHA_HORA_TRANSAC", 
													Compani, CodArticulo,
													filtro.FechaInicial,filtro.FechaFinal);
					var Inv = context.Database.SqlQuery<TransaccionInv>(sqlInv).ToList();
					var ListaInv = new List<TransaccionInv>();
					foreach (var item in Inv)
					{
						ListaInv.Add(new TransaccionInv
						{
							IdArticulo = item.IdArticulo,
							DescripcionArticulo = item.DescripcionArticulo,
							FechaHora = item.FechaHora,
							Cantidad = item.Cantidad,
							Bodega = item.Bodega,
							Tipo = item.Tipo,
							CostoLocal = item.CostoLocal,
							CostoDolar = item.CostoDolar,
							CostoComparativoLocal = item.CostoComparativoLocal,
							CostoComparativoDolar = item.CostoComparativoDolar,
							PrecioLocal = item.PrecioLocal,
							PrecioDolar = item.PrecioDolar
						});
					}
					//Asignamos la lista a la variable de retorno
					Articulo.TransaccionInv = ListaInv;

					//Mandamos a buscar la existencia en bodega
					string sqlExistencia = string.Format(@"SELECT 
															exi.ARTICULO AS IdArticulo,
															art.DESCRIPCION AS ArticuloDescripcion,
															exi.BODEGA AS Bodega,
															Convert(decimal(28, 2), exi.CANT_DISPONIBLE) AS CantidadDisponible 
															FROM {0}.EXISTENCIA_BODEGA exi 
															INNER JOIN {0}.ARTICULO art on exi.ARTICULO = art.ARTICULO 
															WHERE exi.ARTICULO = '{1}'", Compani, CodArticulo);
					var Exi = context.Database.SqlQuery<ExistenciaBodega>(sqlExistencia).ToList();
					var ListaExistencia = new List<ExistenciaBodega>();
					foreach (var item in Exi)
					{
						ListaExistencia.Add(new ExistenciaBodega
						{
							IdArticulo = item.IdArticulo,
							ArticuloDescripcion = item.ArticuloDescripcion,
							Bodega = item.Bodega,
							CantidadDisponible = item.CantidadDisponible
						});
					}
					Articulo.ExistenciaBodega = ListaExistencia;

					//Buscamos el precio de este articulo
					string sqlprecio = string.Format(@"SELECT 
														pre.ARTICULO AS Articulo,
														art.DESCRIPCION AS ArticuloDescripcion,
														Convert(decimal(28, 2), pre.PRECIO)AS Precio 
														FROM {0}.ARTICULO_PRECIO pre 
														INNER JOIN {0}.ARTICULO art on pre.ARTICULO = art.ARTICULO 
														WHERE pre.ARTICULO = '{1}'", Compani, CodArticulo);
					var Pre = context.Database.SqlQuery<ArticuloPrecio>(sqlprecio).FirstOrDefault();
					if (Pre!=null)
					{
						Articulo.ArticuloPrecio = Pre;
					}
					retorno = Articulo;
				}
				else
				{
					throw new FaultException("El Articulo no Existe");
				}
				
			}
			return retorno;
		}

		public decimal ObtenerPrecio(string Codigo, string Nivel_Precio, string Moneda,string Compani)
		{
			string Mo = "";
			if (Moneda.Equals("NIO"))
			{
				Mo = "L";
			}
			else
			{
				Mo = "D";
			}

			try
			{
				decimal respuesta = 0;
				using (var context = new ApiContext(Conexion))
				{
					string sql = string.Format(@"SELECT 
												ISNULL(Convert(decimal(28,2),PRECIO),0)AS PRECIO 
												FROM {0}.ARTICULO_PRECIO 
												where ARTICULO = '{1}' 
												and NIVEL_PRECIO = '{2}' 
												AND MONEDA = '{3}'",Compani,Codigo,Nivel_Precio,Moneda);
					var precio = context.Database.SqlQuery<decimal>(sql).FirstOrDefault();
					respuesta = precio;
				}
				return respuesta;
			}
			catch (Exception Ex)
			{
				throw Ex;
			}

		}
	}
}