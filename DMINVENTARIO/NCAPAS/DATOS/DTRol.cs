using DMINVENTARIO.Modelo.Context;
using DMINVENTARIO.NCAPAS.MODELO.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBBCFOODS.Ncapas;

namespace DMINVENTARIO.NCAPAS.DATOS
{
	public class DTRol : IGENERIC<ROL_WEB>
	{
		public static string Conexion = System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
		public bool Eliminar(ROL_WEB obj)
		{
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					using (var scope = context.Database.BeginTransaction())
					{
						var Rol = context.Rol.Find(obj.ID_ROL);
						if (Rol != null)
						{
							context.Rol.Attach(Rol);
							context.Rol.Remove(Rol);
							context.SaveChanges();
							scope.Commit();
							return true;
						}
						else
						{
							return false;
						}
					}
				}

			}
			catch (Exception EX)
			{
				throw EX;
			}
		}

		public bool Insertar(ROL_WEB obj)
		{
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					using (var scope = context.Database.BeginTransaction())
					{
						context.Rol.Add(obj);
						context.SaveChanges();
						scope.Commit();
					}
				}
				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<ROL_WEB> listarTodo()
		{
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					var lista = context.Rol.ToList();
					return lista;
				}
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public bool Modificar(ROL_WEB obj)
		{
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					using (var scope = context.Database.BeginTransaction())
					{
						var Rol = context.Rol.Find(obj.ID_ROL);
						if (Rol != null)
						{
							Rol.ROL = obj.ROL;
							Rol.FECHA_MODIFICACION = DateTime.Now;
							context.SaveChanges();
							scope.Commit();
							return true;
						}
						else
						{
							return false;
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}