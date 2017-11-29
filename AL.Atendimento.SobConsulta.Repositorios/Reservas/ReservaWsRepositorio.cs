using AL.Atendimento.SobConsulta.Base.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AL.Atendimento.SobConsulta.Entidades;
using static AL.Atendimento.SobConsulta.Base.Repositorios.ConexaoBanco;
using AL.Atendimento.SobConsulta.Util;
using AL.Atendimento.SobConsulta.Repositorios.wsReserva;
using AL.Atendimento.SobConsulta.Util.Excecoes;

namespace AL.Atendimento.SobConsulta.Repositorios.Reservas
{
    public class ReservaWsRepositorio : RepositorioBase<ReservaWsEntidade>, IReservaWsRepositorio
    {
        #region Constantes

        #endregion

        #region Construtores
        public ReservaWsRepositorio() : base(TipoConexao.Aguia) { }
        #endregion

        #region Metodos Públicos
        public DadosComunicacao obterDadosReserva(string localizador, string codigoUsuario)
        {

            var reserva = new wsReserva.Reserva();
            using (wsReserva.Reservas servicoReserva = ObterServicoReservaConfigurado(codigoUsuario))
            {
                reserva = servicoReserva.Buscar(localizador);
            }

            DadosComunicacao dadosComunicacao = ConverterDtoEntidadeComunicacao(reserva);

            return dadosComunicacao;

        }

        public void EnviarEmailConsultorAGVIGSeNecessario(DadosComunicacao dadosComunicacao, string codigoUsuario)
        {

            using (wsReserva.Reservas servicoReserva = ObterServicoReservaConfigurado(codigoUsuario))
            {
                servicoReserva.EnviarEmailConsultorAGVIGSeNecessario(dadosComunicacao.DestinatarioEmail, dadosComunicacao.NomeConsultor, dadosComunicacao.CodigoAGVIG, dadosComunicacao.NomeAgenciaViagemSeguradora, dadosComunicacao.NomeCliente,
                    dadosComunicacao.TipoProtecao, dadosComunicacao.Localizador, dadosComunicacao.NomeCliente, dadosComunicacao.NomeCidadeRetirada, dadosComunicacao.NomeCidadeRetorno, dadosComunicacao.NomeAgenciaRetirada, dadosComunicacao.NomeAgenciaRetorno,
                    dadosComunicacao.DataRetirada, dadosComunicacao.DataRetorno);
            }
        }

        public void EnviarEmailConfirmacaoSolicitante(string localizador, string codigoUsuario)
        {
            using (wsReserva.Reservas servicoReserva = ObterServicoReservaConfigurado(codigoUsuario))
            {
                servicoReserva.EnviarEmailConfirmacaoReservaAsync(localizador, false);
            }
        }
        #endregion

        #region Metodos Privados
        private DadosComunicacao ConverterDtoEntidadeComunicacao(wsReserva.Reserva reservaWsReserva)
        {
            if (reservaWsReserva == null)
            {
                return null;
            }
            ContatosAssociadosReserva[] contatosAssociadosReserva = ConverterContatosAssociadosReserva(reservaWsReserva.ContatosAssociadosReserva);

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
                IdOferta = reservaWsReserva.IdOferta,
                IndicadorPrePagamento = reservaWsReserva.IndicadorPrePagamento,
                BinaRequisitante = reservaWsReserva.BinaRequisitante,
                TelefoneCelularRequisitante = reservaWsReserva.TelefoneCelularRequisitante,
                CodigoEmpresaAerea = reservaWsReserva.CodigoEmpresaAerea,
                TipoReserva = reservaWsReserva.SiglaTipoReserva,
                DataVoo = reservaWsReserva.DataVoo,
                EnviarSMS = reservaWsReserva.EnviarSMS,
                NomeRequisitante = reservaWsReserva.NomeRequisitante,
                NumeroVoo = reservaWsReserva.NumeroVoo,
                ObservacaoReserva = reservaWsReserva.ObservacaoReserva,
                TelefoneSolicitante = reservaWsReserva.TelefoneSolicitante,
                CodigoMotivoCancelamento = reservaWsReserva.CodigoMotivoCancelamento,
                MotivoNaoConfirmacao = reservaWsReserva.MotivoNaoConfirmacao,
                CodigoCartao = reservaWsReserva.CodigoCartao,
                CodigoAdministradoraCartao = reservaWsReserva.CodigoAdministradoraCartao,
                DataValidadeCartrao = reservaWsReserva.DataValidadeCartrao,
                CodigoEmissor = reservaWsReserva.CodigoEmissor,
                PNR = reservaWsReserva.Pnr,
                NumeroSinistro = reservaWsReserva.NumeroSinistro,
                CodigoDepartamento = reservaWsReserva.CodigoDepartamento,
                TipoUpgradeCliente = reservaWsReserva.UpgradeCliente != null ? reservaWsReserva.UpgradeCliente.TipoUpgrade : null,
                GrupoDestino = reservaWsReserva.UpgradeCliente != null ? reservaWsReserva.UpgradeCliente.GrupoDestino : string.Empty,
                TelefoneRequisitante = reservaWsReserva.TelefoneRequisitante,
                CelularSMS = reservaWsReserva.CelularSMS,
                CpfUsuario = reservaWsReserva.CpfUsuario,
                Cpf = reservaWsReserva.Cpf,
                TipoNacionalidadeClienteUsuario = reservaWsReserva.TipoNacionalidadeClienteUsuario,
                TipoMotorista = reservaWsReserva.TipoMotorista,
                CodigoTipoMotorista = reservaWsReserva.TipoMotorista.ToString(),
                Destino = reservaWsReserva.Destino,
                LocalAtendimentoMotorista = reservaWsReserva.LocalAtendimentoMotorista,
                Roteiro = reservaWsReserva.Roteiro,
                MotoristaDisponivelViagem = reservaWsReserva.MotoristaDisponivelViagem,
                MotoristaTerno = reservaWsReserva.MotoristaTerno,
                ContatosAssociados = contatosAssociadosReserva,
                GrupoReserva = reservaWsReserva.GrupoTarifa

                //TipoProtecao = reservaWsReserva.
            };

