using AL.Atendimento.SobConsulta.Util.Enumeracoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Entidades
{
    public class TelefoneCliente
    {
        public string DDD { get; set; }
        public string DDI { get; set; }
        public string Numero { get; set; }
        public TipoTelefone Tipo { get; set; }

    }
}
