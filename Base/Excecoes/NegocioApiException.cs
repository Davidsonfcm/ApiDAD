using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Base.Excecoes
{
    public class NegocioApiException: Exception
    {
        public string Codigo { get; set; }
        public object ObjetoEnviado { get; set; }

        public NegocioApiException(string mensagem, string codigo, object objetoEnviado) : base(mensagem)
        {
            Codigo = codigo;
            ObjetoEnviado = objetoEnviado;
        }
        public NegocioApiException(string mensagem, string codigo, Exception innerException, object objetoEnviado) : base(mensagem, innerException)
        {
            Codigo = codigo;
            ObjetoEnviado = objetoEnviado;
        }

        public override string ToString()
        {
            StringBuilder mensagem = new StringBuilder();
            mensagem.AppendLine("");
            mensagem.AppendLine($"ObjetoOrigem: { (ObjetoEnviado == null ? "null" : ObjetoEnviado.ToString()) }");

            return base.ToString() + mensagem;
        }
    }
}
