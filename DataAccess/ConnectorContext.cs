using DataAccess.Mapping;
using Model;
using System.Data.Entity;

namespace DataAccess
{
    public partial class ConnectorContext : DbContext
    {
        static ConnectorContext()
        {
            Database.SetInitializer<ConnectorContext>(null);
        }

        public ConnectorContext()
            : base("Name=ConnectorContext")
        {
        }

        public ConnectorContext(bool proxyCreationEnabled = true)
            : base()
        {
            base.Database.CommandTimeout = 60 * 5; 
            base.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
            base.Configuration.LazyLoadingEnabled = true;
            //definir connection string
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Consulta> Consulta { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new ConsultaMap());
        }
    }
}
