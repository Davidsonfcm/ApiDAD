using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using AL.Atendimento.SobConsulta.Repositorios.Entidade;
using System;
using System.Configuration;
using Atendimento = AL.Atendimento.SobConsulta.Repositorios.NRCentralDeReservas;
using Autorizacao = AL.Atendimento.SobConsulta.Repositorios.NRAutorizacao;
using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Util.Enumeracoes;
using AL.Atendimento.SobConsulta.Base.Repositorios;
using static AL.Atendimento.SobConsulta.Base.Repositorios.ConexaoBanco;
using System.ServiceModel;
using System.Net;
using System.Linq;
using Dapper;
using Localiza.SDK.Cache;
using Serilog;
using System.Collections.Generic;

namespace AL.Atendimento.SobConsulta.Repositorios.Reservas
{
    public class ReservaNrRepositorio : RepositorioBase<ReservaEntidade>, IReservaNrRepositorio
    {
        #region constantes

        private NRCentralDeReservas.DTOAutenticacaoAcessoExterno instanciaUnica;
        private DateTime ultimaSessaoNR = new DateTime();
        private readonly Object lockObjeto = new Object();

        #endregion

        #region Construtores

        public ReservaNrRepositorio() : base(TipoConexao.Aguia) { }

        #endregion

        #region Métodos Implementados

        protected NRCentralDeReservas.DTOAutenticacaoAcessoExterno ObterInstanciaAutorizacao()
        {
            lock (lockObjeto)
            {
                if (instanciaUnica == null || ultimaSessaoNR <= DateTime.Now)
                {
                    string token = ConfigurationManager.AppSettings["Token_Validacao_NR"];

                    instanciaUnica = Inicializar(token);
                    ultimaSessaoNR = AtualizarHoraSessaoExpira(token, instanciaUnica.IdSessao);
                }

                return instanciaUnica;
            }
        }

        private NRCentralDeReservas.DTOAutenticacaoAcessoExterno Inicializar(string tokenAplicacao)
        {
            Autorizacao.DTOAutenticacaoAcessoExterno autorizacao;

            using (Autorizacao.AutorizacaoClient autorizacaoClient = new Autorizacao.AutorizacaoClient())
            {
                autorizacao = autorizacaoClient.AutenticarAcessoExterno(tokenAplicacao);
            }
            return ConvertAutenticacaoInAtendimento(autorizacao);
        }

        private static NRCentralDeReservas.DTOAutenticacaoAcessoExterno ConvertAutenticacaoInAtendimento(Autorizacao.DTOAutenticacaoAcessoExterno autorizacao)
        {
            NRCentralDeReservas.DTOAutenticacaoAcessoExterno autenticacao = new NRCentralDeReservas.DTOAutenticacaoAcessoExterno();
            autenticacao.IdSessao = autorizacao.IdSessao;
            autenticacao.TokenAplicacao = autorizacao.TokenAplicacao;
            autenticacao.Cultura = autorizacao.Cultura;
            autenticacao.Matricula = autorizacao.Matricula;
            return autenticacao;
        }

        private DateTime AtualizarHoraSessaoExpira(string tokenAplicacao, string codigoSessao)
        {
            Autorizacao.StatusSessao statusToken;

            using (Autorizacao.AutorizacaoClient autorizacaoClient = new Autorizacao.AutorizacaoClient())
            {
                statusToken = autorizacaoClient.VerificarStatusSessaoAcessoExterno(tokenAplicacao, codigoSessao);
            }

            return DefinirHoraExpiracao(statusToken);
        }