            return dadosComunicacao;
        }

        private ContatosAssociadosReserva[] ConverterContatosAssociadosReserva(AssociacaoReservaContato[] contatosAssociadosReserva)
        {
            if (contatosAssociadosReserva != null && contatosAssociadosReserva.Length > 0)
            {
                return contatosAssociadosReserva.Select(contatoAssociado => new ContatosAssociadosReserva()
                {
                    CodigoCliente = contatoAssociado.CodigoCliente,
                    CodigoContato = contatoAssociado.CodigoContato,
                    CodigoReserva = contatoAssociado.CodigoReserva,
                    DataResposta = contatoAssociado.DataResposta,
                    DataUltimoEnvio = contatoAssociado.DataUltimoEnvio,
                    Resposta = contatoAssociado.Resposta
                }).ToArray();
            }

            return new List<ContatosAssociadosReserva>().ToArray();
        }

        public void AlterarStatusReservaWs(DadosComunicacao reservaWs, string codigoUsuario)
        {
            using (wsReserva.Reservas servicoWsReserva = ObterServicoReservaConfigurado(codigoUsuario))
            {
                wsReserva.Reserva reserva = servicoWsReserva.Buscar(reservaWs.Localizador);
                if(reservaWs.SituacaoReserva == "1" 
                    && reserva.TipoCliente == '2' 
                    && reserva.SiglaTipoReserva == "8C")
                {
                    reserva.SituacaoReserva = "6";
                }
                else
                {
                    reserva.SituacaoReserva = reservaWs.SituacaoReserva;
                }
                reserva.ObservacaoReserva = reservaWs.ObservacaoReserva;
                reserva.CodigoMotivoCancelamento = reservaWs.CodigoMotivoCancelamento;
                reserva.NumeroSinistro = reservaWs.NumeroSinistro;
                reserva.EnviarEmail = reservaWs.EnviarEmail;
                reserva.EnviarSMS = reservaWs.EnviarSMS;
                reserva.MotivoNaoConfirmacao = reservaWs.MotivoNaoConfirmacao;

                try
                {
                    var retorno = servicoWsReserva.GravarReserva(reserva);

                    if (!retorno.Situacao)
                    {
                        string mensagem = retorno.Mensagem ?? "Erro Genérico ao gravar Reserva";
                        throw new NegocioException(mensagem, CodigosErro.RESERVA_NAO_PODE_SER_ALTERADA);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public string BuscarSituacaoReserva(string numeroReserva, string codigoUsuario)
        {
            using (wsReserva.Reservas servicoWsReserva = ObterServicoReservaConfigurado(codigoUsuario))
            {
                return servicoWsReserva.BuscarSituacaoReserva(numeroReserva).Tables[0].Rows[0]["res_status"].ToString();
            }
        }

        private wsReserva.Reservas ObterServicoReservaConfigurado(string codigoUsuario)
        {
            wsReserva.Reservas servicoWsReserva = new wsReserva.Reservas();
            servicoWsReserva.HeaderValue = new wsReserva.Header();
            servicoWsReserva.HeaderValue.ApplicationName = Configuracoes.Geral.NomeAplicacao;
            servicoWsReserva.HeaderValue.User = codigoUsuario;
            servicoWsReserva.HeaderValue.WorkstationID = Environment.MachineName;

            return servicoWsReserva;

        }
        #endregion
    }
}
