using AL.Atendimento.SobConsulta.Base.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Entidades
{
    public class ItemParametro : IEntidade
    {
        public virtual string CodigoParametro { get; set; }
        public virtual int NumeroSequencia { get; set; }
        public virtual double? NumeroDecimal { get; set; }
        public virtual string DescricaoAlfanumerica { get; set; }
        public virtual DateTime? DataParametro { get; set; }
        public virtual bool? Ativo { get; set; }

        public IChaveEntidade ObterChave()
        {
            return new ChaveEntidadeString(CodigoParametro + NumeroSequencia);
        }
    }
}
