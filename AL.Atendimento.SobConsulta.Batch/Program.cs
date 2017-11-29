using Localiza.SDK.Fronteira;
using Localiza.SDK.InversaoControle;
using AL.Atendimento.SobConsulta.Mapeamentos;
using AL.Atendimento.SobConsulta.Util;
using System;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.SobConsulta;

namespace AL.Atendimento.SobConsulta.Batch
{
    static class Program
    {
        static void Main(string[] args)
        {

            //NOME_ROTINA=GERAR_PROVISAO
            //NOME_ROTINA=ENVIAR_PROVISAO_SAP
            //NOME_ROTINA=EXPURGAR_PROVISAO
            //NOME_ROTINA=AGENDAR_GERACAO_PROVISAO
            //NOME_ROTINA=AGENDAR_ENVIO_PROVISAO_SAP
            //NOME_ROTINA=TESTAR_BULK_INSERT

            ResolvedorDeDependencias.Instance().CarregarMapeamentos(Mapeador.Mapeamentos());

            string nomeRotina = "CONFIRMACAO_AUTOMATICA_SOB_CONSULTA";

            if ("CONFIRMACAO_AUTOMATICA_SOB_CONSULTA".Equals(nomeRotina))
                ConfirmacaoAutomaticaDeReservas();
        }
        
        private static void ConfirmacaoAutomaticaDeReservas()
        {
            var executor = ResolvedorDeDependencias.Instance()
                .ObterInstanciaDe<IExecutorSemRequisicao<ProcessarConfirmacaoAutomaticaResultado>>();

            executor.Executar();
        }
    }
}
