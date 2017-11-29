using AL.Atendimento.SobConsulta.Entidades;
using System;
using System.Collections.Generic;

namespace AL.Atendimento.SobConsulta.Fronteiras.Repositorios
{
    public interface IMotivoCancelamentoRepositorio
    {
        List<MotivoCancelamento> ListarMotivos();
    }
}
