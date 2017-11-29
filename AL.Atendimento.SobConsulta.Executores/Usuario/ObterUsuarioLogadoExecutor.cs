using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.Usuario;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using Localiza.SDK.Fronteira;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Executores.Usuario
{
    public class ObterUsuarioLogadoExecutor : IExecutor<ObterUsuarioLogadoRequisicao, ObterUsuarioLogadoResultado>
    {
        private readonly IInformacoesUsuarioLogadoRepositorio obterInformacoesUsuarioLogadoRepositorio;
        public ObterUsuarioLogadoExecutor(IInformacoesUsuarioLogadoRepositorio obterInformacoesUsuarioLogadoRepositorio)
        {
            this.obterInformacoesUsuarioLogadoRepositorio = obterInformacoesUsuarioLogadoRepositorio;
        }

        public ObterUsuarioLogadoResultado Executar(ObterUsuarioLogadoRequisicao requisicao)
        {
            if (requisicao != null)
            {
                var codigoUsuarioLogado = requisicao.CodigoUsuarioLogado;

                Entidades.InformacoesUsuario informacoesUsuarioLogado = obterInformacoesUsuarioLogadoRepositorio.ObterUsuarioLogado(codigoUsuarioLogado);

                return new ObterUsuarioLogadoResultado
                {
                    UsuarioLogado = InformacoesUsuarioDto.CriarAPartirDeEntidade(informacoesUsuarioLogado),
                    Estado = EstadoResultado.OK
                };
            }
            else
                return null;


        }
    }
}
