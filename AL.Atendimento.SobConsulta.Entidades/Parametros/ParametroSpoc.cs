using AL.Atendimento.SobConsulta.Base.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Entidades.Parametros
{
    public class ParametroSpoc :IEntidade
    {
        public string Valor { get; set; }

        public IChaveEntidade ObterChave()
        {
            throw new NotImplementedException();
        }
    }
}
