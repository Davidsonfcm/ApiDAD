using AL.Atendimento.SobConsulta.Util.Enumeracoes;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.SobConsulta
{
    public class MotivoCancelamentoDto : DtoBase<Atendimento.SobConsulta.Entidades.MotivoCancelamento, MotivoCancelamentoDto>
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
    }
}
