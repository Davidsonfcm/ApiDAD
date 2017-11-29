using AL.Atendimento.SobConsulta.Entidades;
using System;

namespace AL.Atendimento.SobConsulta.Fronteiras.Repositorios
{
    public interface IEmailRepositorio
    {
        void EnviarEmail(string remetente, string[] destinatarios, string[] destinatariosCopia, string assunto, string mensagem, string chaveUtilizacaoBuildBlock);
        void EnviarEmailComAnexo(string remetente, string[] destinatarios, string[] destinatariosCopia, string assunto,
            string mensagem, byte[] conteudoAnexo, string nomeAnexo, string chaveUtilizacaoBuildBlock);
    }
}
