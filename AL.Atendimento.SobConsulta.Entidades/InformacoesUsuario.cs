using AL.Atendimento.SobConsulta.Base.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Entidades
{
    public class InformacoesUsuario : IEntidade
    {
        public string NomeUsuario { get; set; }
        public string CodigoUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public IChaveEntidade ObterChave()
        {
            throw new NotImplementedException();
        }
    }
}