        private static DateTime DefinirHoraExpiracao(Autorizacao.StatusSessao statusToken)
        {
            switch (statusToken)
            {
                case Autorizacao.StatusSessao.SessaoExpirada:
                case Autorizacao.StatusSessao.SessaoExpiraEmMenosDe5Minutos:
                    return DateTime.Now;
                case Autorizacao.StatusSessao.SessaoExpiraEmMenosDe10Minutos:
                    return DateTime.Now.AddMinutes(5);
                case Autorizacao.StatusSessao.SessaoExpiraEmMenosDe30Minutos:
                    return DateTime.Now.AddMinutes(20);
                case Autorizacao.StatusSessao.SessaoExpiraEmMenosDe1Hora:
                    return DateTime.Now.AddMinutes(50);
                case Autorizacao.StatusSessao.SessaoExpiraEmMaisDe1Hora:
                    return DateTime.Now.AddHours(1);
                default:
                    return DateTime.Now.AddMinutes(1);
            }
        }

        public Reserva ObterReserva(string localizador)
        {
            NRCentralDeReservas.DTOAutenticacaoAcessoExterno autorizacaoAcessoExterno = ObterInstanciaAutorizacao();
            NRCentralDeReservas.DTOReservaConsulta reservaDtoNR = null;

            using (NRCentralDeReservas.CentralReservaClient centralDeReservasClient = new NRCentralDeReservas.CentralReservaClient())
            {
                reservaDtoNR = centralDeReservasClient.Consultar(autorizacaoAcessoExterno, localizador);
            }

            if (reservaDtoNR == null)
                return null;

            Reserva reserva = ConverterDtoEntidade(reservaDtoNR);

            return reserva;
        }

        private Reserva ConverterDtoEntidade(NRCentralDeReservas.DTOReservaConsulta reservaDtoNR)
        {
            if (reservaDtoNR == null)
                return null;

            var possuiTarifaPromocional = ReservaPossuiTarifaPromocional(reservaDtoNR);
            var reserva = new Reserva
            {
                Localizador = reservaDtoNR.Localizador,
                DataRetirada = reservaDtoNR.DataHoraRetirada,
                DataCriacaoReserva = reservaDtoNR.DataHoraCriacao,
                PossuiTarifaPromocional = possuiTarifaPromocional,
                Grupo = reservaDtoNR.GrupoVeiculo.CodigoLocaliza == null ? null : reservaDtoNR.GrupoVeiculo.CodigoLocaliza.Trim(),
                Filial = reservaDtoNR.AgenciaRetirada.CodigoFilial == null ? null : reservaDtoNR.AgenciaRetirada.CodigoFilial.Trim(),
                Agencia = reservaDtoNR.AgenciaRetirada.CodigoLocaliza == null ? null : reservaDtoNR.AgenciaRetirada.CodigoLocaliza.Trim(),
                NomeAgencia = reservaDtoNR.AgenciaRetirada.Nome,
                CodigoNegociacao = reservaDtoNR.Tarifa.CodigoNegociacao,
                QuantidadeDiarias = reservaDtoNR.ValoresReserva.QuantidadeDiarias,
                TarifaMensal = reservaDtoNR.Tarifa.RegrasTarifa[0].AluguelTemporario,
                DataDevolucaoReserva = reservaDtoNR.DataHoraDevolucao,

                TelefoneAgenciaRetirada = reservaDtoNR.AgenciaRetirada.TelefoneContato,
                HorarioFuncionamentoAgenciaRetirada = ConverterDtoEntidadeHorarioFuncionamento(reservaDtoNR.AgenciaRetirada.ListaHorariosFuncionamento.ToList()),
                EnderecoAgenciaRetirada = reservaDtoNR.AgenciaRetirada.Endereco,
                Motorista = reservaDtoNR.Motorista != null,
                Fast = reservaDtoNR.ReservaFast,
                Entrega = reservaDtoNR.Entrega != null,
                AgenciaDevolucao = reservaDtoNR.AgenciaDevolucao.CodigoLocaliza,
                NomeAgenciaDevolucao = reservaDtoNR.AgenciaDevolucao.Nome,
                NomeCliente = reservaDtoNR.Cliente.NomeCompleto,
                SegmentoCliente = reservaDtoNR.Cliente.TipoCliente,
                ContatoCliente = ConverterDtoEntidadeTelefoneCliente(reservaDtoNR.Cliente.TelefoneContato),
                EmailCliente = reservaDtoNR.Cliente.Email,
                Observacao = reservaDtoNR.Observacao,
                Status = ConverterStatusNr(reservaDtoNR.Status),
                FormaPagamento = (TipoFormaPagamento)reservaDtoNR.FormaPagamento.Tipo,
                Consultor = reservaDtoNR.Requisitante != null ? reservaDtoNR.Requisitante.CodigoEmissor : String.Empty,//Pendente Buscar nome
                SituacaoFidelidade = reservaDtoNR.Cliente.SituacaoFidelidade
            };

            return reserva;
        }

