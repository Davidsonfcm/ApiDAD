using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Util.Excecoes
{
    public class ParametroNuloException : Exception
    {
        public string Parametro { get; set; }

        public ParametroNuloException(string parametro)
        {
            Parametro = parametro;
        }
    }
}
