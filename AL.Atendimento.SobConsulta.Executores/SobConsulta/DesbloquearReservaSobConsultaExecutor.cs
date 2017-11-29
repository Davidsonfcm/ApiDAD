using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using AL.Atendimento.SobConsulta.Repositorios.Reservas;
using AL.Atendimento.SobConsulta.Util.Excecoes;
using Localiza.SDK.Fronteira;
using System;

namespace AL.Atendimento.SobConsulta.Executores.SobConsulta
{
    public class DesbloquearReservaSobConsultaExecutor : IExecutor<DesbloquearReservaSobConsultaRequisicao, DesbloquearReservaSobConsultaResultado>
    {
        private readonly ILockSobConsultaRepositorio lockSobConsultaRepositorio;
        private readonly IReservaNrRepositorio reservaNrRepositorio;

        public DesbloquearReservaSobConsultaExecutor(ILockSobConsultaRepositorio lockSobConsultaRepositorio, IReservaNrRepositorio reservaNrRepositorio)
        {
            this.lockSobConsultaRepositorio = lockSobConsultaRepositorio;
            this.reservaNrRepositorio = reservaNrRepositorio;
        }

        public object ReservaDtoreservaNrRepositorio { get; private set; }

        public DesbloquearReservaSobConsultaResultado Executar(DesbloquearReservaSobConsultaRequisicao requisicao)
        {
            if (String.IsNullOrEmpty(requisicao.Localizador))
                throw new ParametroNuloException("Localizador");

            if (String.IsNullOrEmpty(requisicao.UsuarioDesbloqueio))
                throw new ParametroNuloException("Usuário Bloqueio");

            var bloqueio = lockSobConsultaRepositorio.ObterBloqueio(requisicao.Localizador);

            if (bloqueio != null && bloqueio.UsuarioLock != null && bloqueio.UsuarioLock.CodigoUsuario != requisicao.UsuarioDesbloqueio && !requisicao.ForcarDesbloqueio)
                throw new NegocioException($"Reserva já bloqueada para o usuário {bloqueio.UsuarioLock.CodigoUsuario} - {bloqueio.UsuarioLock.NomeUsuario}", CodigosErro.RESERVA_BLOQUEADA_POR_OUTRO_USUARIO);

            Reserva reservaBloqueada = reservaNrRepositorio.ObterReserva(requisicao.Localizador);

            if(reservaBloqueada == null)
                throw new ParametroNaoEncontradoException("Reserva não encontrada.", "Localizador", requisicao.Localizador, CodigosErro.RESERVA_NAO_ENCONTRADA);

            lockSobConsultaRepositorio.DesbloquearReserva(requisicao.Localizador,requisicao.UsuarioDesbloqueio,requisicao.ForcarDesbloqueio);

            return new DesbloquearReservaSobConsultaResultado()
            {
                Estado = EstadoResultado.OK,
                ReservaSobConsulta = ReservaDto.CriarAPartirDeEntidade(reservaBloqueada)
            };
        }

    }
}
