using System;
using System.Collections.Generic;
using AL.Atendimento.SobConsulta.Entidades.Parametros;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;

namespace AL.Atendimento.SobConsulta.MapeamentosMock
{
    public class ParametroSpocRepositorioMock : IParametroSpocRepositorio
    {
        public ParametroSpoc ObterParametro(string nomeParametro)
        {
            ParametroSpoc parametro = new ParametroSpoc()
            {
                Valor = "42"
            };
            return parametro;
        }

        public List<ParametroSpoc> ObterParametros(string nomeParametro)
        {
            List<ParametroSpoc> parametros = new List<ParametroSpoc>();

            ParametroSpoc parametro = new ParametroSpoc()
            {
                Valor = "42"
            };
            parametros.Add(parametro);
            parametros.Add(parametro);
            parametros.Add(parametro);
            parametros.Add(parametro);

            return parametros;
        }
    }
}