        private string ConverterStatusNr(NRCentralDeReservas.StatusReserva status)
        {
            if (status == NRCentralDeReservas.StatusReserva.Aberta)
                return SituacaoReserva.Aberta;
            if (status == NRCentralDeReservas.StatusReserva.Cancelada)
                return SituacaoReserva.Cancelada;
            if (status == NRCentralDeReservas.StatusReserva.NaoConfirmada)
                return SituacaoReserva.NaoConfirmada;
            if (status == NRCentralDeReservas.StatusReserva.SobConsulta)
                return SituacaoReserva.SobConsulta;

            return "";

        }

        private TelefoneCliente ConverterDtoEntidadeTelefoneCliente(NRCentralDeReservas.DTOTelefone dto)
        {
            var entidade = new TelefoneCliente();
            if (dto != null)
            {
                entidade.DDD = dto.DDD;
                entidade.DDI = dto.DDI;
                entidade.Numero = dto.Numero;
                entidade.Tipo = (TipoTelefone)dto.Tipo;
            }
            return entidade;
        }

        private List<HorarioFuncionamento> ConverterDtoEntidadeHorarioFuncionamento(List<NRCentralDeReservas.DTOHorarioFuncionamento> dto)
        {
            var lista = new List<HorarioFuncionamento>();
            foreach (var item in dto)
            {
                var entidade = new HorarioFuncionamento();
                entidade.DiaDaSemana = (DiaReferenciaComercial)item.DiaDaSemana;
                entidade.DiaDaSemanaInternacional = item.DiaDaSemanaInternacional;
                entidade.HorarioAbertura = item.HorarioAbertura;
                entidade.HorarioFechamento = item.HorarioFechamento;
                lista.Add(entidade);
            }
            return lista;
        }

        private ReservaSobConsulta ConverterDtoEntidade(NRCentralDeReservas.DTOReservaSobConsulta reservaSobConsulta)
        {
            if (reservaSobConsulta == null)
                return null;

            var reserva = new ReservaSobConsulta
            {
                Localizador = reservaSobConsulta.Localizador,
                DataRetirada = reservaSobConsulta.DataHoraRetirada,
                DataDevolucao = reservaSobConsulta.DataHoraDevolucao,
                DataCriacaoReserva = reservaSobConsulta.DataCriacao,
                Grupo = !String.IsNullOrEmpty(reservaSobConsulta.GrupoVeiculo) ? reservaSobConsulta.GrupoVeiculo.ToUpper().Trim() : String.Empty,
                Filial = reservaSobConsulta.CodigoFilial == null ? null : reservaSobConsulta.CodigoFilial.Trim(),
                Agencia = reservaSobConsulta.CodigoAgencia == null ? null : reservaSobConsulta.CodigoAgencia.Trim(),
                NomeAgencia = reservaSobConsulta.NomeAgencia,
                NomeCliente = reservaSobConsulta.NomeCliente,
                QuantidadeDiarias = reservaSobConsulta.DataHoraDevolucao.Subtract(reservaSobConsulta.DataHoraRetirada).Days,
                TarifaMensal = reservaSobConsulta.TarifaMensal,
                CodigoOrigem = reservaSobConsulta.CodigoOrigem,
                DescricaoOrigem = reservaSobConsulta.DescricaoOrigem
                //SegmentoCliente = reservaSobConsulta.SegmentoCliente //TO DO Alteração Nucleo
            };
            return reserva;
        }

