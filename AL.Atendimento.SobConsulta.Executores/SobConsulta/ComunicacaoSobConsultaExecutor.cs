using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using AL.Atendimento.SobConsulta.Repositorios.Reservas;
using AL.Atendimento.SobConsulta.Util;
using Localiza.SDK.Fronteira;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Executores.SobConsulta
{
    public class ComunicacaoSobConsultaExecutor : IExecutor<ComunicacaoSobConsultaRequisicao, ComunicacaoSobConsultaResultado>
    {
        private readonly IReservaWsRepositorio reservaWsRepositorio;
        private readonly IReservaNrRepositorio reservaNrRepositorio;
        private readonly IItensParametrosRepositorio itensParametrosRepositorio;
        private readonly ICanaisWebRepositorio canaisWebRepositorio;
        private readonly IEnviarEmailReportRepositorio enviarEmailReportRepositorio;
        public ComunicacaoSobConsultaExecutor(IReservaWsRepositorio reservaRepositorio, IItensParametrosRepositorio itensParametrosRepositorio, IReservaNrRepositorio reservaNrRepositorio, 
            ICanaisWebRepositorio canaisWebRepositorio, IEnviarEmailReportRepositorio enviarEmailReportRepositorio)
        {
            this.reservaWsRepositorio = reservaRepositorio;
            this.itensParametrosRepositorio = itensParametrosRepositorio;
            this.reservaNrRepositorio = reservaNrRepositorio;
            this.canaisWebRepositorio = canaisWebRepositorio;
            this.enviarEmailReportRepositorio = enviarEmailReportRepositorio;
        }
        public ComunicacaoSobConsultaResultado Executar(ComunicacaoSobConsultaRequisicao requisicao)
        {
            if (requisicao != null && requisicao.DadosComunicacaoReserva != null)
            {
                var dadosComunicacaoReserva = requisicao.DadosComunicacaoReserva;
                var dadosReservasNR = requisicao.DadosReservaNR;

                if (dadosComunicacaoReserva.TipoOrigem == "CR")
                {
                    EnviarEmailConsultorAGVIG(dadosComunicacaoReserva, requisicao.CodigoUsuario);
                }
                if (dadosComunicacaoReserva.SituacaoReserva == "1")
                {
                    var origensParceiras = itensParametrosRepositorio.ObterListaParametroOrigemParceiros();
                    bool origemInformadaEhDeParceia = OrigemInformadaEhDeParceria(dadosComunicacaoReserva.TipoOrigem, origensParceiras);
                    bool enviarEmailPF = (
                           dadosComunicacaoReserva.TipoOrigem == "IT"
                        || dadosComunicacaoReserva.TipoOrigem == "PH"
                        || origemInformadaEhDeParceia
                        || (dadosComunicacaoReserva.Localizador.Substring(0, 2).Equals("8C")
                        && ((!string.IsNullOrEmpty(dadosComunicacaoReserva.CodigoCliente) && dadosComunicacaoReserva.TipoCliente.Equals('1'))
                        || (!string.IsNullOrEmpty(dadosComunicacaoReserva.NomeCliente)))
                        && string.IsNullOrEmpty(dadosComunicacaoReserva.NomeAgenciaViagemSeguradora))
                    );
                    if ((dadosComunicacaoReserva.EnviarEmail && !string.IsNullOrEmpty(dadosComunicacaoReserva.EmailRequisitante))
                       || (dadosComunicacaoReserva.EnviarEmailSolicitante && !string.IsNullOrEmpty(dadosComunicacaoReserva.EmailSolicitante)))
                    {
                        bool ofertasHabilitadas = reservaNrRepositorio.VerificarSeOfertasEstaoAtivasParaAgencia(dadosComunicacaoReserva.NomeAgenciaRetirada.Trim());
                        EnviarEmailReservaCliente(enviarEmailPF, ofertasHabilitadas, dadosComunicacaoReserva, dadosReservasNR, requisicao.CodigoUsuario);
                    }
                }
            }
            return new ComunicacaoSobConsultaResultado
            {
                Estado = EstadoResultado.OK
            };
        }

        private void EnviarEmailReservaCliente(bool enviarEmailPF, bool ofertasHabilitadas, DadosComunicacao dadosComunicacaoReserva, Reserva dadosReservasNR, string codigoUsuario)
        {
            if (dadosComunicacaoReserva.EnviarEmail)
            {
                if (enviarEmailPF && ofertasHabilitadas
                    && ((String.IsNullOrEmpty(dadosComunicacaoReserva.CodigoEvento) || dadosReservasNR.PossuiTarifaPromocional))
                    && String.IsNullOrEmpty(dadosReservasNR.CodigoNegociacao))
                {
                    EnviarEmailNucleoReserva(dadosComunicacaoReserva.Localizador);
                }
                else if (enviarEmailPF)
                {
                    var EmailEnviadoPeloNucleo = VerificarSeListaParametrosContemValor(Configuracoes.ItensParametro.OrigemNucleo, dadosComunicacaoReserva.TipoOrigem);
                    var EmailNovoFormatoParaInternetMobile = VerificarSeListaParametrosContemValor(Configuracoes.ItensParametro.NovoFormatoInternetMobile, dadosComunicacaoReserva.TipoOrigem);
                    if (EmailEnviadoPeloNucleo || dadosComunicacaoReserva.IndicadorPrePagamento || EmailNovoFormatoParaInternetMobile
                        || (!String.IsNullOrEmpty(dadosComunicacaoReserva.CodigoEvento) || dadosComunicacaoReserva.IndicadorPrePagamento))
                    {
                        EnviarEmailNucleoReserva(dadosComunicacaoReserva.Localizador);
                    }
                    else
                    {
                        DadosEnvioEmailCeres dadosEmail = new DadosEnvioEmailCeres()
                        {
                            Alteracao = true,
                            DestinatarioEmail = dadosComunicacaoReserva.DestinatarioEmail,
                            EmailRequisitante = dadosComunicacaoReserva.EmailRequisitante,
                            EnviarEmail = dadosComunicacaoReserva.EnviarEmail,
                            Localizador = dadosComunicacaoReserva.Localizador

                        };
                        EnviaEmail(dadosEmail);
                    }
                }
            }

            if ((!enviarEmailPF && dadosComunicacaoReserva.EnviarEmail) || dadosComunicacaoReserva.EnviarEmailSolicitante)
            {
                if (VerificarSeEhReservaAGVIG(dadosComunicacaoReserva.TipoClienteAgenciaViagemSeguradora))
                {
                    EnviarEmailAgenciaViagem(dadosComunicacaoReserva.EmailSolicitante, dadosComunicacaoReserva.EmailRequisitante, dadosComunicacaoReserva.Localizador, dadosComunicacaoReserva.CodigoEmissor);
                }
                else if (!ofertasHabilitadas)
                {
                    EnviarEmailConfirmacaoSolicitante(dadosComunicacaoReserva.Localizador, codigoUsuario);
                }
            }
        }

        private void EnviaEmail(DadosEnvioEmailCeres dadosEmail)
        {
            enviarEmailReportRepositorio.EnviarEmailViaReport(dadosEmail);
        }

        private void EnviarEmailConfirmacaoSolicitante(string localizador,string codigoUsuario)
        {
            reservaWsRepositorio.EnviarEmailConfirmacaoSolicitante(localizador, codigoUsuario);
        }

        private void EnviarEmailAgenciaViagem(string emailSolicitante, string emailRequisitante, string localizador, string codigoEmissor)
        {
            List<string> destinatariosEmail = new List<string>();
            if (!String.IsNullOrEmpty(emailSolicitante))
            {
                destinatariosEmail.Add(emailSolicitante);
            }
            if (!String.IsNullOrEmpty(emailRequisitante))
            {
                destinatariosEmail.Add(emailRequisitante);
            }

            canaisWebRepositorio.EnviarEmailConfirmacaoReservaComListaDestinatarios(localizador, destinatariosEmail.ToArray(), codigoEmissor);
        }

        private bool VerificarSeEhReservaAGVIG(char tipoClienteAgenciaViagemSeguradora)
        {
            return tipoClienteAgenciaViagemSeguradora == '3' || tipoClienteAgenciaViagemSeguradora == '7';
        }

        private bool VerificarSeListaParametrosContemValor(string codigoParametro, string tipoOrigem)
        {
            return itensParametrosRepositorio.VerificarSeListaParametrosContemValor(codigoParametro, tipoOrigem);
        }


        private void EnviarEmailNucleoReserva(string localizador)
        {
            reservaNrRepositorio.EnviarEmailNucleoReserva(localizador);
        }

        private bool OrigemInformadaEhDeParceria(string tipoOrigem, string[] origensParceiras)
        {
            if (String.IsNullOrEmpty(tipoOrigem))
            {
                return false;
            }

            foreach (string origemCorrente in origensParceiras)
            {
                if (tipoOrigem.Trim() == origemCorrente.Trim())
                {
                    return true;
                }
            }
            return false;
        }

        private void EnviarEmailConsultorAGVIG(DadosComunicacao dadosComunicacao, string codigoUsuario)
        {
            if (VerificarNecessidadeDeEnviarEmailConsultorAGVIG(dadosComunicacao.Localizador, dadosComunicacao.FaturaAgenciaViagem, dadosComunicacao.SituacaoReserva))
            {
                reservaWsRepositorio.EnviarEmailConsultorAGVIGSeNecessario(dadosComunicacao, codigoUsuario);
            }
        }

        private bool VerificarNecessidadeDeEnviarEmailConsultorAGVIG(string localizador, bool faturaAgenciaViagem, string statusReserva)
        {
            return (!string.IsNullOrEmpty(localizador)
                && (faturaAgenciaViagem)
                && (statusReserva == "1"));
           
        }
    }
}