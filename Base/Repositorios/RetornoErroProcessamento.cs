using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Al.GestaoDeContratos.Core.Base.Repositorios
{
    public class RetornoErroProcessamento
    {
        public Erro[] Erros { get; set; }
        public bool ErroNegocio { get; set; }
        public string Mensagem
        {
            get
            {
                if (Erros == null)
                    return null;
                return string.Join(" - ", Erros.Select(erro => erro.Mensagem));
            }
        }

        public string CodigoErro
        {
            get
            {
                if (Erros == null)
                    return null;
                return Erros.FirstOrDefault()?.CodigoErro;
            }
        }


        public class Erro
        {
            public string Mensagem { get; set; }
            public string Parametro { get; set; }
            public string Valor { get; set; }
            public string CodigoErro { get; set; }
        }
    }
}
