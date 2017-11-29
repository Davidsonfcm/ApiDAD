using System;

namespace AL.Atendimento.SobConsulta.Util.Excecoes
{
    public class NegocioException : Exception
    {
        public string Codigo { get; set; }

        public NegocioException(string mensagem, string codigo) : base(mensagem)
        {
            Codigo = codigo;
        }
        public NegocioException(string mensagem, string codigo, Exception innerException) : base(mensagem, innerException)
        {
            Codigo = codigo;
        }
    }
}