        private bool ReservaPossuiTarifaPromocional(NRCentralDeReservas.DTOReservaConsulta reservaDtoNR)
        {
            return reservaDtoNR != null && reservaDtoNR.TarifaPromocional != null && reservaDtoNR.TarifaPromocional.NovoCodigoPromocional && TarifaTemCodigoPromocional(reservaDtoNR.Tarifa);
        }

        private bool TarifaTemCodigoPromocional(NRCentralDeReservas.DTOTarifa tarifaSelecionada)
        {
            return tarifaSelecionada != null && tarifaSelecionada.CondicaoComercial != null && tarifaSelecionada.CondicaoComercial.PossuiCodigoPromocional;
        }

        public ReservaSobConsulta[] ListarReservasSobConsulta(bool internacional = false)
        {
            List<ReservaSobConsulta> retorno = new List<ReservaSobConsulta>();

            NRCentralDeReservas.DTOReservaSobConsulta[] reservasSobConsulta = new NRCentralDeReservas.DTOReservaSobConsulta[] { };

            using (NRCentralDeReservas.CentralReservaClient CentralReservaWS = new NRCentralDeReservas.CentralReservaClient())
            {
                reservasSobConsulta = CentralReservaWS.ListarReservasSobConsulta(ObterInstanciaAutorizacao(), internacional);
            }

            if (reservasSobConsulta != null)
            {
                retorno = reservasSobConsulta.Select(item => ConverterDtoEntidade(item)).ToList();
            }

            return retorno.ToArray();
        }
        public bool VerificarSeOfertasEstaoAtivasParaAgencia(string codigoAgenciaRetirada)
        {
            NRCentralDeReservas.DTOAutenticacaoAcessoExterno autorizacaoAcessoExterno = ObterInstanciaAutorizacao();

            if (String.IsNullOrEmpty(codigoAgenciaRetirada))
                return true;
            try
            {
                using (NRCentralDeReservas.CentralReservaClient CentralReservaWS = new NRCentralDeReservas.CentralReservaClient())
                {
                    return CentralReservaWS.VerificarSeOfertasEstaoAtivasParaAgencia(autorizacaoAcessoExterno, codigoAgenciaRetirada);
                }
            }
            catch
            {
                return false;
            }
        }

        public void EnviarEmailNucleoReserva(string localizador)
        {
            using (NRCentralDeReservas.CentralReservaClient CentralReservaWS = new NRCentralDeReservas.CentralReservaClient())
            {
                NRCentralDeReservas.DTOAutenticacaoAcessoExterno autorizacaoAcessoExterno = ObterInstanciaAutorizacao();
                CentralReservaWS.ReenviarConfirmacaoPorEmail(autorizacaoAcessoExterno, localizador);
            }
        }
        public void AlterarStatusReservaNr(DadosComunicacao reservaWs)
        {
            NRCentralDeReservas.DTOReservaConsultaCompleto reservaConsulta = ConsultarReserva(reservaWs.Localizador);

            if (reservaWs.IndicadorPrePagamento)
            {
                reservaWs.SituacaoReserva = MudarStatusReservaAbertaDePrePagamento(reservaWs);
            }

            NRCentralDeReservas.DTODadosBasicosReserva dadosBasicos = ObterDadosBasicosReserva(reservaWs);
            NRCentralDeReservas.DTOCliente dadosCliente = ObterClienteUsuarioReserva(reservaWs, reservaConsulta);

            using (NRCentralDeReservas.CentralReservaClient centralReservaService = new NRCentralDeReservas.CentralReservaClient())
            {
                NRCentralDeReservas.DTOAutenticacaoAcessoExterno autorizacaoAcessoExterno = ObterInstanciaAutorizacao();

                NRCentralDeReservas.DTOMotorista motorista = ObterDadosMotoristaDeAcordoComReserva(reservaWs);
                centralReservaService.AlterarDadosBasicos(autorizacaoAcessoExterno,
                        reservaWs.Localizador, dadosBasicos, dadosCliente, motorista);
            }

        }

