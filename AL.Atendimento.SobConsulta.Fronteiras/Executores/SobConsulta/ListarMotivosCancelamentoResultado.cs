using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.SobConsulta;
using Localiza.SDK.Fronteira;
using System.Collections.Generic;

namespace AL.Atendimento.SobConsulta.Fronteiras.Executores.SobConsulta
{
    public class ListarMotivosCancelamentoResultado : IResultado
    {
        public List<MotivoCancelamentoDto> MotivosCancelamento { get; set; }
        public EstadoResultado Estado { get; set; }
    }
}
