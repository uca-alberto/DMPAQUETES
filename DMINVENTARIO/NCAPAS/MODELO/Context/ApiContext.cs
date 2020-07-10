using System.Data.Entity;
using DMINVENTARIO.NCAPAS.MODELO.Entidades;

namespace DMINVENTARIO.Modelo.Context
{
    public class ApiContext : DbContext
    {
        public ApiContext(string  connectionString)
        {
            Database.Connection.ConnectionString = connectionString;
        }
        public DbSet<ROL_WEB> Rol { get; set; }
        public DbSet<MENU_WEB> Menu { get; set; }
        public DbSet<USUARIO_WEB> Users { get; set; }
        public DbSet<SUBMENU_WEB> SubMenu { get; set; }
		public DbSet<ROL_MENU_WEB> RolMenu { get; set; }
		public DbSet<LINEA_PEDIDO_WEB> pedidolinea { get; set; }
		public DbSet<CABECERA_PEDIDO_WEB> pedidocabecera { get; set; }
		public DbSet<CONSECUTIVO_WEB> consecutivo { get; set; }


	}
}
