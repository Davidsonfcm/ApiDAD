using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.RepositoriosMock.Parametros
{
    public class ItensParametrosRepositorioMock : IItensParametrosRepositorio
    {
        
        public string[] ObterListaParametroOrigemParceiros()
        {
            List<string> parametrosBase = new List<string>();
            parametrosBase.Add("GO");
            parametrosBase.Add("TN");

            return parametrosBase.ToArray();
        }

        public bool VerificarSeListaParametrosContemValor(string codigoParametro, string tipoOrigem)
        {
            return false;
        }
    }
}
