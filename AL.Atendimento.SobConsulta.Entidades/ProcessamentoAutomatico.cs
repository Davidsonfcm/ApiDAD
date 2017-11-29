using AL.Atendimento.SobConsulta.Base.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Entidades
{
    public class ProcessamentoAutomatico : IEntidade
    {
        public int ID { get; set; }
        public DateTime InicioProcessamento { get; set; }
        public DateTime FimProcessamento { get; set; }
        public int QtdRegistrosProcessados { get; set; }
        public int QtdRegistrosConfirmados { get; set; }
        public int QtdRegistrosNegados { get; set; }
        public DateTime UltimoProcessoBI { get; set; }

        public IChaveEntidade ObterChave()
        {
            throw new NotImplementedException();
        }
    }
}
