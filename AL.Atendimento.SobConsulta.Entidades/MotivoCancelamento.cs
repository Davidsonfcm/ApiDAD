using System;
using AL.Atendimento.SobConsulta.Base.Entidades;
using AL.Atendimento.SobConsulta.Util.Enumeracoes;
using System.Collections.Generic;

namespace AL.Atendimento.SobConsulta.Entidades
{
    public class MotivoCancelamento : IEntidade
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }


        public IChaveEntidade ObterChave()
        {
            return new ChaveEntidadeString(Codigo);
        }

    }
}
