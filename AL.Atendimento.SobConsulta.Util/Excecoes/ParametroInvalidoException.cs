using System;

namespace AL.Atendimento.SobConsulta.Util.Excecoes
{
    public class ParametroInvalidoException : Exception
    {
        public string Parametro { get; set; }
        public string Valor { get; set; }
        public string Codigo { get; set; }

        public ParametroInvalidoException(string mensagem, string parametro, string valor, string codigo) : base(mensagem)
        {
            Parametro = parametro;
            Valor = valor;
            Codigo = codigo;
        }
    }
}