        private NRCentralDeReservas.DTOMotorista ObterDadosMotoristaDeAcordoComReserva(DadosComunicacao reservaWs)
        {
            if (reservaWs.TipoMotorista != char.MinValue)
            {
                return new NRCentralDeReservas.DTOMotorista()
                {
                    CodigoTipoMotorista = reservaWs.TipoMotorista.ToString(),
                    Destino = reservaWs.Destino,
                    LocalAtendimento = reservaWs.LocalAtendimentoMotorista,
                    PodeContratar = true,
                    Roteiro = reservaWs.Roteiro,
                    Viagem = reservaWs.MotoristaDisponivelViagem,
                    Terno = reservaWs.MotoristaTerno,
                };
            }
            return null;
        }

        private NRCentralDeReservas.DTOCliente ObterClienteUsuarioReserva(DadosComunicacao reservaWs, NRCentralDeReservas.DTOReservaConsultaCompleto reservaConsulta)
        {
            NRCentralDeReservas.DTOCliente cliente = new NRCentralDeReservas.DTOCliente();
            cliente.CodigoTipoDocumento = reservaWs.TipoNacionalidadeClienteUsuario == 1 ? "3" : "1";
            if (reservaWs.TipoCliente == '2')
            {
                if (reservaConsulta != null)
                {
                    reservaConsulta.Requisitante.CodigoPJ = reservaWs.CodigoCliente;
                    reservaConsulta.Requisitante.CodigoEmissor = reservaWs.CodigoEmissor;
                }

                cliente.CodigoLocaliza = reservaWs.CodigoUsuario;
                cliente.NumeroDocumento = reservaWs.CpfUsuario;
                cliente.NomeCompleto = reservaWs.NomeUsuario;
            }
            else
            {
                cliente.CodigoLocaliza = reservaWs.CodigoCliente;
                cliente.NomeCompleto = reservaWs.NomeCliente;
                cliente.NumeroDocumento = reservaWs.Cpf;
            }

            cliente.Email = reservaWs.EmailRequisitante;
            if (!String.IsNullOrEmpty(reservaWs.TelefoneRequisitante))
            {
                cliente.TelefoneContato = ObterTelefoneDaReserva(reservaWs.TelefoneRequisitante, NRCentralDeReservas.TipoTelefone.Outro);
            }
            if (!String.IsNullOrEmpty(reservaWs.CelularSMS))
            {
                cliente.TelefoneSMS = ObterTelefoneDaReserva(reservaWs.CelularSMS, NRCentralDeReservas.TipoTelefone.Celular);
            }
            return cliente;
        }

