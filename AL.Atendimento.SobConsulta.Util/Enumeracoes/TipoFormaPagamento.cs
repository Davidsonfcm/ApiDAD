using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Util.Enumeracoes
{
    public enum TipoFormaPagamento
    {
        PagamentoNoBalcao = 0,
        CartaoVirtual = 1,
        FaturamentoParaAgenciaDeViagem = 2,
        FaturamentoParaEmpresa = 3,
        PagamentoPF = 4,
        FaturamentoParaSeguradora = 5
    }
}
