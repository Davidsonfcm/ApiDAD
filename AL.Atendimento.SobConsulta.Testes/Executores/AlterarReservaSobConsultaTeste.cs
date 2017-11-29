using AL.Atendimento.SobConsulta.Executores.SobConsulta;
using AL.Atendimento.SobConsulta.Util.Enumeracoes;
using Localiza.SDK.Fronteira;
using Localiza.SDK.InversaoControle;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Testes
{
    [TestClass]
    public class AlterarReservaSobConsultaTeste : TesteBase
    {
        [TestMethod]
        public void AlterarReservaCERESComPrePagamentoViaNR()
        {

            var requisicao = new AlterarReservaSobConsultaRequisicao()
            {
                Localizador = "MOBO155C9KLA",
                CodigoUsuario = "01685781",
                Observacao = "Teste",
                SituacaoReserva = SituacaoReserva.SobConsulta,
            };

            AlterarReservaSobConsultaResultado resultado = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IExecutor<AlterarReservaSobConsultaRequisicao, AlterarReservaSobConsultaResultado>>().Executar(requisicao);
            Assert.IsTrue(resultado.DadosReserva.SituacaoReserva == "9");
        }
        [TestMethod]
        public void AlterarReservaCERESSemPrePagamento()
        {
            var requisicao = new AlterarReservaSobConsultaRequisicao()
            {
                Localizador = "MOBC95K3HJP",
                CodigoUsuario = "03554127",
                Observacao = "Testes",
                SituacaoReserva = SituacaoReserva.SobConsulta
            };

            AlterarReservaSobConsultaResultado resultado = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IExecutor<AlterarReservaSobConsultaRequisicao, AlterarReservaSobConsultaResultado>>().Executar(requisicao);
            Assert.IsTrue(resultado.DadosReserva.SituacaoReserva == "9");
        }
        [TestMethod]
        public void AlterarReservaViaNR()
        {
            var requisicao = new AlterarReservaSobConsultaRequisicao()
            {
                Localizador = "AV2A5CF93U",
                CodigoUsuario = "03554127",
                Observacao = "Testes42",
                SituacaoReserva = SituacaoReserva.SobConsulta,
            };

            AlterarReservaSobConsultaResultado resultado = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IExecutor<AlterarReservaSobConsultaRequisicao, AlterarReservaSobConsultaResultado>>().Executar(requisicao);
            Assert.IsTrue(resultado.DadosReserva.SituacaoReserva == "9");
        }
        [TestMethod]
        public void AlterarReservaInvalida()
        {
            var requisicao = new AlterarReservaSobConsultaRequisicao()
            {
                Localizador = "AVC95GVH3P",
                CodigoUsuario = "01685781",
                Observacao = "Teste",
                SituacaoReserva = SituacaoReserva.Aberta,
            };
            try
            {
                AlterarReservaSobConsultaResultado resultado = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IExecutor<AlterarReservaSobConsultaRequisicao, AlterarReservaSobConsultaResultado>>().Executar(requisicao);
                Assert.Fail();
            }
            catch
            {
                
            }
        }
    }
}
