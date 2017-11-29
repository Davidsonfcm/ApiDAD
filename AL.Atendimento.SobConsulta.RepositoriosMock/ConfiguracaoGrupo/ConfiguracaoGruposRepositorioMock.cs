using AL.Atendimento.SobConsulta.Base.Repositorios;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using AL.Atendimento.SobConsulta.Repositorios.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Localiza.SDK.InversaoControle;
using Localiza.SDK.Cache;
using AL.Atendimento.SobConsulta.Entidades;
using Dapper;

namespace AL.Atendimento.SobConsulta.Repositorios.RegraSaldo
{
    public class ConfiguracaoGruposRepositorioMock : RepositorioBase<ConfiguracaoGrupos>, IConfiguracaoGruposRepositorio
    {

        private const string SQL_SELECT_JOIN = @"
        SELECT 
            og.rate_group as CodigoGrupo, 
            og.num_ordena as Ordenacao,
            og.vlr_faixa_minima as MinimoProdutivo,
		    og.vlr_faixa_maxima as MaximoProdutivo,
            cast(isnull(og.idc_status, 0) as bit) Ativo,
            u.rate_group_upgrade as Upgrade
        FROM res_sobconsulta_ordena_grupo og
            LEFT JOIN res_sobconsulta_upgrade u
            ON u.rate_group_ordena = og.rate_group";

        private const string SQL_INSERT_ORDENACAO = @"INSERT INTO res_sobconsulta_ordena_grupo 
                (rate_group, num_ordena, idc_status, dt_ultima_alteracao, cd_responsavel_alteracao, vlr_faixa_minima, vlr_faixa_maxima)
                VALUES (@codigoGrupo, @ordenacao, @ativo, getdate(), @codigoUsuario, @minimo, @maximo)";

        private const string SQL_INSERT_UPGRADE = @"INSERT INTO 
                res_sobconsulta_upgrade (rate_group_ordena, rate_group_upgrade, dt_ultima_alteracao, cd_responsavel_alteracao)
                VALUES (@codigoGrupo, @codigoGrupoUpgrade, getdate(), @codigoUsuario)";

        private const string SQL_DELETE_UPGRADE = @"
                DELETE FROM res_sobconsulta_upgrade
                WHERE rate_group_ordena = @codigoGrupo";

        private const string SQL_UPDATE_ORDENACAO = @"UPDATE dbo.res_sobconsulta_ordena_grupo
                            SET num_ordena = @ordenacao,
	                            idc_status = @ativo,
	                            dt_ultima_alteracao = getdate(),
	                            cd_responsavel_alteracao = @codigoUsuario,
	                            vlr_faixa_minima = @minimo,
	                            vlr_faixa_maxima = @maximo
                            WHERE rate_group = @codigoGrupo";

        private static readonly ICache<List<ConfiguracaoGrupos>> cacheOrdenacao = FabricaCache.BaseadoConfiguracao<List<ConfiguracaoGrupos>>("cacheProvider", "cacheOrdenacaoGrupos");

        public ConfiguracaoGruposRepositorioMock() : base(ConexaoBanco.TipoConexao.Aguia)
        {
        }

        protected virtual List<ConfiguracaoGrupos> ObterInterno(string chave)
        {
            var grupos = QueryComListaFilho<ConfiguracaoGrupos, string>(SQL_SELECT_JOIN, null, new string[] { "Upgrade" }, Func);
            return grupos.ToList();
        }
        public ConfiguracaoGrupos ObterConfiguracaoGrupo(string codigoGrupo)
        {
            var grupo = ListarConfiguracoesGrupo().Where(x => x.CodigoGrupo == codigoGrupo);
            if (grupo == null || grupo.ToList().Count == 0)
            {
                return null;
            }

            return grupo.FirstOrDefault();
        }
        public ConfiguracaoGrupos Func(ConfiguracaoGrupos grupo, string upgrade)
        {
            if (grupo.ListaUpgrade == null)
                grupo.ListaUpgrade = new List<string>();

            if (!String.IsNullOrEmpty(upgrade))
                grupo.ListaUpgrade.Add(upgrade);

            return grupo;
        }
        public List<ConfiguracaoGrupos> ListarConfiguracoesGrupo()
        {
            var tempo = TimeSpan.FromSeconds(double.Parse(ObterTempoCache()));

            return cacheOrdenacao.Obter("chaveObter", ObterInterno, tempo);
        }

        private string ObterTempoCache()
        {
            var repositorio = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IParametroSpocRepositorio>();
            var resultado = repositorio.ObterParametro(Util.Configuracoes.ParametrosSpoc.ChaveCacheOrdenacaoGrupos);
            return resultado.Valor;
        }

        public void Alterar(ConfiguracaoGrupos configuracao)
        {
            RemoverConfiguracoesDeUpgrade(configuracao);

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@ordenacao", configuracao.Ordenacao, System.Data.DbType.Int32);
            parametros.Add("@ativo", configuracao.Ativo, System.Data.DbType.Int32);
            parametros.Add("@codigoUsuario", configuracao.CodigoUsuario, System.Data.DbType.AnsiString);
            parametros.Add("@minimo", configuracao.MinimoProdutivo, System.Data.DbType.Decimal);
            parametros.Add("@maximo", configuracao.MaximoProdutivo, System.Data.DbType.Decimal);
            parametros.Add("@codigoGrupo", configuracao.CodigoGrupo, System.Data.DbType.AnsiString);

            Executar(SQL_UPDATE_ORDENACAO, parametros);

            InsereConfiguracoesDeUpgrade(configuracao);
        }
        private void RemoverConfiguracoesDeUpgrade(ConfiguracaoGrupos configuracao)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@codigoGrupo", configuracao.CodigoGrupo, System.Data.DbType.AnsiString);

            Executar(SQL_DELETE_UPGRADE, parametros);
        }

        public void Gravar(ConfiguracaoGrupos configuracao)
        {
            DynamicParameters parametros = new DynamicParameters();

            parametros.Add("@codigoGrupo", configuracao.CodigoGrupo, System.Data.DbType.AnsiString);
            parametros.Add("@ordenacao", configuracao.Ordenacao, System.Data.DbType.Int32);
            parametros.Add("@ativo", configuracao.Ativo, System.Data.DbType.Binary);
            parametros.Add("@codigoUsuario", configuracao.CodigoUsuario, System.Data.DbType.AnsiString);
            parametros.Add("@minimo", configuracao.MinimoProdutivo, System.Data.DbType.Decimal);
            parametros.Add("@maximo", configuracao.MaximoProdutivo, System.Data.DbType.Decimal);

            Executar(SQL_INSERT_ORDENACAO, parametros);

            InsereConfiguracoesDeUpgrade(configuracao);
        }
        private void InsereConfiguracoesDeUpgrade(ConfiguracaoGrupos configuracao)
        {
            foreach (string codigoGrupoUpgrade in configuracao.ListaUpgrade)
            {
                if (!string.IsNullOrEmpty(codigoGrupoUpgrade))
                {
                    DynamicParameters parametros = new DynamicParameters();

                    parametros.Add("@codigoGrupo", configuracao.CodigoGrupo, System.Data.DbType.AnsiString);
                    parametros.Add("@codigoGrupoUpgrade", codigoGrupoUpgrade, System.Data.DbType.AnsiString);
                    parametros.Add("@codigoUsuario", configuracao.CodigoUsuario, System.Data.DbType.AnsiString);
                    Executar(SQL_INSERT_UPGRADE, parametros);
                }
            }
        }
    }
}