        private NRCentralDeReservas.DTODadosBasicosReserva ObterDadosBasicosReserva(DadosComunicacao reservaWs)
        {
            NRCentralDeReservas.DTODadosBasicosReserva dadosBasicos = new NRCentralDeReservas.DTODadosBasicosReserva();
            dadosBasicos.BinaRequisitante = new NRCentralDeReservas.DTOTelefone();
            if (!String.IsNullOrEmpty(reservaWs.BinaRequisitante))
            {
                dadosBasicos.BinaRequisitante = ObterTelefoneDaReserva(reservaWs.BinaRequisitante, NRCentralDeReservas.TipoTelefone.Outro);
            }
            dadosBasicos.CelularRequisitante = new NRCentralDeReservas.DTOTelefone();
            if (!String.IsNullOrEmpty(reservaWs.TelefoneCelularRequisitante))
            {
                dadosBasicos.CelularRequisitante = ObterTelefoneDaReserva(reservaWs.TelefoneCelularRequisitante, NRCentralDeReservas.TipoTelefone.Celular);
            }
            dadosBasicos.TelefoneSolicitante = new NRCentralDeReservas.DTOTelefone();
            if (!String.IsNullOrEmpty(reservaWs.TelefoneSolicitante))
            {
                dadosBasicos.TelefoneSolicitante = ObterTelefoneDaReserva(reservaWs.TelefoneSolicitante, NRCentralDeReservas.TipoTelefone.Outro);
            }
            dadosBasicos.CiaAerea = (!String.IsNullOrEmpty(reservaWs.CodigoEmpresaAerea)) ? reservaWs.CodigoEmpresaAerea : null;
            dadosBasicos.CodigoOrigemReserva = reservaWs.TipoOrigem;
            dadosBasicos.CodigoTipoReserva = (!String.IsNullOrEmpty(reservaWs.TipoReserva) ? reservaWs.TipoReserva.Trim() : string.Empty);
            dadosBasicos.DataHoraVoo = reservaWs.DataVoo != DateTime.MinValue ? reservaWs.DataVoo : (DateTime?)null;
            dadosBasicos.EmailSolicitante = reservaWs.EmailSolicitante;
            dadosBasicos.EnviarSMS = reservaWs.EnviarSMS;
            dadosBasicos.EnviarEmailConfirmacao = reservaWs.EnviarEmail;
            dadosBasicos.JustificativaDesconto = String.Empty;
            dadosBasicos.NomeRequisitante = reservaWs.NomeRequisitante;
            dadosBasicos.NumeroVoo = (!String.IsNullOrEmpty(reservaWs.NumeroVoo)) ? Convert.ToInt32(reservaWs.NumeroVoo) : (int?)null;
            dadosBasicos.Observacao = reservaWs.ObservacaoReserva;
            dadosBasicos.StatusReserva = reservaWs.SituacaoReserva;
            dadosBasicos.JustificativaDesconto = string.Empty;
            dadosBasicos.MotivoCancelamento = reservaWs.CodigoMotivoCancelamento.ToString();
            dadosBasicos.MotivoNaoConfirmada = reservaWs.MotivoNaoConfirmacao.ToString();
            dadosBasicos.CartaoCredito = new NRCentralDeReservas.DTOCartaoCredito();
            dadosBasicos.CartaoCredito.NumeroCartao = reservaWs.CodigoCartao;
            dadosBasicos.CartaoCredito.CodigoBandeira = reservaWs.CodigoAdministradoraCartao;
            dadosBasicos.CartaoCredito.Vencimento = reservaWs.DataValidadeCartrao;
            dadosBasicos.CartaoCredito.NomeIdentificacao = String.Empty;

            if (reservaWs.TipoCliente == '2')
            {
                dadosBasicos.CodigoClientePJ = reservaWs.CodigoCliente;
            }
            dadosBasicos.CodigoEmissor = reservaWs.CodigoEmissor;
            dadosBasicos.EnviarEmailSolicitante = reservaWs.EnviarEmailSolicitante;
            dadosBasicos.ContatosReserva = ObterContatosReserva(reservaWs.ContatosAssociados);
            dadosBasicos.PNR = reservaWs.PNR;
            dadosBasicos.IdentificadorExterno = reservaWs.NumeroSinistro;

            dadosBasicos.CodigoSequencialDepartamento = reservaWs.CodigoDepartamento == 0 ? (int?)null : reservaWs.CodigoDepartamento;
            dadosBasicos.UpgradeFidelidade = ObterUpgradeFidelidade(reservaWs);
            return dadosBasicos;
        }

        private NRCentralDeReservas.DTOContatoReserva[] ObterContatosReserva(ContatosAssociadosReserva[] associacaoReservaContato)
        {
            if (associacaoReservaContato != null && associacaoReservaContato.Length > 0)
            {
                return associacaoReservaContato.Select(contatoAssociado => new NRCentralDeReservas.DTOContatoReserva()
                {
                    CodigoContato = contatoAssociado.CodigoContato
                }).ToArray();
            }
            return new NRCentralDeReservas.DTOContatoReserva[0];
        }

