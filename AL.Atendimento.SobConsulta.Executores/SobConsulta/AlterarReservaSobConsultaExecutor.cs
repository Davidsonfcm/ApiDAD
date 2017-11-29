using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using AL.Atendimento.SobConsulta.Repositorios.Reservas;
using AL.Atendimento.SobConsulta.Util.Enumeracoes;
using AL.Atendimento.SobConsulta.Util.Excecoes;
using Localiza.SDK.Fronteira;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Executores.SobConsulta
{
    public class AlterarReservaSobConsultaExecutor : IExecutor<AlterarReservaSobConsultaRequisicao, AlterarReservaSobConsultaResultado>
    {
        private readonly IReservaWsRepositorio reservaWsRepositorio;
        private readonly IReservaNrRepositorio reservaNrRepositorio;

        public AlterarReservaSobConsultaExecutor(IReservaNrRepositorio reservaNrRepositorio, IReservaWsRepositorio reservaWsRepositorio)
        {
            this.reservaNrRepositorio = reservaNrRepositorio;
            this.reservaWsRepositorio = reservaWsRepositorio;
        }

        public AlterarReservaSobConsultaResultado Executar(AlterarReservaSobConsultaRequisicao requisicao)
        {
            if (string.IsNullOrEmpty(requisicao.Localizador))
                throw new ParametroNuloException("Localizador");

            string localizador = requisicao.Localizador;

            Reserva reservaNR = reservaNrRepositorio.ObterReserva(localizador);

            DadosComunicacao reservaWS = reservaWsRepositorio.obterDadosReserva(localizador, requisicao.CodigoUsuario);

            if (reservaWS == null)
            {
                throw new ParametroInvalidoException("Reserva não encontrada.", "localizador", localizador, CodigosErro.RESERVA_NAO_ENCONTRADA);
            }

            requisicao.Observacao = (reservaWS.ObservacaoReserva == null ? "" : reservaWS.ObservacaoReserva + "\n")  + (reservaNR.Observacao == null ? "" : reservaNR.Observacao + "\n")  + requisicao.Observacao;

            EfetuaAlteracoesDaReservaSobConsulta(requisicao, reservaWS);

            if (reservaWS.IdOferta != null)
            {
                if (!string.IsNullOrEmpty(reservaWS.CodigoEvento) && reservaNR != null && reservaNR.PossuiTarifaPromocional)
                {
                    reservaNR = null;
                }
            }
            else
            {
                reservaNR = null;
            }

            if (reservaNR == null && !reservaWS.IndicadorPrePagamento)
            {
                reservaWsRepositorio.AlterarStatusReservaWs(reservaWS, requisicao.CodigoUsuario);
            }
            else
            {
                reservaNrRepositorio.AlterarStatusReservaNr(reservaWS);
            }

            return new AlterarReservaSobConsultaResultado()
            {
                Estado = EstadoResultado.OK,
                DadosReserva = reservaWsRepositorio.obterDadosReserva(requisicao.Localizador, requisicao.CodigoUsuario),
                DadosReservaNr = reservaNrRepositorio.ObterReserva(requisicao.Localizador)
            };
        }

        private void EfetuaAlteracoesDaReservaSobConsulta(AlterarReservaSobConsultaRequisicao requisicao, DadosComunicacao reservaWS)
        {
            if (reservaWS.SituacaoReserva != SituacaoReserva.SobConsulta)
            {
                throw new NegocioException("Reserva não pode ser alterada", CodigosErro.RESERVA_NAO_PODE_SER_ALTERADA);
            }

            if (requisicao.SituacaoReserva != null)
            {
                reservaWS.SituacaoReserva = requisicao.SituacaoReserva;
            }
            reservaWS.ObservacaoReserva = requisicao.Observacao;

            if (requisicao.SituacaoReserva == SituacaoReserva.Cancelada)
            {
                reservaWS.CodigoMotivoCancelamento = Convert.ToInt16(requisicao.MotivoCancelamento);
            }

            if (requisicao.SituacaoReserva == SituacaoReserva.NaoConfirmada)
            {
                reservaWS.MotivoNaoConfirmacao = Convert.ToInt16(requisicao.MotivoNaoConfirmada);
            }

            if (!String.IsNullOrEmpty(requisicao.LocalizadorExterno))
            {
                reservaWS.NumeroSinistro = requisicao.LocalizadorExterno;
            }

            if (requisicao.EnviarEmail.HasValue)
                reservaWS.EnviarEmail = requisicao.EnviarEmail.Value;

            if (requisicao.EnviarSMS.HasValue)
                reservaWS.EnviarSMS = requisicao.EnviarSMS.Value;
        }
    }
}
