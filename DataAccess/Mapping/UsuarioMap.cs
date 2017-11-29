using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping
{
    public class UsuarioMap:EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            this.ToTable("usuario");

            this.HasKey(t => t.cpf);

            this.Property(t => t.nome).HasColumnName("nome");
            this.Property(t => t.email).HasColumnName("email");
            this.Property(t => t.cpf).HasColumnName("cpf");
            this.Property(t => t.senha).HasColumnName("senha");
            this.Property(t => t.tipo).HasColumnName("tipo");
            this.Property(t => t.tentativas).HasColumnName("tentativas");
            this.Property(t => t.bloqueado).HasColumnName("bloqueado");
        }
    }
}
