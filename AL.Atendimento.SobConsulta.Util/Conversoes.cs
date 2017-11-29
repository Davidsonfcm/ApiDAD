using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Util
{
    public static class Conversoes
    {
        public static decimal ConverterValor(double valor)
        {
            return Convert.ToDecimal(Math.Round(valor, 2));
        }

        public static double ConverterValor(decimal valor)
        {
            return Convert.ToDouble(valor);
        }
    }
}
