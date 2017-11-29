using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using AL.Atendimento.SobConsulta.Repositorios.EmailBuildBlock;
using Localiza.SDK.InversaoControle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Repositorios.Email
{
    public class EmailRepositorio : IEmailRepositorio
    {
        public void EnviarEmail(string remetente, string[] destinatarios, string[] destinatariosCopia, string assunto, string mensagem, string chaveUtilizacaoBuildBlock)
        {
            EnviarEmailInterno(remetente, destinatarios, destinatariosCopia, assunto, mensagem, null, chaveUtilizacaoBuildBlock);
        }

        public void EnviarEmailComAnexo(string remetente, string[] destinatarios, string[] destinatariosCopia, string assunto,
            string mensagem, byte[] conteudoAnexo, string nomeAnexo, string chaveUtilizacaoBuildBlock)
        {
            Anexo anexo = new Anexo() { conteudo = conteudoAnexo, nome = nomeAnexo };
            EnviarEmailInterno(remetente, destinatarios, destinatariosCopia, assunto, mensagem, new Anexo[] { anexo }, chaveUtilizacaoBuildBlock);
        }

        private void EnviarEmailInterno(string remetente, string[] destinatarios, string[] destinatariosCopia, string assunto,
            string mensagem, Anexo[] anexos, string chaveUtilizacaoBuildBlock)
        {

            string[] destinatario = destinatarios;
            string[] destinatarioCopia = destinatariosCopia;
            string corpo = mensagem;
            using (EmailClient wsEmail = new EmailClient())
            {
                wsEmail.EnviarEmail(
                    new Guid(chaveUtilizacaoBuildBlock),
                    remetente,
                    destinatario,
                    destinatarioCopia,
                    null,
                    assunto,
                    corpo,
                    anexos,
                    TipoCorpo.Html,
                    null);
            }
        }
    }
}
