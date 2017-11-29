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

namespace AL.Atendimento.SobConsulta.Repositorios.Parametros
{
    public class ItensParametrosRepositorio : RepositorioBase<ItemParametroEntidade>, IItensParametrosRepositorio
    {
        public ItensParametrosRepositorio() : base(ConexaoBanco.TipoConexao.Aguia)
        {
        }


        private static readonly string SQL_LISTAR = @"
            SELECT 
	             cod_parametro AS CodigoParametro,
	             num_seq_item AS NumeroSequencia,
	             num_decimal AS NumeroDecimal,
	             desc_alfanumerica AS DescricaoAlfanumerica,
	             data_parametro AS DataParametro,
	             idc_ativo AS Ativo
            FROM 
	            itens_parametros 
            WHERE 
	            cod_parametro = @cod_parametro";
        private static readonly ICache<IEnumerable<ItemParametro>> cacheItensParametros = FabricaCache.BaseadoConfiguracao<IEnumerable<ItemParametro>>("cacheProvider", "cacheItensParametros");

        protected virtual IEnumerable<ItemParametro> ListarParametro(string codigoParametro)
        {
            return cacheItensParametros.Obter(codigoParametro, InternoListarParametro, new TimeSpan(24, 0, 0));
        }

        protected IEnumerable<ItemParametro> InternoListarParametro(string codigoParametro)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@cod_parametro", codigoParametro, TipoParametro.StringComTamanhoVariavel);
            return Listar(SQL_LISTAR, parametros);
        }
        
        public string[] ObterListaParametroOrigemParceiros()
        {
            return ListarParametro("CR_ORIGEM_PARCEIRO").Select(item => item.DescricaoAlfanumerica).ToArray();
        }

        public bool VerificarSeListaParametrosContemValor(string codigoParametro, string tipoOrigem)
        {
            return (ListarParametro(codigoParametro).FirstOrDefault()?.DescricaoAlfanumerica == tipoOrigem);
        }
    }
}
