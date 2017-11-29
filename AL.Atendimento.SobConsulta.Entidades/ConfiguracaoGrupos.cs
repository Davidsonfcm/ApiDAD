using AL.Atendimento.SobConsulta.Base.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Entidades
{
    public class ConfiguracaoGrupos : IEntidade
    {
        public string CodigoGrupo { get; set; }
        public int Ordenacao { get; set; }
        public List<string> ListaUpgrade { get; set; }
        public double MinimoProdutivo { get; set; }
        public double MaximoProdutivo { get; set; }
        public virtual bool Ativo { get; set; }
        public virtual String CodigoUsuario { get; set; }
        

        public IChaveEntidade ObterChave()
        {
            return new ChaveEntidadeString(CodigoGrupo);
        }
    }
}
