using System;
using AL.Atendimento.SobConsulta.Repositorios.Reservas;
using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Util.Enumeracoes;

namespace AL.Atendimento.SobConsulta.MapeamentosMock
{
    public class ReservaWsRepositorioMock : IReservaWsRepositorio
    {
        #region Constantes

        #endregion

        #region Construtores
        
        #endregion

        #region Metodos Públicos
        public DadosComunicacao obterDadosReserva(string localizador, string codigoUsuario)
        {
            DadosComunicacao dadosComunicacaoMock = new DadosComunicacao();
         if (localizador == "AVC95GVH3P")
            {
                dadosComunicacaoMock = new DadosComunicacao()
                {
                    CodigoCliente = "",
                    CodigoEvento = "",
                    CodigoMotivoCancelamento = 0,
                    CodigoUsuario = "",
                    DataRetirada = new DateTime(2017, 04, 17, 15, 00, 00),
                    DataRetorno = new DateTime(2017, 04, 30, 14, 00, 00),
                    Localizador = "MOBO155C9KLA",
                    SituacaoReserva = "1"
                };
            }
        else
            { 
                dadosComunicacaoMock = new DadosComunicacao()
                {
                    CodigoCliente = "",
                    CodigoEvento = "",
                    CodigoMotivoCancelamento = 0,
                    CodigoUsuario = "",
                    DataRetirada = new DateTime(2017,04,17,15,00,00),
                    DataRetorno = new DateTime(2017,04,30,14,00,00),
                    Localizador = "MOBO155C9KLA",
                    SituacaoReserva = "9"
                };
        }
            return dadosComunicacaoMock;

        }

        public void EnviarEmailConsultorAGVIGSeNecessario(DadosComunicacao dadosComunicacao, string codigoUsuario)
        {

            using (Repositorios.wsReserva.Reservas servicoReserva = new Repositorios.wsReserva.Reservas())
            {
                servicoReserva.EnviarEmailConsultorAGVIGSeNecessario(dadosComunicacao.DestinatarioEmail, dadosComunicacao.NomeConsultor, dadosComunicacao.CodigoAGVIG, dadosComunicacao.NomeAgenciaViagemSeguradora, dadosComunicacao.NomeCliente,
                    dadosComunicacao.TipoProtecao, dadosComunicacao.Localizador, dadosComunicacao.NomeCliente, dadosComunicacao.NomeCidadeRetirada, dadosComunicacao.NomeCidadeRetorno, dadosComunicacao.NomeAgenciaRetirada, dadosComunicacao.NomeAgenciaRetorno,
                    dadosComunicacao.DataRetirada, dadosComunicacao.DataRetorno);
            }
        }

        public void EnviarEmailConfirmacaoSolicitante(string localizador, string codigoUsuario)
        {
            using (Repositorios.wsReserva.Reservas servicoReserva = new Repositorios.wsReserva.Reservas())
            {
                servicoReserva.EnviarEmailConfirmacaoReservaAsync(localizador, false);
            }
        }
        #endregion

        #region Metodos Privados
        private DadosComunicacao ConverterDtoEntidadeComunicacao(Repositorios.wsReserva.Reserva reservaWsReserva)
        {
            if (reservaWsReserva == null)
            {
                return null;
            }
            var dadosComunicacao = new DadosComunicacao
            {
                CodigoAGVIG = reservaWsReserva.CodigoAgenciaViagemSeguradora,
                DataRetirada = reservaWsReserva.DataRetirada,
                DataRetorno = reservaWsReserva.DataRetorno,
                DestinatarioEmail = reservaWsReserva.EmailRequisitante,
                NomeAgenciaRetirada = reservaWsReserva.AgenciaRetirada,
                NomeAgenciaRetorno = reservaWsReserva.AgenciaRetorno,
                NomeAgenciaViagemSeguradora = reservaWsReserva.NomeAgenciaViagemSeguradora,
                NomeCidadeRetirada = reservaWsReserva.CidadeRetirada,
                NomeCidadeRetorno = reservaWsReserva.CidadeRetorno,
                NomeCliente = reservaWsReserva.NomeCliente,
                NomeConsultor = reservaWsReserva.NomeEmissor,
                TipoOrigem = reservaWsReserva.TipoOrigem,
                Localizador = reservaWsReserva.NumeroReserva,
                SituacaoReserva = reservaWsReserva.SituacaoReserva,
                CodigoCliente = reservaWsReserva.CodigoCliente,
                CodigoUsuario = reservaWsReserva.CodigoUsuario,
                NomeUsuario = reservaWsReserva.NomeUsuario,
                TipoCliente = reservaWsReserva.TipoCliente,
                EmailRequisitante = reservaWsReserva.EmailRequisitante,
                EmailSolicitante = reservaWsReserva.EmailSolicitante,
                EnviarEmail = reservaWsReserva.EnviarEmail,
                EnviarEmailSolicitante = reservaWsReserva.EnviarEmailSolicitante,
                FaturaAgenciaViagem = reservaWsReserva.FaturarAgenciaViagem,
                CodigoEvento = reservaWsReserva.CodigoEvento,
                TipoClienteAgenciaViagemSeguradora = reservaWsReserva.TipoClienteAgenciaViagemSeguradora,
                IdOferta = reservaWsReserva.IdOferta
                //TipoProtecao = reservaWsReserva.
            };

            return dadosComunicacao;
        }

        public void AlterarStatusReservaWs(DadosComunicacao reservaWS, string codigoUsuario)
        {
            reservaWS.SituacaoReserva = SituacaoReserva.Aberta;
        }

        public void AlterarStatusReservaWs(DadosComunicacao reservaWS, Reserva reservaNR)
        {
            reservaWS.SituacaoReserva = SituacaoReserva.Aberta;
        }

        public void GravarReservaAssincrona(DadosComunicacao reservaWS, Reserva reservaNR)
        {
            throw new NotImplementedException();
        }

        public string BuscarSituacaoReserva(string numeroReserva, string codigoUsuario)
        {
            throw new NotImplementedException();
        }
        
        #endregion
    }
}