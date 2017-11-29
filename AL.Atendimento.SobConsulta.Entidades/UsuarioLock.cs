using AL.Atendimento.SobConsulta.Base.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Entidades
{
    public class UsuarioLock : IEntidade
    {
        public String CodigoUsuario { get; set; }
        public String NomeUsuario { get; set; }

        public IChaveEntidade ObterChave()
        {
            throw new NotImplementedException();
        }
    }
}
