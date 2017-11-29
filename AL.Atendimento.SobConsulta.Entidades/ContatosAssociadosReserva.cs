using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Entidades
{
    public class ContatosAssociadosReserva
    {
        public string CodigoCliente { get; set; }
        public int CodigoContato { get; set; }
        public string CodigoReserva { get; set; }
        public DateTime? DataUltimoEnvio { get; set; }
        public DateTime? DataResposta { get; set; }
        public int? Resposta { get; set; }
    }
}
