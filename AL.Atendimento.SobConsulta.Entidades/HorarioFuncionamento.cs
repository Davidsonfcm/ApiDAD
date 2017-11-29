﻿using AL.Atendimento.SobConsulta.Util.Enumeracoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Entidades
{
    public class HorarioFuncionamento
    {
        public DayOfWeek? DiaDaSemanaInternacionalField { get; set; }
        public DiaReferenciaComercial DiaDaSemana { get; set; }
        public DayOfWeek? DiaDaSemanaInternacional { get; set; }
        public DateTime HorarioAbertura { get; set; }
        public DateTime HorarioFechamento { get; set; }
    }
}
