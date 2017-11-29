using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.SobConsulta;
using Localiza.SDK.Fronteira;
using System.Collections.Generic;

namespace AL.Atendimento.SobConsulta.Fronteiras.Executores.SobConsulta
{
    public class ListarMotivosNaoConfirmacaoResultado : IResultado
    {
        public List<MotivoNaoConfirmacaoDto> MotivosNaoConfirmacao { get; set; }
        public EstadoResultado Estado { get; set; }
    }
}
