using AL.Atendimento.SobConsulta.Executores.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.ConfiguracoesGrupo;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using AL.Atendimento.SobConsulta.RepositoriosMock;
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
    public class ListarConfiguracoesGrupoTeste : TesteBase
    {
        [TestMethod]
        public void ListarConfiguracoesGrupos()
        {
            ListarConfiguracoesGrupoResultado resultado = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IExecutor<ListarConfiguracoesGrupoRequisicao, ListarConfiguracoesGrupoResultado>>().Executar(new ListarConfiguracoesGrupoRequisicao());
            var configuracaoGrupos = resultado.ConfiguracoesGrupo;

            Assert.IsTrue(configuracaoGrupos != null);
        }

        [TestMethod]
        public void ListarConfiguracoesGrupo(string grupo)
        {
            var repositorio = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IConfiguracaoGruposRepositorio>();
            var configuracaoGrupo = repositorio.ObterConfiguracaoGrupo("C");
        }
    }
}
