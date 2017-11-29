using AL.Atendimento.SobConsulta.Base.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Entidades
{
    public class ReservasProcessadas : IEntidade
    {
        public string Localizador { get; set; }
        public DateTime DataAtendimento { get; set; }
        public int JustificativaConfirmacao { get; set; }
        public string Situacao { get; set; }
        public int CodigoMotivoCancelamento { get; set; }
        public string DescricaoMotivoCancelamento { get; set; }
        public int CodigoMotivoNaoConfirmacao { get; set; }
        public string DescricaoMotivoNaoConfirmacao { get; set; }
        public double SaldoOriginal { get; set; }
        public double SaldoProcessado { get; set; }
        public double AlugadoOriginal { get; set; }
        public double AlugadoProcessado { get; set; }
        public string GrupoProcessado { get; set; }
        public string Responsavel { get; set; }
        public DateTime DataCriacaoReserva { get; set; }

        public IChaveEntidade ObterChave()
        {
            throw new NotImplementedException();
        }
    }
}
