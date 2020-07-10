using DMINVENTARIO.Modelo.Context;
using DMINVENTARIO.NCAPAS.MODELO.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBBCFOODS.Ncapas;

namespace DMINVENTARIO.NCAPAS.DATOS
{
	public class DTUsuario:IGENERIC<USUARIO_WEB>
	{
		public static string Conexion = System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString;

		public bool Eliminar(USUARIO_WEB obj)
		{
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					using (var scope = context.Database.BeginTransaction())
					{
						var Usuario = context.Users.Find(obj.ID_USUARIO);
						if (Usuario != null)
						{
							context.Users.Attach(Usuario);
							context.Users.Remove(Usuario);
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

		public bool Insertar(USUARIO_WEB obj)
		{
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					using (var scope = context.Database.BeginTransaction())
					{
						context.Users.Add(obj);
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

		public List<USUARIO_WEB> listarTodo()
		{
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					var lista = context.Users.ToList();
					return lista;
				}
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public bool Login(string USUARIO, string PASSWORD,ref USUARIO_WEB user)
		{
			
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					var Usuario = context.Users.FirstOrDefault(x => x.USUARIO == USUARIO && x.PASS == PASSWORD);
					if (Usuario != null)
					{
						user = Usuario;
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
				throw Ex;
			}
		}

		public bool Modificar(USUARIO_WEB obj)
		{
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					using (var scope = context.Database.BeginTransaction())
					{
						var Usuario = context.Users.Find(obj.ID_USUARIO);
						if (Usuario != null)
						{
							Usuario.USUARIO = obj.USUARIO;
							Usuario.PASS = obj.PASS;
							Usuario.ID_ROL = obj.ID_ROL;
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