using AL.Atendimento.SobConsulta.Base.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Entidades
{
    public class LockSobConsulta : IEntidade
    {
        public String Localizador { get; set; }
        public UsuarioLock UsuarioLock { get; set; }
        public DateTime DataLock { get; set; }

        public IChaveEntidade ObterChave()
        {
            return new ChaveEntidadeString(Localizador);
        }
    }
}
