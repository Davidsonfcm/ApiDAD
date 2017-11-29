using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Util.Enumeracoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.SobConsulta
{
    public class TelefoneClienteDto : DtoBase<TelefoneCliente, TelefoneClienteDto>
    {
        public string DDD { get; set; }
        public string DDI { get; set; }
        public string Numero { get; set; }
        public TipoTelefone Tipo { get; set; }
    }
}
