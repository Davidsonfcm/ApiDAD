using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Entidades.Parametros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Fronteiras.Repositorios
{
    public interface IParametroSpocRepositorio
    {
        ParametroSpoc ObterParametro(string nomeParametro);
        List<ParametroSpoc> ObterParametros(string nomeParametro);
    }
}
