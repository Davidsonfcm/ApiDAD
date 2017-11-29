using System;

namespace AL.Atendimento.SobConsulta.Util.Excecoes
{
    public class ParametroNaoEncontradoException : Exception
    {
        public string Parametro { get; set; }
        public string Valor { get; set; }
        public string Codigo { get; set; }

        public ParametroNaoEncontradoException(string mensagem, string parametro, string valor, string codigo) : base(mensagem)
        {
            Parametro = parametro;
            Valor = valor;
            Codigo = codigo;
        }
    }
}
