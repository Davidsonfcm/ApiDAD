using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Util.Enumeracoes
{
    public static class SituacaoReserva
    {
        public const String Aberta = "1";
        public const String Cancelada = "2";
        public const String Utilizada = "3";
        public const String NoShow = "4";
        public const String NaoConfirmada = "5";
        public const String EmConfirmacao = "6";
        public const String PendentePF = "7";
        public const String PendenteAprovacaoEmbratel = "8";
        public const String SobConsulta = "9";
        public const String EmConsultaInternacional = "C";
        public const String PendenteABAV = "A";
        public const String Finalizada = "Z";
        public const String Negada = "N";
        public const String FrotaIndisponivel = "I";
        public const String Cotacao = "O";
        public const String PendenteDeCredito = "P";
        public const String AguardandoPagamento = "B";
        
    }

}
