using Model;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mapping
{
    public class ConsultaMap : EntityTypeConfiguration<Consulta>
    {
        public ConsultaMap()
        {
            this.ToTable("consulta");

            this.HasKey(t => t.identificador);

            this.Property(t => t.identificador).HasColumnName("identificador");
            this.Property(t => t.data).HasColumnName("data");
            this.Property(t => t.animal).HasColumnName("animal");
            this.Property(t => t.diagnostico).HasColumnName("diagnostico");
            this.Property(t => t.usuarioCpf).HasColumnName("usuarioCpf");
        }
    }
}
