using AL.Atendimento.SobConsulta.Fronteiras.Executores.Email;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using AL.Atendimento.SobConsulta.Util;
using Localiza.SDK.Fronteira;
using System;
using System.Linq;
using System.Text;

namespace AL.Atendimento.SobConsulta.Executores.Email
{
    public class EnviarEmailErroExecutor : IExecutorSemResultado<EnviarEmailErroRequisicao>
    {
        private readonly IEmailRepositorio emailRepositorio;
        private readonly IParametroSpocRepositorio parametroSpocRepositorio;

        public EnviarEmailErroExecutor(IEmailRepositorio emailRepositorio, IParametroSpocRepositorio parametroSpocRepositorio)
        {
            this.emailRepositorio = emailRepositorio;
            this.parametroSpocRepositorio = parametroSpocRepositorio;
        }

        public void Executar(EnviarEmailErroRequisicao requisicao)
        {
            emailRepositorio.EnviarEmail(ObterRemetenteEnvioEmail(), ObterDestinatariosEnvioEmail(),
                null, "[API SobConsulta] Comportamento Inesperado", ConstruirCorpoEmailErro(requisicao), ObterChaveUtilizacaoBuildBlockEmail());
           
        }

        private string ObterRemetenteEnvioEmail()
        {
            return parametroSpocRepositorio.ObterParametro(Configuracoes.ParametrosSpoc.RemetenteEmailErro).Valor;
        }

        private string ObterChaveUtilizacaoBuildBlockEmail()
        {
            return parametroSpocRepositorio.ObterParametro(Configuracoes.ParametrosSpoc.ChaveUtilizacaoBuildBlockEmail).Valor;
        }

        private string[] ObterDestinatariosEnvioEmail()
        {
            return parametroSpocRepositorio.ObterParametros(Configuracoes.ParametrosSpoc.DestinatariosEmailErro).Select(item => item.Valor).ToArray();
        }

        private string ConstruirCorpoEmailErro(EnviarEmailErroRequisicao requisicao)
        {
            if (requisicao.Erro == null)
                return String.Empty;

            StringBuilder retorno = new StringBuilder();

            retorno.AppendLine($"<b>Data:</b> {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");

            retorno.AppendLine($"<br><br><b>Mensagem:</b> {requisicao.Erro.Message}");

            retorno.AppendLine($"<br><br><b>Parametros:</b> {requisicao.Parametros}");

            if (requisicao.Erro.InnerException != null)
                retorno.AppendLine($"<br><br><b>Mensagem Interna:</b> {requisicao.Erro.InnerException.Message}");

            retorno.AppendLine($"<br><br><b>StackTrace:</b> {requisicao.Erro.StackTrace.ToString()}");

            return retorno.ToString();
        }
    }
}
