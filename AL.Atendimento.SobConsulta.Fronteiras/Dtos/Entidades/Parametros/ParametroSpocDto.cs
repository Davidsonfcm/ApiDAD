using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.Parametros
{
    public class ParametroSpocDto : DtoBase<Atendimento.SobConsulta.Entidades.Parametros.ParametroSpoc, ParametroSpocDto>
    {
        public string Valor { get; set; }
    }
}
