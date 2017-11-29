using AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using AL.Atendimento.SobConsulta.Repositorios.Reservas;
using AL.Atendimento.SobConsulta.Util;
using AL.Atendimento.SobConsulta.Util.Excecoes;
using Localiza.SDK.Fronteira;
using System.Linq;
using System;
using AL.Atendimento.SobConsulta.Entidades;

namespace AL.Atendimento.SobConsulta.Executores.Contrato
{
    public class ObterReservaSobConsultaExecutor : IExecutor<ObterReservaSobConsultaRequisicao, ObterReservaSobConsultaResultado>
    {
        private readonly IReservaNrRepositorio reservaRepositorio;
        private readonly ILockSobConsultaRepositorio lockSobConsultaRepositorio;
        private readonly IOperacoesServiceRepositorio operacoesServiceRepositorio;
        private readonly IInformacoesUsuarioLogadoRepositorio informacoesUsuarioLogadoRepositorio;

        public ObterReservaSobConsultaExecutor(IReservaNrRepositorio reservaRepositorio, ILockSobConsultaRepositorio lockSobConsultaRepositorio, IOperacoesServiceRepositorio operacoesServiceRepositorio, IInformacoesUsuarioLogadoRepositorio informacoesUsuarioLogadoRepositorio)
        {
            this.reservaRepositorio = reservaRepositorio;
            this.lockSobConsultaRepositorio = lockSobConsultaRepositorio;
            this.operacoesServiceRepositorio = operacoesServiceRepositorio;
            this.informacoesUsuarioLogadoRepositorio = informacoesUsuarioLogadoRepositorio;
        }

        public ObterReservaSobConsultaResultado Executar(ObterReservaSobConsultaRequisicao requisicao)
        {

            if (String.IsNullOrEmpty(requisicao.Localizador))
            {
                throw new ParametroNuloException("Localizador");
            }

            Reserva reserva = reservaRepositorio.ObterReserva(requisicao.Localizador);
            AgenciaEntidade agenciaEntidade = operacoesServiceRepositorio.ObterCodigoSupervisorRegionalAgencia(reserva.Agencia);
            if (agenciaEntidade != null)
            {
                reserva.SupervisorAgenciaRetirada = informacoesUsuarioLogadoRepositorio.ObterUsuarioLogado(agenciaEntidade.MatriculaSupervisor);
                reserva.RegionalAgenciaRetirada = informacoesUsuarioLogadoRepositorio.ObterUsuarioLogado(agenciaEntidade.MatriculaGerente);
            }
            if (reserva == null)
            {
                throw new ParametroInvalidoException("Reserva não encontrada.", "Localizador", requisicao.Localizador, CodigosErro.RESERVA_NAO_ENCONTRADA);
            }

            //TODO: Mapear o campo de reserva bloqueada.
            //var reservasBloqueadas = lockSobConsultaRepositorio.VerificarSeReservasEstaoBloqueadas(reservas.Select(x=> x.Localizador).ToArray());

            return new ObterReservaSobConsultaResultado
            {
                ReservaSobConsulta = ReservaDto.CriarAPartirDeEntidade(reserva),
                Estado = EstadoResultado.OK
            };
        }
    }
}
