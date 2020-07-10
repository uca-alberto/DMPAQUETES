using DMINVENTARIO.Modelo.Context;
using DMINVENTARIO.NCAPAS.ENTIDADES;
using DMINVENTARIO.NCAPAS.MODELO.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using WEBBCFOODS.Ncapas;

namespace DMINVENTARIO.NCAPAS.DATOS
{
	public class DTMenu : IGENERIC<ROL_MENU_WEB>
	{
		public static string Conexion = System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
		public bool Eliminar(ROL_MENU_WEB obj)
		{
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					var menu = context.RolMenu.FirstOrDefault(x => x.ID_ROL == obj.ID_ROL && x.ID_MENU == obj.ID_MENU);
					if (menu!=null)
					{
						context.RolMenu.Remove(menu);
						context.SaveChanges();
					}
					else
					{
						throw new FaultException("El Menu no Existe");
					}
				}
				return true;
			}
			catch (Exception Ex)
			{
				throw new FaultException(Ex.Message);
			}
			
		}

		public bool Insertar(ROL_MENU_WEB obj)
		{
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					var menu = context.RolMenu.FirstOrDefault(x => x.ID_MENU == obj.ID_MENU && x.ID_ROL == obj.ID_ROL);
					if (menu == null)
					{
						context.RolMenu.Add(obj);
						context.SaveChanges();
					}
					else
					{
						throw new FaultException("El Menu ya esta asignado");
					}
				}
				return true;

			}
			catch (Exception Ex)
			{
				throw new FaultException(Ex.Message);
			}
		}

		public List<ROL_MENU_WEB> listarTodo()
		{
			throw new NotImplementedException();
		}

		public bool Modificar(ROL_MENU_WEB obj)
		{
			throw new NotImplementedException();
		}

		public List<MENU> ObtenerMenuRol(int RolID)
		{
			var Lista = new List<MENU>();
			using (var context = new ApiContext(Conexion))
			{
				var RolMenu = context.RolMenu.Where(x => x.ID_ROL == RolID).ToList();
				foreach (var item in RolMenu)
				{
					var menu = context.Menu.FirstOrDefault(x=>x.ID_MENU == item.ID_MENU);
					if (menu !=null )
					{
						var ListSubmenu = new List<SUBMENU>();
						var submenu = context.SubMenu.Where(x=>x.ID_MENU == menu.ID_MENU).ToList();
						foreach (var itemSub in submenu)
						{
							ListSubmenu.Add(new SUBMENU
							{
								IdSubMenu = itemSub.ID_SUBMENU,
								Descripcion = itemSub.DESCRIPCION,
								Url = itemSub.URL
							});
						}
						Lista.Add(new MENU
						{
							IdMenu = item.ID_MENU,
							Descripcion = menu.DESCRIPCION,
							ListSubMenu = ListSubmenu
						});
					}
				}
			}
			return Lista;
		}

		public List<MENU_WEB> ObtenerMenuWeb()
		{
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					var menu = context.Menu.ToList();
					return menu;
				}
			}
			catch (Exception)
			{
				throw;
			}
			
		}

		public List<SUBMENU_WEB> ObtenerSubMenuWeb(int id)
		{
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					var submenu = context.SubMenu.Where(x=>x.ID_MENU == id).ToList();
					return submenu;
				}
			}
			catch (Exception)
			{
				throw;
			}

		}

		public List<ROL_WEB> ObtenerRolWeb()
		{
			try
			{
				using (var context = new ApiContext(Conexion))
				{
					var rol = context.Rol.ToList();
					return rol;
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}