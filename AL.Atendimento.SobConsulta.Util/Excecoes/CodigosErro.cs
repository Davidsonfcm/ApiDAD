using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Util.Excecoes
{
    public static class CodigosErro
    {
        public static readonly string RESERVA_NAO_ENCONTRADA = "ERR001";
        public static readonly string RESERVA_JA_BLOQUEADA = "ERR002";
        public static readonly string RESERVA_BLOQUEADA_POR_OUTRO_USUARIO = "ERR003";
        public static readonly string RESERVA_NAO_PODE_SER_ALTERADA = "ERR004";
    }
}
