using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Executores.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
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
    public class DesbloquearReservaSobConsultaTeste : TesteBase
    {

        [TestMethod]
        public void MultiplasRequisicoesMesmoUsuarioEReserva()
        {
            List<String> listaReservas = new List<string>();

            int quantidadeBloqueioComSucesso = 0;
            int codigoUsuario = 1;

            for (int i = 0; i < 20; i++)
                listaReservas.Add("AV2A5KC55RA");

            Parallel.ForEach(listaReservas, (reserva) =>
            {
                if(BloquearReserva(reserva, (codigoUsuario++).ToString()))
                {
                    quantidadeBloqueioComSucesso++;
                }
            });

            Assert.IsTrue(quantidadeBloqueioComSucesso == 1);
        }

        [TestMethod]
        public void DesbloquearReservaMesmoUsuario()
        {
            BloquearReserva("IT2A5KC55RA", "192225");
            var bloqueio = ObterBloqueio("IT2A5KC55RA");
            Assert.IsTrue(bloqueio != null);

            DesloquearReserva("IT2A5KC55RA", "192225");
            var desbloqueio = ObterBloqueio("IT2A5KC55RA");
            Assert.IsTrue(desbloqueio == null);
        }

        [TestMethod]
        public void DesbloquearReservaUsuarioDiferente()
        {
            BloquearReserva("AV2A5KC55RA", "192208");
            var bloqueio = ObterBloqueio("AV2A5KC55RA");
            Assert.IsTrue(bloqueio != null);

            try
            {
                DesloquearReserva("AV2A5KC55RA", "192207");
                Assert.Fail();
            }
            catch (Exception e)
            {

            }
        }

        [TestMethod]
        public void ForcarDesbloqueio()
        {
            BloquearReserva("AV2A5KC55RA", "192208");
            var bloqueio = ObterBloqueio("AV2A5KC55RA");
            Assert.IsTrue(bloqueio != null);

            DesloquearReserva("AV2A5KC55RA", "192207", true);
            var desbloqueio = ObterBloqueio("AV2A5KC55RA");
            Assert.IsTrue(desbloqueio == null);
        }

        private bool BloquearReserva(string localizador, string usuario)
        {
            var requisicaoBloqueio = new BloquearReservaSobConsultaRequisicao()
            {
                Localizador = localizador,
                UsuarioBloqueio = usuario
            };

            var executor = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IExecutor<BloquearReservaSobConsultaRequisicao, BloquearReservaSobConsultaResultado>>();

            return executor.Executar(requisicaoBloqueio).Sucesso;
        }

        private void DesloquearReserva(string localizador, string usuario, bool forcarDesbloqueio = false)
        {
            var requisicaoBloqueio = new DesbloquearReservaSobConsultaRequisicao()
            {
                Localizador = localizador,
                UsuarioDesbloqueio = usuario,
                ForcarDesbloqueio = forcarDesbloqueio
            };

            var executor = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IExecutor<DesbloquearReservaSobConsultaRequisicao, DesbloquearReservaSobConsultaResultado>>();

            executor.Executar(requisicaoBloqueio);
        }

        private LockSobConsulta ObterBloqueio(string localizador)
        {
            var repositorio = ResolvedorDeDependencias.Instance().ObterInstanciaDe<ILockSobConsultaRepositorio>();

            return repositorio.ObterBloqueio(localizador);
        }
    }
}
