using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Util.Excecoes
{
    public class InterfacePagamentoException : Exception
    {
        public InterfacePagamentoException(string mensagem) : base(mensagem)
        { }

        public InterfacePagamentoException(Exception innerException) : base(innerException.Message, innerException)
        { }
    }
}
