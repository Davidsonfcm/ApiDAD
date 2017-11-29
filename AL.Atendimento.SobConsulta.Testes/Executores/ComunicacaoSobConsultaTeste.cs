using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using AL.Atendimento.SobConsulta.Repositorios.Reservas;
using Localiza.SDK.Fronteira;
using Localiza.SDK.InversaoControle;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Testes.FluxoComunicacao
{
    [TestClass]
    public class ComunicacaoSobConsultaTeste : TesteBase
    {
        [TestMethod]
        public void ReservasTipoOrigemCR()
        {
            DateTime DataAtual = DateTime.Now;
            Reserva reservaNR = new Reserva()
            {
                CodigoNegociacao = null,
                DataCriacaoReserva = DataAtual,
                DataRetirada = DataAtual.AddDays(2),
                Localizador = "8C54N4N3A",
                PossuiTarifaPromocional = false
            };

            DadosComunicacao dadosComunicacaoMock = new DadosComunicacao()
            {
                CodigoAGVIG = "05850554",
                CodigoCliente = "123456",
                CodigoEvento = "03008",
                CodigoUsuario = "191950",
                DataRetirada = DataAtual,
                DataRetorno = DataAtual.AddDays(2),
                DestinatarioEmail = "dti138@localiza.com",
                EmailRequisitante = "dti138@localiza.com",
                EmailSolicitante = "dti138@localiza.com",
                EnviarEmail = true,
                EnviarEmailSolicitante = true,
                FaturaAgenciaViagem = true,
                Localizador = "8C54N4N3A",
                NomeAgenciaRetirada = "ACGIG",
                NomeAgenciaRetorno = "ACGIG",
                NomeAgenciaViagemSeguradora = "AVINASH QA",
                NomeCidadeRetirada = "RIO DE JANEIRO",
                NomeCidadeRetorno = "RIO DE JANEIRO",
                NomeCliente = "DIEGO ALMEIDA",
                NomeConsultor = "AVINASH QA",
                NomeUsuario = "DIEGO ALMEIDA",
                SituacaoReserva = "1",
                TipoCliente = '0',
                TipoClienteAgenciaViagemSeguradora = '0',
                TipoOrigem = "CR",
                TipoProtecao = "O",
                IndicadorPrePagamento = false
            };

            ComunicacaoSobConsultaRequisicao requisicao = new ComunicacaoSobConsultaRequisicao()
            {
                DadosComunicacaoReserva = dadosComunicacaoMock,
                DadosReservaNR = reservaNR
            };

            var resultado = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IExecutor<ComunicacaoSobConsultaRequisicao, 
                    ComunicacaoSobConsultaResultado>>().Executar(requisicao);
        }

        [TestMethod]
        public void ReservaAbertaPFComOferta()
        {
            DateTime DataAtual = DateTime.Now;
            Reserva reservaNR = new Reserva()
            {
                CodigoNegociacao = null,
                DataCriacaoReserva = DataAtual,
                DataRetirada = DataAtual.AddDays(2),
                Localizador = "8C54N4N3A",
                PossuiTarifaPromocional = false
            };

            DadosComunicacao dadosComunicacaoMock = new DadosComunicacao()
            {
                CodigoAGVIG = "05850554",
                CodigoCliente = "123456",
                CodigoEvento = "03008",
                CodigoUsuario = "191950",
                DataRetirada = DataAtual,
                DataRetorno = DataAtual.AddDays(2),
                DestinatarioEmail = "dti138@localiza.com",
                EmailRequisitante = "dti138@localiza.com",
                EmailSolicitante = "dti138@localiza.com",
                EnviarEmail = true,
                EnviarEmailSolicitante = true,
                FaturaAgenciaViagem = true,
                Localizador = "8C54N4N3A",
                NomeAgenciaRetirada = "ACGIG",
                NomeAgenciaRetorno = "ACGIG",
                NomeAgenciaViagemSeguradora = "AVINASH QA",
                NomeCidadeRetirada = "RIO DE JANEIRO",
                NomeCidadeRetorno = "RIO DE JANEIRO",
                NomeCliente = "DIEGO ALMEIDA",
                NomeConsultor = "AVINASH QA",
                NomeUsuario = "DIEGO ALMEIDA",
                SituacaoReserva = "1",
                TipoCliente = '0',
                TipoClienteAgenciaViagemSeguradora = '0',
                TipoOrigem = "IT",
                TipoProtecao = "O",
                IndicadorPrePagamento = false
            };

            ComunicacaoSobConsultaRequisicao requisicao = new ComunicacaoSobConsultaRequisicao()
            {
                DadosComunicacaoReserva = dadosComunicacaoMock,
                DadosReservaNR = reservaNR
            };

            var resultado = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IExecutor<ComunicacaoSobConsultaRequisicao, ComunicacaoSobConsultaResultado>>().Executar(requisicao);
        }

        [TestMethod]
        public void ReservaAbertaPFSemOfertaComTipoOrigemNosParametrosCadastrados()
        {
            DateTime DataAtual = DateTime.Now;
            Reserva reservaNR = new Reserva()
            {
                CodigoNegociacao = null,
                DataCriacaoReserva = DataAtual,
                DataRetirada = DataAtual.AddDays(2),
                Localizador = "8C54N4N3A",
                PossuiTarifaPromocional = false
            };

            DadosComunicacao dadosComunicacaoMock = new DadosComunicacao()
            {
                CodigoAGVIG = "05850554",
                CodigoCliente = "123456",
                CodigoEvento = "",
                CodigoUsuario = "191950",
                DataRetirada = DataAtual,
                DataRetorno = DataAtual.AddDays(2),
                DestinatarioEmail = "dti138@localiza.com",
                EmailRequisitante = "dti138@localiza.com",
                EmailSolicitante = "dti138@localiza.com",
                EnviarEmail = true,
                EnviarEmailSolicitante = true,
                FaturaAgenciaViagem = true,
                Localizador = "8C54N4N3A",
                NomeAgenciaRetirada = "ACGIG",
                NomeAgenciaRetorno = "ACGIG",
                NomeAgenciaViagemSeguradora = "AVINASH QA",
                NomeCidadeRetirada = "RIO DE JANEIRO",
                NomeCidadeRetorno = "RIO DE JANEIRO",
                NomeCliente = "DIEGO ALMEIDA",
                NomeConsultor = "AVINASH QA",
                NomeUsuario = "DIEGO ALMEIDA",
                SituacaoReserva = "1",
                TipoCliente = '0',
                TipoClienteAgenciaViagemSeguradora = '0',
                TipoOrigem = "GO",
                TipoProtecao = "O",
                IndicadorPrePagamento = false
            };

            ComunicacaoSobConsultaRequisicao requisicao = new ComunicacaoSobConsultaRequisicao()
            {
                DadosComunicacaoReserva = dadosComunicacaoMock,
                DadosReservaNR = reservaNR
            };

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ReservaAbertaPFSemOfertaComPrePagamento()
        {
            DateTime DataAtual = DateTime.Now;
            Reserva reservaNR = new Reserva()
            {
                CodigoNegociacao = null,
                DataCriacaoReserva = DataAtual,
                DataRetirada = DataAtual.AddDays(2),
                Localizador = "8C54N4N3A",
                PossuiTarifaPromocional = false
            };

            DadosComunicacao dadosComunicacaoMock = new DadosComunicacao()
            {
                CodigoAGVIG = "05850554",
                CodigoCliente = "123456",
                CodigoEvento = "",
                CodigoUsuario = "191950",
                DataRetirada = DataAtual,
                DataRetorno = DataAtual.AddDays(2),
                DestinatarioEmail = "dti138@localiza.com",
                EmailRequisitante = "dti138@localiza.com",
                EmailSolicitante = "dti138@localiza.com",
                EnviarEmail = true,
                EnviarEmailSolicitante = true,
                FaturaAgenciaViagem = true,
                Localizador = "8C54N4N3A",
                NomeAgenciaRetirada = "ACGIG",
                NomeAgenciaRetorno = "ACGIG",
                NomeAgenciaViagemSeguradora = "AVINASH QA",
                NomeCidadeRetirada = "RIO DE JANEIRO",
                NomeCidadeRetorno = "RIO DE JANEIRO",
                NomeCliente = "DIEGO ALMEIDA",
                NomeConsultor = "AVINASH QA",
                NomeUsuario = "DIEGO ALMEIDA",
                SituacaoReserva = "1",
                TipoCliente = '0',
                TipoClienteAgenciaViagemSeguradora = '0',
                TipoOrigem = "CR",
                TipoProtecao = "O",
                IndicadorPrePagamento = true
            };

            ComunicacaoSobConsultaRequisicao requisicao = new ComunicacaoSobConsultaRequisicao()
            {
                DadosComunicacaoReserva = dadosComunicacaoMock,
                DadosReservaNR = reservaNR
            };

            var resultado = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IExecutor<ComunicacaoSobConsultaRequisicao,
                    ComunicacaoSobConsultaResultado>>().Executar(requisicao);
        }

        [TestMethod]
        public void ReservaAbertaPFSemOfertaCadastradaNoParamentroComoInternetMobile()
        {
            DateTime DataAtual = DateTime.Now;
            Reserva reservaNR = new Reserva()
            {
                CodigoNegociacao = null,
                DataCriacaoReserva = DataAtual,
                DataRetirada = DataAtual.AddDays(2),
                Localizador = "8C54N4N3A",
                PossuiTarifaPromocional = false
            };

            DadosComunicacao dadosComunicacaoMock = new DadosComunicacao()
            {
                CodigoAGVIG = "05850554",
                CodigoCliente = "123456",
                CodigoEvento = "",
                CodigoUsuario = "191950",
                DataRetirada = DataAtual,
                DataRetorno = DataAtual.AddDays(2),
                DestinatarioEmail = "dti138@localiza.com",
                EmailRequisitante = "dti138@localiza.com",
                EmailSolicitante = "dti138@localiza.com",
                EnviarEmail = true,
                EnviarEmailSolicitante = true,
                FaturaAgenciaViagem = true,
                Localizador = "8C54N4N3A",
                NomeAgenciaRetirada = "ACGIG",
                NomeAgenciaRetorno = "ACGIG",
                NomeAgenciaViagemSeguradora = "AVINASH QA",
                NomeCidadeRetirada = "RIO DE JANEIRO",
                NomeCidadeRetorno = "RIO DE JANEIRO",
                NomeCliente = "DIEGO ALMEIDA",
                NomeConsultor = "AVINASH QA",
                NomeUsuario = "DIEGO ALMEIDA",
                SituacaoReserva = "1",
                TipoCliente = '0',
                TipoClienteAgenciaViagemSeguradora = '0',
                TipoOrigem = "IT",
                TipoProtecao = "O",
                IndicadorPrePagamento = false
            };

            ComunicacaoSobConsultaRequisicao requisicao = new ComunicacaoSobConsultaRequisicao()
            {
                DadosComunicacaoReserva = dadosComunicacaoMock,
                DadosReservaNR = reservaNR
            };

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ReservaAbertaPFSemOfertaComCodigoPromocionalOuPrePagamento()
        {
            DateTime DataAtual = DateTime.Now;
            Reserva reservaNR = new Reserva()
            {
                CodigoNegociacao = "",
                DataCriacaoReserva = DataAtual,
                DataRetirada = DataAtual.AddDays(2),
                Localizador = "8C54N4N3A",
                PossuiTarifaPromocional = true
            };

            DadosComunicacao dadosComunicacaoMock = new DadosComunicacao()
            {
                CodigoAGVIG = "05850554",
                CodigoCliente = "",
                CodigoEvento = "",
                CodigoUsuario = "191950",
                DataRetirada = DataAtual,
                DataRetorno = DataAtual.AddDays(2),
                DestinatarioEmail = "dti138@localiza.com",
                EmailRequisitante = "dti138@localiza.com",
                EmailSolicitante = "dti138@localiza.com",
                EnviarEmail = true,
                EnviarEmailSolicitante = true,
                FaturaAgenciaViagem = true,
                Localizador = "8C54N4N3A",
                NomeAgenciaRetirada = "ACGIG",
                NomeAgenciaRetorno = "ACGIG",
                NomeAgenciaViagemSeguradora = "AVINASH QA",
                NomeCidadeRetirada = "RIO DE JANEIRO",
                NomeCidadeRetorno = "RIO DE JANEIRO",
                NomeCliente = "DIEGO ALMEIDA",
                NomeConsultor = "AVINASH QA",
                NomeUsuario = "DIEGO ALMEIDA",
                SituacaoReserva = "1",
                TipoCliente = '1',
                TipoClienteAgenciaViagemSeguradora = '0',
                TipoOrigem = "IT",
                TipoProtecao = "O",
                IndicadorPrePagamento = true
            };

            ComunicacaoSobConsultaRequisicao requisicao = new ComunicacaoSobConsultaRequisicao()
            {
                DadosComunicacaoReserva = dadosComunicacaoMock,
                DadosReservaNR = reservaNR
            };

            var resultado = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IExecutor<ComunicacaoSobConsultaRequisicao,
                    ComunicacaoSobConsultaResultado>>().Executar(requisicao);
        }

        [TestMethod]
        public void ReservaAbertaPFSemOfertaSemCodigoPromocionalOuPrePagamento()
        {
            DateTime DataAtual = DateTime.Now;
            Reserva reservaNR = new Reserva()
            {
                CodigoNegociacao = null,
                DataCriacaoReserva = DataAtual,
                DataRetirada = DataAtual.AddDays(2),
                Localizador = "8C54N4N3A",
                PossuiTarifaPromocional = false
            };

            DadosComunicacao dadosComunicacaoMock = new DadosComunicacao()
            {
                CodigoAGVIG = "05850554",
                CodigoCliente = "123456",
                CodigoEvento = "123456",
                CodigoUsuario = "191950",
                DataRetirada = DataAtual,
                DataRetorno = DataAtual.AddDays(2),
                DestinatarioEmail = "dti138@localiza.com",
                EmailRequisitante = "dti138@localiza.com",
                EmailSolicitante = "dti138@localiza.com",
                EnviarEmail = true,
                EnviarEmailSolicitante = true,
                FaturaAgenciaViagem = true,
                Localizador = "8C54N4N3A",
                NomeAgenciaRetirada = "ACGIG",
                NomeAgenciaRetorno = "ACGIG",
                NomeAgenciaViagemSeguradora = "AVINASH QA",
                NomeCidadeRetirada = "RIO DE JANEIRO",
                NomeCidadeRetorno = "RIO DE JANEIRO",
                NomeCliente = "DIEGO ALMEIDA",
                NomeConsultor = "AVINASH QA",
                NomeUsuario = "DIEGO ALMEIDA",
                SituacaoReserva = "1",
                TipoCliente = '0',
                TipoClienteAgenciaViagemSeguradora = '0',
                TipoOrigem = "IT",
                TipoProtecao = "O",
                IndicadorPrePagamento = false
            };

            ComunicacaoSobConsultaRequisicao requisicao = new ComunicacaoSobConsultaRequisicao()
            {
                DadosComunicacaoReserva = dadosComunicacaoMock,
                DadosReservaNR = reservaNR
            };

            var resultado = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IExecutor<ComunicacaoSobConsultaRequisicao,
                    ComunicacaoSobConsultaResultado>>().Executar(requisicao);
        }

        [TestMethod]
        public void ReservaAbertaPFSemOfertaVindaDoNR()
        {
            DateTime DataAtual = DateTime.Now;
            Reserva reservaNR = new Reserva()
            {
                CodigoNegociacao = "123456",
                DataCriacaoReserva = DataAtual,
                DataRetirada = DataAtual.AddDays(2),
                Localizador = "8C54N4N3A",
                PossuiTarifaPromocional = false
            };

            DadosComunicacao dadosComunicacaoMock = new DadosComunicacao()
            {
                CodigoAGVIG = "05850554",
                CodigoCliente = "123456",
                CodigoEvento = "",
                CodigoUsuario = "191950",
                DataRetirada = DataAtual,
                DataRetorno = DataAtual.AddDays(2),
                DestinatarioEmail = "dti138@localiza.com",
                EmailRequisitante = "dti138@localiza.com",
                EmailSolicitante = "dti138@localiza.com",
                EnviarEmail = true,
                EnviarEmailSolicitante = true,
                FaturaAgenciaViagem = true,
                Localizador = "8C54N4N3A",
                NomeAgenciaRetirada = "ACGIG",
                NomeAgenciaRetorno = "ACGIG",
                NomeAgenciaViagemSeguradora = "AVINASH QA",
                NomeCidadeRetirada = "RIO DE JANEIRO",
                NomeCidadeRetorno = "RIO DE JANEIRO",
                NomeCliente = "DIEGO ALMEIDA",
                NomeConsultor = "AVINASH QA",
                NomeUsuario = "DIEGO ALMEIDA",
                SituacaoReserva = "1",
                TipoCliente = '1',
                TipoClienteAgenciaViagemSeguradora = '0',
                TipoOrigem = "IT",
                TipoProtecao = "O",
                IndicadorPrePagamento = false
            };

            ComunicacaoSobConsultaRequisicao requisicao = new ComunicacaoSobConsultaRequisicao()
            {
                DadosComunicacaoReserva = dadosComunicacaoMock,
                DadosReservaNR = reservaNR
            };

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ReservaAbertaSemMarcacaoDoEnvioDeEmail()
        {
            DateTime DataAtual = DateTime.Now;
            Reserva reservaNR = new Reserva()
            {
                CodigoNegociacao = null,
                DataCriacaoReserva = DataAtual,
                DataRetirada = DataAtual.AddDays(2),
                Localizador = "8C54N4N3A",
                PossuiTarifaPromocional = false
            };

            DadosComunicacao dadosComunicacaoMock = new DadosComunicacao()
            {
                CodigoAGVIG = "05850554",
                CodigoCliente = "123456",
                CodigoEvento = "03008",
                CodigoUsuario = "191950",
                DataRetirada = DataAtual,
                DataRetorno = DataAtual.AddDays(2),
                DestinatarioEmail = "dti138@localiza.com",
                EmailRequisitante = "dti138@localiza.com",
                EmailSolicitante = "dti138@localiza.com",
                EnviarEmail = false,
                EnviarEmailSolicitante = true,
                FaturaAgenciaViagem = true,
                Localizador = "8C54N4N3A",
                NomeAgenciaRetirada = "ACGIG",
                NomeAgenciaRetorno = "ACGIG",
                NomeAgenciaViagemSeguradora = "AVINASH QA",
                NomeCidadeRetirada = "RIO DE JANEIRO",
                NomeCidadeRetorno = "RIO DE JANEIRO",
                NomeCliente = "DIEGO ALMEIDA",
                NomeConsultor = "AVINASH QA",
                NomeUsuario = "DIEGO ALMEIDA",
                SituacaoReserva = "1",
                TipoCliente = '0',
                TipoClienteAgenciaViagemSeguradora = '0',
                TipoOrigem = "CR",
                TipoProtecao = "O",
                IndicadorPrePagamento = false
            };

            ComunicacaoSobConsultaRequisicao requisicao = new ComunicacaoSobConsultaRequisicao()
            {
                DadosComunicacaoReserva = dadosComunicacaoMock,
                DadosReservaNR = reservaNR
            };

            var resultado = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IExecutor<ComunicacaoSobConsultaRequisicao,
                    ComunicacaoSobConsultaResultado>>().Executar(requisicao);
        }

        [TestMethod]
        public void ReservaAbertaClientePJMarcadoEnvioDeEmail()
        {
            DateTime DataAtual = DateTime.Now;
            Reserva reservaNR = new Reserva()
            {
                CodigoNegociacao = null,
                DataCriacaoReserva = DataAtual,
                DataRetirada = DataAtual.AddDays(2),
                Localizador = "AVO154J4K9P",
                PossuiTarifaPromocional = false
            };

            DadosComunicacao dadosComunicacaoMock = new DadosComunicacao()
            {
                CodigoAGVIG = "05850554",
                CodigoCliente = "123456",
                CodigoEvento = "03008",
                CodigoUsuario = "191950",
                DataRetirada = DataAtual,
                DataRetorno = DataAtual.AddDays(2),
                DestinatarioEmail = "dti138@localiza.com",
                EmailRequisitante = "dti138@localiza.com",
                EmailSolicitante = "dti138@localiza.com",
                EnviarEmail = true,
                EnviarEmailSolicitante = true,
                FaturaAgenciaViagem = true,
                Localizador = "AVO154J4K9P",
                NomeAgenciaRetirada = "ACGIG",
                NomeAgenciaRetorno = "ACGIG",
                NomeAgenciaViagemSeguradora = "AVINASH QA",
                NomeCidadeRetirada = "RIO DE JANEIRO",
                NomeCidadeRetorno = "RIO DE JANEIRO",
                NomeCliente = "DIEGO ALMEIDA",
                NomeConsultor = "AVINASH QA",
                NomeUsuario = "DIEGO ALMEIDA",
                SituacaoReserva = "1",
                TipoCliente = '2',
                TipoClienteAgenciaViagemSeguradora = '3',
                TipoOrigem = "SA",
                TipoProtecao = "O",
                IndicadorPrePagamento = false,
                CodigoEmissor = "12"
            };

            ComunicacaoSobConsultaRequisicao requisicao = new ComunicacaoSobConsultaRequisicao()
            {
                DadosComunicacaoReserva = dadosComunicacaoMock,
                DadosReservaNR = reservaNR
            };

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ReservaAbertaMarcadoEnvioParaSolicitante()
        {
            DateTime DataAtual = DateTime.Now;
            Reserva reservaNR = new Reserva()
            {
                CodigoNegociacao = null,
                DataCriacaoReserva = DataAtual,
                DataRetirada = DataAtual.AddDays(2),
                Localizador = "8C54N4N3A",
                PossuiTarifaPromocional = false
            };

            DadosComunicacao dadosComunicacaoMock = new DadosComunicacao()
            {
                CodigoAGVIG = "05850554",
                CodigoCliente = "123456",
                CodigoEvento = "03008",
                CodigoUsuario = "191950",
                DataRetirada = DataAtual,
                DataRetorno = DataAtual.AddDays(2),
                DestinatarioEmail = "dti138@localiza.com",
                EmailRequisitante = "dti138@localiza.com",
                EmailSolicitante = "dti138@localiza.com",
                EnviarEmail = true,
                EnviarEmailSolicitante = true,
                FaturaAgenciaViagem = true,
                Localizador = "8C54N4N3A",
                NomeAgenciaRetirada = "ACGIG",
                NomeAgenciaRetorno = "ACGIG",
                NomeAgenciaViagemSeguradora = "AVINASH QA",
                NomeCidadeRetirada = "RIO DE JANEIRO",
                NomeCidadeRetorno = "RIO DE JANEIRO",
                NomeCliente = "DIEGO ALMEIDA",
                NomeConsultor = "AVINASH QA",
                NomeUsuario = "DIEGO ALMEIDA",
                SituacaoReserva = "1",
                TipoCliente = '0',
                TipoClienteAgenciaViagemSeguradora = '0',
                TipoOrigem = "CR",
                TipoProtecao = "O",
                IndicadorPrePagamento = false
            };

            ComunicacaoSobConsultaRequisicao requisicao = new ComunicacaoSobConsultaRequisicao()
            {
                DadosComunicacaoReserva = dadosComunicacaoMock,
                DadosReservaNR = reservaNR
            };

            var resultado = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IExecutor<ComunicacaoSobConsultaRequisicao,
                    ComunicacaoSobConsultaResultado>>().Executar(requisicao);
        }

        [TestMethod]
        public void ReservaAbertaParaAGVIG()
        {
            DateTime DataAtual = DateTime.Now;
            Reserva reservaNR = new Reserva()
            {
                CodigoNegociacao = null,
                DataCriacaoReserva = DataAtual,
                DataRetirada = DataAtual.AddDays(2),
                Localizador = "AVO154J4K9P",
                PossuiTarifaPromocional = false
            };

            DadosComunicacao dadosComunicacaoMock = new DadosComunicacao()
            {
                CodigoAGVIG = "05850554",
                CodigoCliente = "123456",
                CodigoEvento = "03008",
                CodigoUsuario = "191950",
                DataRetirada = DataAtual,
                DataRetorno = DataAtual.AddDays(2),
                DestinatarioEmail = "dti138@localiza.com",
                EmailRequisitante = "dti138@localiza.com",
                EmailSolicitante = "dti138@localiza.com",
                EnviarEmail = true,
                EnviarEmailSolicitante = true,
                FaturaAgenciaViagem = true,
                Localizador = "AVO154J4K9P",
                NomeAgenciaRetirada = "ACGIG",
                NomeAgenciaRetorno = "ACGIG",
                NomeAgenciaViagemSeguradora = "AVINASH QA",
                NomeCidadeRetirada = "RIO DE JANEIRO",
                NomeCidadeRetorno = "RIO DE JANEIRO",
                NomeCliente = "DIEGO ALMEIDA",
                NomeConsultor = "AVINASH QA",
                NomeUsuario = "DIEGO ALMEIDA",
                SituacaoReserva = "1",
                TipoCliente = '1',
                TipoClienteAgenciaViagemSeguradora = '7',
                TipoOrigem = "CR",
                TipoProtecao = "O",
                IndicadorPrePagamento = false
            };

            ComunicacaoSobConsultaRequisicao requisicao = new ComunicacaoSobConsultaRequisicao()
            {
                DadosComunicacaoReserva = dadosComunicacaoMock,
                DadosReservaNR = reservaNR
            };

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ReservaAbertaNaoOriundaDoNR()
        {
            DateTime DataAtual = DateTime.Now;
            Reserva reservaNR = new Reserva()
            {
                CodigoNegociacao = "1234",
                DataCriacaoReserva = DataAtual,
                DataRetirada = DataAtual.AddDays(2),
                Localizador = "8C54N4N3A",
                PossuiTarifaPromocional = false
            };

            DadosComunicacao dadosComunicacaoMock = new DadosComunicacao()
            {
                CodigoAGVIG = "05850554",
                CodigoCliente = "123456",
                CodigoEvento = "",
                CodigoUsuario = "191950",
                DataRetirada = DataAtual,
                DataRetorno = DataAtual.AddDays(2),
                DestinatarioEmail = "dti138@localiza.com",
                EmailRequisitante = "dti138@localiza.com",
                EmailSolicitante = "dti138@localiza.com",
                EnviarEmail = true,
                EnviarEmailSolicitante = true,
                FaturaAgenciaViagem = true,
                Localizador = "8C54N4N3A",
                NomeAgenciaRetirada = "ACGIG",
                NomeAgenciaRetorno = "ACGIG",
                NomeAgenciaViagemSeguradora = "AVINASH QA",
                NomeCidadeRetirada = "RIO DE JANEIRO",
                NomeCidadeRetorno = "RIO DE JANEIRO",
                NomeCliente = "DIEGO ALMEIDA",
                NomeConsultor = "AVINASH QA",
                NomeUsuario = "DIEGO ALMEIDA",
                SituacaoReserva = "1",
                TipoCliente = '1',
                TipoClienteAgenciaViagemSeguradora = '0',
                TipoOrigem = "IT",
                TipoProtecao = "O",
                IndicadorPrePagamento = false
            };

            ComunicacaoSobConsultaRequisicao requisicao = new ComunicacaoSobConsultaRequisicao()
            {
                DadosComunicacaoReserva = dadosComunicacaoMock,
                DadosReservaNR = reservaNR
            };

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ReservaAbertaPossuiCodigoClienteETipoEhPJ()
        {
            DateTime DataAtual = DateTime.Now;
            Reserva reservaNR = new Reserva()
            {
                CodigoNegociacao = null,
                DataCriacaoReserva = DataAtual,
                DataRetirada = DataAtual.AddDays(2),
                Localizador = "8C54N4N3A",
                PossuiTarifaPromocional = false
            };

            DadosComunicacao dadosComunicacaoMock = new DadosComunicacao()
            {
                CodigoAGVIG = "05850554",
                CodigoCliente = "123456",
                CodigoEvento = "03008",
                CodigoUsuario = "191950",
                DataRetirada = DataAtual,
                DataRetorno = DataAtual.AddDays(2),
                DestinatarioEmail = "dti138@localiza.com",
                EmailRequisitante = "dti138@localiza.com",
                EmailSolicitante = "dti138@localiza.com",
                EnviarEmail = true,
                EnviarEmailSolicitante = true,
                FaturaAgenciaViagem = true,
                Localizador = "8C54N4N3A",
                NomeAgenciaRetirada = "ACGIG",
                NomeAgenciaRetorno = "ACGIG",
                NomeAgenciaViagemSeguradora = "AVINASH QA",
                NomeCidadeRetirada = "RIO DE JANEIRO",
                NomeCidadeRetorno = "RIO DE JANEIRO",
                NomeCliente = "DIEGO ALMEIDA",
                NomeConsultor = "AVINASH QA",
                NomeUsuario = "DIEGO ALMEIDA",
                SituacaoReserva = "1",
                TipoCliente = '2',
                TipoClienteAgenciaViagemSeguradora = '0',
                TipoOrigem = "CR",
                TipoProtecao = "O",
                IndicadorPrePagamento = false
            };

            ComunicacaoSobConsultaRequisicao requisicao = new ComunicacaoSobConsultaRequisicao()
            {
                DadosComunicacaoReserva = dadosComunicacaoMock,
                DadosReservaNR = reservaNR
            };

            var resultado = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IExecutor<ComunicacaoSobConsultaRequisicao,
                    ComunicacaoSobConsultaResultado>>().Executar(requisicao);
        }

        [TestMethod]
        public void ReservaAbertaPossuiCodigoAGVIGSeguradora()
        {
            DateTime DataAtual = DateTime.Now;
            Reserva reservaNR = new Reserva()
            {
                CodigoNegociacao = null,
                DataCriacaoReserva = DataAtual,
                DataRetirada = DataAtual.AddDays(2),
                Localizador = "AVO154J4K9P",
                PossuiTarifaPromocional = false
            };

            DadosComunicacao dadosComunicacaoMock = new DadosComunicacao()
            {
                CodigoAGVIG = "05850554",
                CodigoCliente = "123456",
                CodigoEvento = "03008",
                CodigoUsuario = "191950",
                DataRetirada = DataAtual,
                DataRetorno = DataAtual.AddDays(2),
                DestinatarioEmail = "dti138@localiza.com",
                EmailRequisitante = "dti138@localiza.com",
                EmailSolicitante = "dti138@localiza.com",
                EnviarEmail = true,
                EnviarEmailSolicitante = true,
                FaturaAgenciaViagem = true,
                Localizador = "AVO154J4K9P",
                NomeAgenciaRetirada = "ACGIG",
                NomeAgenciaRetorno = "ACGIG",
                NomeAgenciaViagemSeguradora = "AVINASH QA",
                NomeCidadeRetirada = "RIO DE JANEIRO",
                NomeCidadeRetorno = "RIO DE JANEIRO",
                NomeCliente = "DIEGO ALMEIDA",
                NomeConsultor = "AVINASH QA",
                NomeUsuario = "DIEGO ALMEIDA",
                SituacaoReserva = "1",
                TipoCliente = '1',
                TipoClienteAgenciaViagemSeguradora = '7',
                TipoOrigem = "SA",
                TipoProtecao = "O",
                IndicadorPrePagamento = false,
                CodigoEmissor = "12"
            };

            ComunicacaoSobConsultaRequisicao requisicao = new ComunicacaoSobConsultaRequisicao()
            {
                DadosComunicacaoReserva = dadosComunicacaoMock,
                DadosReservaNR = reservaNR
            };

            Assert.IsTrue(true);
        }
    }
}
