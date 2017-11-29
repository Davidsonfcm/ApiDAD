
using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using AL.Atendimento.SobConsulta.Util;
using Localiza.SDK.InversaoControle;
using System;
using System.IO;

namespace AL.Atendimento.SobConsulta.Repositorios
{
    public class EnviarEmailReportRepositorio : IEnviarEmailReportRepositorio
    {
        #region Constantes

        #endregion

        #region Construtores
        public EnviarEmailReportRepositorio() { }
        #endregion

        #region Metodos Implementados

        public void EnviarEmailViaReport(DadosEnvioEmailCeres dadosEmail)
        {
            /*Gerar Relatorio*/
            wsReport.ReportingService rsReport = new wsReport.ReportingService();
            rsReport.Credentials = new System.Net.NetworkCredential(Configuracoes.ParametroReport.UsuarioReport, Configuracoes.ParametroReport.SenhaReport,
                Configuracoes.ParametroReport.DominioReport);

            rsReport.Url = Configuracoes.ParametroReport.URLRelatorio;

            string DevInfo = "<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";
            string formato = Configuracoes.ParametroReport.FormatoReport;
            string relatorioPath = Configuracoes.ParametroReport.CaminhoRelatorioConfirmacao;
            string strIdioma = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            rsReport.RenderCompleted += new wsReport.RenderCompletedEventHandler(GerarArquivoReport);

            rsReport.RenderAsync(relatorioPath, formato, null, DevInfo,
                PreecheParametros(dadosEmail.Localizador, strIdioma, "true"), null, null, dadosEmail);
        }

        private void GerarArquivoReport(object sender, wsReport.RenderCompletedEventArgs e)
        {
            var repositorioEmail = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IEmailRepositorio>();
            var parametroSpocRepositorio = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IParametroSpocRepositorio>();

            DadosEnvioEmailCeres dadosEmail = (DadosEnvioEmailCeres)e.UserState;

            string nomeArquivo = dadosEmail.Localizador + ".pdf";
            string remetente = Configuracoes.ParametroReport.RemetenteEmail;
            string urlEmailPadrao = Configuracoes.ParametroReport.CaminhoRelatorioConfirmacao;

            string assunto = string.Format("Informações sobre a Reserva {0}", dadosEmail.Localizador);

            string strBody = string.Empty;
            try { 
                using (System.Net.WebClient wc = new System.Net.WebClient())
                {
                    strBody = wc.DownloadString(string.Format(urlEmailPadrao, dadosEmail.Localizador, "pt-br", (dadosEmail.Alteracao ? "SIM" : "NAO")));
                }
            if ((dadosEmail.EnviarEmail &&
                !string.IsNullOrEmpty(dadosEmail.DestinatarioEmail.Trim())) || !string.IsNullOrEmpty(dadosEmail.EmailRequisitante.Trim()))
            {
                string[] destinatario = new string[] { !string.IsNullOrEmpty(dadosEmail.DestinatarioEmail.Trim()) ? dadosEmail.DestinatarioEmail : dadosEmail.EmailRequisitante };
                repositorioEmail.EnviarEmailComAnexo(remetente, destinatario, null, assunto, strBody, e.Result, nomeArquivo, parametroSpocRepositorio.ObterParametro(Configuracoes.ParametrosSpoc.ChaveUtilizacaoBuildBlockEmail).Valor);

            }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private wsReport.ParameterValue[] PreecheParametros(string CodReserva, string CulturaIdioma, string Alteracao)
        {
            wsReport.ParameterValue[] parametros = new wsReport.ParameterValue[4];

            wsReport.ParameterValue paramCodReserva = new wsReport.ParameterValue();
            wsReport.ParameterValue paramCulturaIdioma = new wsReport.ParameterValue();
            wsReport.ParameterValue paramAlteracao = new wsReport.ParameterValue();
            wsReport.ParameterValue paramBuscaBeneficios = new wsReport.ParameterValue();

            paramCodReserva.Name = "PrmCodReserva";
            paramCulturaIdioma.Name = "CodigoPais";
            paramAlteracao.Name = "AlteracaoReserva";
            paramBuscaBeneficios.Name = "BuscaBeneficios";

            paramCodReserva.Value = CodReserva;
            paramCulturaIdioma.Value = CulturaIdioma;
            paramAlteracao.Value = Alteracao;
            paramBuscaBeneficios.Value = "true";

            parametros[0] = paramCodReserva;
            parametros[1] = paramCulturaIdioma;
            parametros[2] = paramAlteracao;
            parametros[3] = paramBuscaBeneficios;

            return parametros;
        }
        #endregion
    }
}
