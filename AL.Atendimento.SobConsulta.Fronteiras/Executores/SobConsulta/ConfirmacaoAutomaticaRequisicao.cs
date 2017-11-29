using AL.Atendimento.SobConsulta.Util.Enumeracoes;
using System;

namespace AL.Atendimento.SobConsulta.Fronteiras.Executores.SobConsulta
{
    public class ConfirmacaoAutomaticaRequisicao
    {
        public double? QtdFrotaOriginal { get; set; }
        public double? QtdFrotaProcessada { get; set; }
        public double? QtdFrotaAlugadaOriginal { get; set; }
        public double? QtdFrotaAlugadaProcessada { get; set; }
        public String GrupoConfirmacao { get; set; }
        public int? idProcessamentoAutomatico { get; set; }
    }
}
