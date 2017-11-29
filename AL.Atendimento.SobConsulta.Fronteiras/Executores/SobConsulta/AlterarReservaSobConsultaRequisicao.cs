using AL.Atendimento.SobConsulta.Util.Enumeracoes;
using System;

namespace AL.Atendimento.SobConsulta.Executores.SobConsulta
{
    public class AlterarReservaSobConsultaRequisicao
    {
        public string Localizador { get; set; }
        public String SituacaoReserva { get; set; }
        public String Observacao { get; set; }
        public string CodigoUsuario { get; set; }
        public string LocalizadorExterno { get; set; }
        public string MotivoCancelamento { get; set; }
        public string MotivoNaoConfirmada { get; set; }
        public bool? EnviarSMS { get; set; }
        public bool? EnviarEmail { get; set; }
    }
}