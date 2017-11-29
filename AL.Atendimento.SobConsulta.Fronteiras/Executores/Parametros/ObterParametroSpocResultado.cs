using AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.Parametros;
using Localiza.SDK.Fronteira;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Fronteiras.Executores.Parametros
{
    public class ObterParametroSpocResultado : IResultado
    {
        public EstadoResultado Estado { get; set; }
        public List<ParametroSpocDto> Parametro { get; set; }

    }
}
