using AL.Atendimento.SobConsulta.Api.Models;
using AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.ConfiguracoesGrupo;
using Localiza.SDK.Fronteira;
using Localiza.SDK.InversaoControle;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Testes.Executores
{
    [TestClass]
    public class ConfigurarGrupoUpgradeTeste : TesteBase
    {
        [TestMethod]
        public void AlterarGruposSucesso()
        {
            List<ConfiguracaoGrupoDto> configuracoesGrupo = ObterConfiguracoesGrupo();

            foreach (ConfiguracaoGrupoDto configuracao in configuracoesGrupo)
            {
                GravarConfiguracaoGrupoRequisicao requisicao = new GravarConfiguracaoGrupoRequisicao
                {
                    Ativo = configuracao.Ativo,
                    CodigoGrupo = configuracao.CodigoGrupo,
                    ListaUpgrade = configuracao.ListaUpgrade,
                    MaximoProdutivo = configuracao.MaximoProdutivo,
                    MinimoProdutivo = configuracao.MinimoProdutivo,
                    Ordenacao = configuracao.Ordenacao,
                    UsuarioLogado = configuracao.UsuarioLogado
                };

                var executor = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IExecutor<GravarConfiguracaoGrupoRequisicao, GravarConfiguracaoGrupoResultado>>();

                var retorno = new ConfiguracaoGrupoModel(executor.Executar(requisicao).ConfiguracoesGrupo);
            }
        }
        [TestMethod]
        public void AlterarGruposOrdenacaoVazio()
        {
            List<ConfiguracaoGrupoDto> configuracoesGrupo = ObterConfiguracoesGrupoSemOrdenacao();

            foreach (ConfiguracaoGrupoDto configuracao in configuracoesGrupo)
            {
                GravarConfiguracaoGrupoRequisicao requisicao = new GravarConfiguracaoGrupoRequisicao
                {
                    Ativo = configuracao.Ativo,
                    CodigoGrupo = configuracao.CodigoGrupo,
                    ListaUpgrade = configuracao.ListaUpgrade,
                    MaximoProdutivo = configuracao.MaximoProdutivo,
                    MinimoProdutivo = configuracao.MinimoProdutivo,
                    Ordenacao = configuracao.Ordenacao,
                    UsuarioLogado = configuracao.UsuarioLogado
                };
                try { 
                    var executor = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IExecutor<GravarConfiguracaoGrupoRequisicao, GravarConfiguracaoGrupoResultado>>();
                    var retorno = new ConfiguracaoGrupoModel(executor.Executar(requisicao).ConfiguracoesGrupo);
                    Assert.Fail();
                }
                catch { }
            }
        }

        private List<ConfiguracaoGrupoDto> ObterConfiguracoesGrupo()
        {
            List<ConfiguracaoGrupoDto> configuracoesGrupo = new List<ConfiguracaoGrupoDto>();
            ConfiguracaoGrupoDto configuracaoGrupo = new ConfiguracaoGrupoDto();
            List<string> listaUpgrade = new List<string>();
            listaUpgrade.Add("F");
            listaUpgrade.Add("G");
            configuracaoGrupo = new ConfiguracaoGrupoDto
            {
                Ativo = true,
                CodigoGrupo = "C",
                ListaUpgrade = listaUpgrade,
                MaximoProdutivo = 90.9,
                MinimoProdutivo = 79.2,
                Ordenacao = 1,
                UsuarioLogado = "191950"
            };
            listaUpgrade = new List<string>();
            listaUpgrade.Add("GX");
            listaUpgrade.Add("G");
            configuracoesGrupo.Add(configuracaoGrupo);
            configuracaoGrupo = new ConfiguracaoGrupoDto
            {
                Ativo = true,
                CodigoGrupo = "F",
                ListaUpgrade = listaUpgrade,
                MaximoProdutivo = 70.6,
                MinimoProdutivo = 79.2,
                Ordenacao = 2,
                UsuarioLogado = "191950"
            };
            listaUpgrade = new List<string>();

            configuracoesGrupo.Add(configuracaoGrupo);
            configuracaoGrupo = new ConfiguracaoGrupoDto
            {
                Ativo = true,
                CodigoGrupo = "G",
                ListaUpgrade = listaUpgrade,
                MaximoProdutivo = 85,
                MinimoProdutivo = 80,
                Ordenacao = 3,
                UsuarioLogado = "191950"
            };
            configuracoesGrupo.Add(configuracaoGrupo);

            listaUpgrade = new List<string>();
            listaUpgrade.Add("LX");
            configuracaoGrupo = new ConfiguracaoGrupoDto
            {
                Ativo = true,
                CodigoGrupo = "L",
                ListaUpgrade = listaUpgrade,
                MaximoProdutivo = 95,
                MinimoProdutivo = 82,
                Ordenacao = 4,
                UsuarioLogado = "191950"
            };
            configuracoesGrupo.Add(configuracaoGrupo);
            return configuracoesGrupo;
        }

        private List<ConfiguracaoGrupoDto> ObterConfiguracoesGrupoSemOrdenacao()
        {
            List<ConfiguracaoGrupoDto> configuracoesGrupo = new List<ConfiguracaoGrupoDto>();
            ConfiguracaoGrupoDto configuracaoGrupo = new ConfiguracaoGrupoDto();
            List<string> listaUpgrade = new List<string>();
            listaUpgrade.Add("F");
            listaUpgrade.Add("G");
            configuracaoGrupo = new ConfiguracaoGrupoDto
            {
                Ativo = true,
                CodigoGrupo = "C",
                ListaUpgrade = listaUpgrade,
                MaximoProdutivo = 90.9,
                MinimoProdutivo = 79.2,
                Ordenacao = 1,
                UsuarioLogado = "191950"
            };
            listaUpgrade = new List<string>();
            listaUpgrade.Add("GX");
            listaUpgrade.Add("G");
            configuracoesGrupo.Add(configuracaoGrupo);
            configuracaoGrupo = new ConfiguracaoGrupoDto
            {
                Ativo = true,
                CodigoGrupo = "F",
                ListaUpgrade = listaUpgrade,
                MaximoProdutivo = 70.6,
                MinimoProdutivo = 79.2,
                Ordenacao = 2,
                UsuarioLogado = "191950"
            };
            listaUpgrade = new List<string>();

            configuracoesGrupo.Add(configuracaoGrupo);
            configuracaoGrupo = new ConfiguracaoGrupoDto
            {
                Ativo = true,
                CodigoGrupo = "G",
                ListaUpgrade = listaUpgrade,
                MaximoProdutivo = 85,
                MinimoProdutivo = 80,
                Ordenacao = 3,
                UsuarioLogado = "191950"
            };
            configuracoesGrupo.Add(configuracaoGrupo);

            listaUpgrade = new List<string>();
            listaUpgrade.Add("LX");
            configuracaoGrupo = new ConfiguracaoGrupoDto
            {
                Ativo = true,
                CodigoGrupo = "L",
                ListaUpgrade = listaUpgrade,
                MaximoProdutivo = 95,
                MinimoProdutivo = 82,
                UsuarioLogado = "191950"
            };
            configuracoesGrupo.Add(configuracaoGrupo);
            return configuracoesGrupo;
        }

    }
}