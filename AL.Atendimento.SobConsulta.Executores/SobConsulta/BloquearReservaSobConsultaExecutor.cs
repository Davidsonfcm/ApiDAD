using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using AL.Atendimento.SobConsulta.Repositorios.Reservas;
using AL.Atendimento.SobConsulta.Util.Excecoes;
using Localiza.SDK.Configuracoes.Transacao;
using Localiza.SDK.Fronteira;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Executores.SobConsulta
{
    public class BloquearReservaSobConsultaExecutor : IExecutor<BloquearReservaSobConsultaRequisicao, BloquearReservaSobConsultaResultado>
    {
        private readonly ILockSobConsultaRepositorio lockSobConsultaRepositorio;
        private readonly IReservaNrRepositorio reservaNrRepositorio;
        private readonly IOperacoesServiceRepositorio operacoesServiceRepositorio;
        private readonly IInformacoesUsuarioLogadoRepositorio informacoesUsuarioLogadoRepositorio;

        public BloquearReservaSobConsultaExecutor(ILockSobConsultaRepositorio lockSobConsultaRepositorio, IReservaNrRepositorio reservaNrRepositorio, IOperacoesServiceRepositorio operacoesServiceRepositorio, IInformacoesUsuarioLogadoRepositorio informacoesUsuarioLogadoRepositorio)
        {
            this.lockSobConsultaRepositorio = lockSobConsultaRepositorio;
            this.reservaNrRepositorio = reservaNrRepositorio;
            this.operacoesServiceRepositorio = operacoesServiceRepositorio;
            this.informacoesUsuarioLogadoRepositorio = informacoesUsuarioLogadoRepositorio;
        }

        [LocalizaTransacao]
        public BloquearReservaSobConsultaResultado Executar(BloquearReservaSobConsultaRequisicao requisicao)
        {
            if (String.IsNullOrEmpty(requisicao.Localizador))
                throw new ParametroNuloException("Localizador");

            if (String.IsNullOrEmpty(requisicao.UsuarioBloqueio))
                throw new ParametroNuloException("Usuário Bloqueio");

            Reserva reservaParaBloquear = reservaNrRepositorio.ObterReserva(requisicao.Localizador);
            AgenciaEntidade agenciaEntidade = operacoesServiceRepositorio.ObterCodigoSupervisorRegionalAgencia(reservaParaBloquear.Agencia);
            if (agenciaEntidade != null)
            {
                reservaParaBloquear.SupervisorAgenciaRetirada = informacoesUsuarioLogadoRepositorio.ObterUsuarioLogado(agenciaEntidade.MatriculaSupervisor);
                reservaParaBloquear.RegionalAgenciaRetirada = informacoesUsuarioLogadoRepositorio.ObterUsuarioLogado(agenciaEntidade.MatriculaGerente);
            }
            if (reservaParaBloquear == null)
                throw new ParametroNaoEncontradoException("Reserva não encontrada.", "Localizador", requisicao.Localizador, CodigosErro.RESERVA_NAO_ENCONTRADA);

            bool bloqueioRealizado = RealizarBloqueioReserva(requisicao.Localizador, requisicao.UsuarioBloqueio, 0);

            if (!bloqueioRealizado)
                throw new NegocioException($"Reserva já bloqueada para outro usuário.", CodigosErro.RESERVA_BLOQUEADA_POR_OUTRO_USUARIO);

            return new BloquearReservaSobConsultaResultado()
            {
                Estado = EstadoResultado.OK,
                Sucesso = bloqueioRealizado,
                ReservaSobConsulta = ReservaDto.CriarAPartirDeEntidade(reservaParaBloquear)
            };
        }

        private bool RealizarBloqueioReserva(string localizador, string codigoUsuario, int tentativa)
        {
            var bloqueioExecutado = false;

            var bloqueioExistente = lockSobConsultaRepositorio.ObterBloqueio(localizador);

            if (bloqueioExistente != null)
                return bloqueioExistente.UsuarioLock != null && bloqueioExistente.UsuarioLock.CodigoUsuario == codigoUsuario;

            try
            {
                bloqueioExecutado = lockSobConsultaRepositorio.BloquearReserva(localizador, codigoUsuario);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Erro ao bloquear: {ex.Message}");
                if (tentativa < 3)
                    return RealizarBloqueioReserva(localizador, codigoUsuario, tentativa++);
                else
                {
                    return bloqueioExecutado;
                }
            }

            return bloqueioExecutado;
        }
    }
}
