using AL.Atendimento.SobConsulta.Executores.SobConsulta;
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
    public class BloquearReservaSobConsultaTeste : TesteBase
    {
        [TestMethod]
        public void BloquearReservaSemBloqueio()
        {
            var requisicaoBloqueio = new BloquearReservaSobConsultaRequisicao()
            {
                Localizador = "AV2A5KC55RA",
                UsuarioBloqueio = "192209"
            };

            var executor = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IExecutor<BloquearReservaSobConsultaRequisicao, BloquearReservaSobConsultaResultado>>();

            var resultado1 = executor.Executar(requisicaoBloqueio);
            requisicaoBloqueio.UsuarioBloqueio = "192208";
            var resultado2 = executor.Executar(requisicaoBloqueio);

            Assert.AreEqual(resultado1.ReservaSobConsulta.Localizador, "AV2A5KC55RA");
            Assert.AreEqual(resultado2.ReservaSobConsulta, null);
        }
    }
}
