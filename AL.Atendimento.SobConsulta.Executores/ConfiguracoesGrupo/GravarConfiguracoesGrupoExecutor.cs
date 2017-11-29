using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.ConfiguracoesGrupo;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using AL.Atendimento.SobConsulta.Repositorios.Reservas;
using AL.Atendimento.SobConsulta.Util;
using AL.Atendimento.SobConsulta.Util.Excecoes;
using Localiza.SDK.Fronteira;
using System;
using System.Linq;

namespace AL.Atendimento.SobConsulta.Executores.Contrato
{
    public class GravarConfiguracoesGrupoExecutor : IExecutor<GravarConfiguracaoGrupoRequisicao, GravarConfiguracaoGrupoResultado>
    {
        private readonly IConfiguracaoGruposRepositorio configuracoesGrupoRepositorio;
        
        public GravarConfiguracoesGrupoExecutor(IConfiguracaoGruposRepositorio configuracoesGrupoRepositorio)
        {
            this.configuracoesGrupoRepositorio = configuracoesGrupoRepositorio;
        }

        public GravarConfiguracaoGrupoResultado Executar(GravarConfiguracaoGrupoRequisicao requisicao)
        {
            if (requisicao.Ativo && requisicao.Ordenacao == 0)
                throw new ParametroNuloException("Ordenacao");

            if (String.IsNullOrEmpty(requisicao.CodigoGrupo))
                throw new ParametroNuloException("CodigoGrupo");

            if(requisicao.MinimoProdutivo == 0)
                throw new ParametroNuloException("MinimoProdutivo");

            if (requisicao.MaximoProdutivo == 0)
                throw new ParametroNuloException("MaximoProdutivo");

            var configuracaoDoGrupo = configuracoesGrupoRepositorio.ObterConfiguracaoGrupo(requisicao.CodigoGrupo);

            ConfiguracaoGrupos configuracao = configuracaoDoGrupo ?? new ConfiguracaoGrupos();

            configuracao.Ativo = requisicao.Ativo;
            configuracao.ListaUpgrade = requisicao.ListaUpgrade;
            configuracao.MaximoProdutivo = requisicao.MaximoProdutivo;
            configuracao.MinimoProdutivo = requisicao.MinimoProdutivo;
            configuracao.Ordenacao = requisicao.Ordenacao;
            configuracao.CodigoUsuario = requisicao.UsuarioLogado;

            if (configuracaoDoGrupo != null)
            {
                configuracoesGrupoRepositorio.Alterar(configuracao);
            }
            else
            {
                configuracao.CodigoGrupo = requisicao.CodigoGrupo;
                configuracoesGrupoRepositorio.Gravar(configuracao);
            }
            
            return new GravarConfiguracaoGrupoResultado
            {
                ConfiguracoesGrupo = ConfiguracaoGrupoDto.CriarAPartirDeEntidade(configuracao),
                Estado = EstadoResultado.OK
            };
        }
    }
}
