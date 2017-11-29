using AL.Atendimento.SobConsulta.Util.Enumeracoes;
using System;

namespace AL.Atendimento.SobConsulta.Fronteiras.Executores.SobConsulta
{
    public class ConfirmarReservaRequisicao
    {
        public string Localizador { get; set; }
        public String Situacao { get; set; }
        public string Observacao { get; set; }
        public string CodigoUsuario { get; set; }
        public string LocalizadorExterno { get; set; }
        public string MotivoCancelamento { get; set; }
        public string MotivoNaoConfirmada { get; set; }
        public bool? EnviarSMS { get; set; }
        public bool? EnviarEmail { get; set; }
        public MotivoConfirmacaoSobConsulta MotivoConfirmacao { get; set; }
        public ConfirmacaoAutomaticaRequisicao ConfirmacaoAutomatica { get; set; }
    }
}
