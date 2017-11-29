using AL.Atendimento.SobConsulta.Base.Repositorios;
using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using AL.Atendimento.SobConsulta.Repositorios.Entidade;
using Dapper;
using Localiza.SDK.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AL.Atendimento.SobConsulta.Util.Extensoes;
using AL.Atendimento.SobConsulta.Entidades.Parametros;

namespace AL.Atendimento.SobConsulta.Repositorios.Parametros
{
    public class ParametroSpocRepositorio : IParametroSpocRepositorio
    {
        public List<ParametroSpoc> ObterParametros(string nomeParametro)
        {
            return ConverterParametroSpoc(ObterParametroSpoc(nomeParametro));
        }

        public ParametroSpoc ObterParametro(string nomeParametro)
        {
            return ObterParametros(nomeParametro).FirstOrDefault();
        }

        private List<ParametroSpoc> ConverterParametroSpoc(Localiza.Corporativo.GestaoParametros.Model.Parametro parametroSpoc)
        {
            List<ParametroSpoc> retorno = new List<ParametroSpoc>();

            foreach (var parametro in parametroSpoc.ListaValoresParametro)
            {
                ParametroSpoc item = new ParametroSpoc();
                item.Valor = parametro.Valor;

                retorno.Add(item);
            }
            return retorno;
        }

        private Localiza.Corporativo.GestaoParametros.Model.Parametro ObterParametroSpoc(string nomeParametro)
        {
            try
            {
                Localiza.Corporativo.GestaoParametros.Model.Parametro parametro = Localiza.Corporativo.GestaoParametros.GerenciadorParametros.RecuperaParametro(nomeParametro);
                
                if (parametro == null)
                {
                    throw new Exception($"Parâmetro {nomeParametro} não cadastrado no SPOC");
                }

                return parametro;
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao buscar o parâmetro {nomeParametro} no SPOC");
            }
        }
    }
}
