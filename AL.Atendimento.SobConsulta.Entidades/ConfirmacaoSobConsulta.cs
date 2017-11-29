using AL.Atendimento.SobConsulta.Base.Entidades;
using AL.Atendimento.SobConsulta.Util.Enumeracoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Entidades
{
    public class ConfirmacaoSobConsulta : IEntidade
    {
        public String Localizador { get; set; }
        public MotivoConfirmacaoSobConsulta MotivoConfirmacao { get; set; }
        public double? QtdFrotaOriginal { get; set; }
        public double? QtdFrotaProcessada { get; set; }
        public double? QtdFrotaAlugadaOriginal { get; set; }
        public double? QtdFrotaAlugadaProcessada { get; set; }
        public String GrupoConfirmacao { get; set; }
        public int? QtdProcessamentos { get; set; }
        public int? IdProcessamentoAutomatico { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public String CodigoUsuarioAlteracao { get; set; }

        public IChaveEntidade ObterChave()
        {
            throw new NotImplementedException();
        }
    }
}
