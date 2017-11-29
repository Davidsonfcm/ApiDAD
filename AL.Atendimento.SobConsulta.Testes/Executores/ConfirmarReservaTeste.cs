using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using AL.Atendimento.SobConsulta.Repositorios.Reservas;
using AL.Atendimento.SobConsulta.Util.Enumeracoes;
using Localiza.SDK.Fronteira;
using Localiza.SDK.InversaoControle;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Testes.FluxoComunicacao
{
    [TestClass]
    public class ConfirmarReservaTeste : TesteBase
    {
        [TestMethod]
        public void ConfirmarReservaDiferenteDeSobConsulta()
        {
            try
            { 
                var confirmacao = ConfirmarReserva("192208", "MOBC9I49964P", SituacaoReserva.Aberta, MotivoConfirmacaoSobConsulta.AguardandoConfirmacaoManual, null);
                Assert.Fail();
            }
            catch(Exception e)
            {

            }
        }

        private ConfirmarReservaResultado ConfirmarReserva(string codigoUsuario, string localizador, string situacaoReserva, MotivoConfirmacaoSobConsulta motivoConfirmacao, 
            ConfirmacaoAutomaticaRequisicao confirmacaoAutomatica)
        {
            var requisicaoBloqueio = new ConfirmarReservaRequisicao()
            {
                CodigoUsuario= codigoUsuario,
                ConfirmacaoAutomatica= confirmacaoAutomatica,
                Localizador = localizador,
                MotivoConfirmacao = motivoConfirmacao,
                Observacao = "",
                Situacao = situacaoReserva
            };

            var executor = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IExecutor<ConfirmarReservaRequisicao, ConfirmarReservaResultado>>();

            return executor.Executar(requisicaoBloqueio);
        }
    }
}