        private NRCentralDeReservas.DTOUpgrade ObterUpgradeFidelidade(DadosComunicacao reservaWs)
        {
            NRCentralDeReservas.DTOCotacao consultarCotacao = RecuperarCotacao(reservaWs.Localizador);
            NRCentralDeReservas.DTOReservaConsultaCompleto reservaConsulta = ConsultarReserva(reservaWs.Localizador);

            if (consultarCotacao != null || reservaConsulta == null ||
                reservaWs.TipoUpgradeCliente != (int)NRCentralDeReservas.TipoUpgrade.Garantido)
                return null;


            NRCentralDeReservas.DTOUpgrade upgrade = new NRCentralDeReservas.DTOUpgrade();
            upgrade.GrupoVeiculo = reservaConsulta.GrupoVeiculo;
            upgrade.TipoUpgrade = NRCentralDeReservas.TipoUpgrade.Garantido;
            upgrade.GrupoUpgrade = new NRCentralDeReservas.DTOGrupoVeiculo();
            upgrade.GrupoUpgrade.CodigoLocaliza = reservaWs.GrupoDestino;
            upgrade.GrupoUpgrade.Categoria = NRCentralDeReservas.CategoriaGrupo.CarroPasseio;
            return upgrade;
        }

        private NRCentralDeReservas.DTOTelefone ObterTelefoneDaReserva(string binaRequisitante, NRCentralDeReservas.TipoTelefone tipoTelefone)
        {
            NRCentralDeReservas.DTOTelefone telefone = new NRCentralDeReservas.DTOTelefone();
            try
            {
                if (!String.IsNullOrEmpty(binaRequisitante))
                {
                    telefone.DDI = binaRequisitante.Substring(0, 4);
                    telefone.DDD = binaRequisitante.Substring(4, 4);
                    telefone.Numero = binaRequisitante.Substring(binaRequisitante.Length - 8);
                    telefone.Tipo = tipoTelefone;
                }
            }
            catch 
            {
                return null;
            }

            return telefone;
        }

        private string MudarStatusReservaAbertaDePrePagamento(DadosComunicacao reservaWs)
        {
            if (RegraValidacaoStatusAguardandoPagamentoReservaPrePagamento(reservaWs))
            {
                return "B";
            }
            else
            {
                return reservaWs.SituacaoReserva;
            }
        }

        private bool RegraValidacaoStatusAguardandoPagamentoReservaPrePagamento(DadosComunicacao reservaWs)
        {
            string situacaoAtualReserva = reservaWs.SituacaoReserva.Replace(";", "");
            string situacaoReservaBanco = null;

            if (!String.IsNullOrEmpty(reservaWs.Localizador))
            {
                using (wsReserva.Reservas servicoWsReserva = new wsReserva.Reservas())
                {
                    situacaoReservaBanco = servicoWsReserva.BuscarSituacaoReserva(reservaWs.Localizador).Tables[0].Rows[0]["res_status"].ToString();
                }
            }

            return reservaWs != null && situacaoAtualReserva != situacaoReservaBanco &&
                reservaWs.IndicadorPrePagamento && reservaWs.SituacaoReserva.Replace(";", "") == "1";
        }

        private NRCentralDeReservas.DTOCotacao RecuperarCotacao(string localizador)
        {
            using (NRCentralDeReservas.CentralReservaClient CentralReservaNR = new NRCentralDeReservas.CentralReservaClient())
            {
                NRCentralDeReservas.DTOAutenticacaoAcessoExterno autorizacaoAcessoExterno = ObterInstanciaAutorizacao();
                NRCentralDeReservas.DTOCotacao CotacaoReserva = CentralReservaNR.RecuperarCotacao(autorizacaoAcessoExterno, localizador);
                return CotacaoReserva;
            }
        }

        private NRCentralDeReservas.DTOReservaConsultaCompleto ConsultarReserva(string localizador)
        {
            using (NRCentralDeReservas.CentralReservaClient CentralReservaNR = new NRCentralDeReservas.CentralReservaClient())
            {
                NRCentralDeReservas.DTOAutenticacaoAcessoExterno autorizacaoAcessoExterno = ObterInstanciaAutorizacao();
                NRCentralDeReservas.DTOReservaConsultaCompleto ReservaConsulta = CentralReservaNR.Consultar(autorizacaoAcessoExterno, localizador);
                return ReservaConsulta;
            }
        }
    }
    #endregion
}
