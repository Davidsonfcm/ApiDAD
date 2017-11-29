using AL.Atendimento.SobConsulta.Entidades;
using System;
using System.Collections.Generic;

namespace AL.Atendimento.SobConsulta.Fronteiras.Repositorios
{
    public interface IReservasProcessadasRepositorio
    {
        List<ReservasProcessadas> Obter(string localizador, string dataInicio, string dataFim);
    }
}
