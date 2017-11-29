using AL.Atendimento.SobConsulta.Base.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AL.Atendimento.SobConsulta.Base.Repositorios
{
    public class RepositorioBase<TEntidade> : AdoHelper where TEntidade : IEntidade
    {
        //Dummy criado para copiar a dll quando compilar
        protected Sybase.Data.AseClient.AseBulkCopyOptions dummy = Sybase.Data.AseClient.AseBulkCopyOptions.Default;

        public RepositorioBase(ConexaoBanco.TipoConexao conexao) : base(conexao)
        {
        }

        protected TEntidade Obter(String consulta, Object parametros)
        {
            using (var conexao = ObterConexao())
            {
                return conexao.Query<TEntidade>(FormatadorConsulta.Formatar(consulta, parametros), parametros).FirstOrDefault();
            }
        }


        protected IEnumerable<TEntidade> Listar(String consulta, Object parametros, bool bufferizado = true, int? timeoutComando = null)
        {
            using (var conexao = ObterConexao())
            {
                return conexao.Query<TEntidade>(FormatadorConsulta.Formatar(consulta, parametros), parametros, bufferizado, timeoutComando);
            }
        }

        protected T Obter<T>(String consulta, Object parametros)
        {
            using (var conexao = ObterConexao())
            {
                return conexao.Query<T>(FormatadorConsulta.Formatar(consulta, parametros), parametros).FirstOrDefault();
            }
        }

        protected T Obter<T>(String consulta, Object parametros, ConexaoBanco.TipoConexao tipoConexao)
        {
            using (var conexao = ObterConexao(tipoConexao))
            {
                return conexao.Query<T>(FormatadorConsulta.Formatar(consulta, parametros), parametros).FirstOrDefault();
            }
        }

        protected IEnumerable<T> Listar<T>(String consulta, Object parametros, bool bufferizado = true, int? timeoutComando = null)
        {
            using (var conexao = ObterConexao())
            {
                return conexao.Query<T>(FormatadorConsulta.Formatar(consulta, parametros), parametros, bufferizado, timeoutComando);
            }
        }


        protected IEnumerable<T> Listar<T>(String consulta, Object parametros, ConexaoBanco.TipoConexao tipoConexao, bool bufferizado = true, int? timeoutComando = null)
        {
            using (var conexao = ObterConexao(tipoConexao))
            {
                return conexao.Query<T>(FormatadorConsulta.Formatar(consulta, parametros), parametros, bufferizado, timeoutComando);
            }
        }

        protected IEnumerable<TPai> QueryComListaFilho<TPai, TFilho>(String consulta, Object parametros, String[] colunasDemarcacao, Func<TPai, TFilho, TPai> funcaoMapeamento)
            where TPai : IEntidade
        {
            Dictionary<IChaveEntidade, TPai> pais = new Dictionary<IChaveEntidade, TPai>();
            using (var conexao = ObterConexao())
            {
                conexao.Query<TPai, TFilho>(FormatadorConsulta.Formatar(consulta, parametros), (pai, filho) =>
                {
                    var pai2 = VerificarEntidade<TPai>(pai, pais);

                    funcaoMapeamento(pai2, filho);

                    return pai2;
                }, parametros, ObterSplitOn(colunasDemarcacao, 1));
            }

            return pais.Values;
        }

        public IEnumerable<TPai> QueryComListaFilho1E2<TPai, TPrimeiroFilho, TSegundoFilho>(String consulta, Object parametros, String[] colunasDemarcacao, Func<TPai, TPrimeiroFilho, TSegundoFilho, TPai> funcaoMapeamento)
            where TPai : IEntidade
            where TPrimeiroFilho : IEntidade
        {
            Dictionary<IChaveEntidade, TPai> pais = new Dictionary<IChaveEntidade, TPai>();
            Dictionary<IChaveEntidade, TPrimeiroFilho> filhos = new Dictionary<IChaveEntidade, TPrimeiroFilho>();
            using (var conexao = ObterConexao())
            {
                conexao.Query<TPai, TPrimeiroFilho, TSegundoFilho>(consulta, (pai, filho, segundoFilho) =>
                {
                    var pai2 = VerificarEntidade<TPai>(pai, pais);
                    var filho2 = VerificarEntidade<TPrimeiroFilho>(filho, filhos);

                    funcaoMapeamento(pai2, filho2, segundoFilho);

                    return pai2;
                }, parametros, ObterSplitOn(colunasDemarcacao, 2));
            }

            return pais.Values;
        }

        public IEnumerable<TPai> QueryComListaFilho1E2E3<TPai, TPrimeiroFilho, TSegundoFilho, TTerceiroFilho>(String consulta, Object parametros, String[] colunasDemarcacao, Func<TPai, TPrimeiroFilho, TSegundoFilho, TTerceiroFilho, TPai> funcaoMapeamento)
            where TPai : IEntidade
            where TPrimeiroFilho : IEntidade
            where TSegundoFilho : IEntidade
        {
            Dictionary<IChaveEntidade, TPai> pais = new Dictionary<IChaveEntidade, TPai>();
            Dictionary<IChaveEntidade, TPrimeiroFilho> filhos = new Dictionary<IChaveEntidade, TPrimeiroFilho>();
            Dictionary<IChaveEntidade, TSegundoFilho> segundosFilhos = new Dictionary<IChaveEntidade, TSegundoFilho>();
            using (var conexao = ObterConexao())
            {
                conexao.Query<TPai, TPrimeiroFilho, TSegundoFilho, TTerceiroFilho>(consulta, (pai, filho, segundoFilho, terceiroFilho) =>
                {
                    var pai2 = VerificarEntidade<TPai>(pai, pais);
                    var filho2 = VerificarEntidade<TPrimeiroFilho>(filho, filhos);
                    var segundoFilho2 = VerificarEntidade<TSegundoFilho>(segundoFilho, segundosFilhos);

                    funcaoMapeamento(pai2, filho2, segundoFilho2, terceiroFilho);

                    return pai2;
                }, parametros, ObterSplitOn(colunasDemarcacao, 3));
            }

            return pais.Values;
        }

        public IEnumerable<TPai> QueryComListaFilho1E2E3E4<TPai, TPrimeiroFilho, TSegundoFilho, TTerceiroFilho, TQuartoFilho>(String consulta, Object parametros, String[] colunasDemarcacao,
            Func<TPai, TPrimeiroFilho, TSegundoFilho, TTerceiroFilho, TQuartoFilho, TPai> funcaoMapeamento)
            where TPai : IEntidade
            where TPrimeiroFilho : IEntidade
            where TSegundoFilho : IEntidade
            where TTerceiroFilho : IEntidade
        {
            Dictionary<IChaveEntidade, TPai> pais = new Dictionary<IChaveEntidade, TPai>();
            Dictionary<IChaveEntidade, TPrimeiroFilho> filhos = new Dictionary<IChaveEntidade, TPrimeiroFilho>();
            Dictionary<IChaveEntidade, TSegundoFilho> segundosFilhos = new Dictionary<IChaveEntidade, TSegundoFilho>();
            Dictionary<IChaveEntidade, TTerceiroFilho> terceirosFilhos = new Dictionary<IChaveEntidade, TTerceiroFilho>();
            using (var conexao = ObterConexao())
            {
                conexao.Query<TPai, TPrimeiroFilho, TSegundoFilho, TTerceiroFilho, TQuartoFilho>(consulta, (pai, filho, segundoFilho, terceiroFilho, quartoFilho) =>
                {
                    var pai2 = VerificarEntidade<TPai>(pai, pais);
                    var filho2 = VerificarEntidade<TPrimeiroFilho>(filho, filhos);
                    var segundoFilho2 = VerificarEntidade<TSegundoFilho>(segundoFilho, segundosFilhos);
                    var terceiroFilho2 = VerificarEntidade<TTerceiroFilho>(terceiroFilho, terceirosFilhos);

                    funcaoMapeamento(pai2, filho2, segundoFilho2, terceiroFilho2, quartoFilho);

                    return pai2;
                }, parametros, ObterSplitOn(colunasDemarcacao, 4));
            }

            return pais.Values;
        }

        private T VerificarEntidade<T>(T entidade, Dictionary<IChaveEntidade, T> mapeamento) where T : IEntidade
        {
            if (!object.Equals(entidade, default(T)) && entidade.ObterChave() != null && entidade.ObterChave().TemValor())
            {
                IChaveEntidade chave = entidade.ObterChave();
                if (!mapeamento.Any(item => item.Key.Equals(chave)))
                {
                    mapeamento[chave] = entidade;
                }

                return mapeamento[chave];
            }

            return entidade;
        }

        private String ObterSplitOn(IEnumerable<String> colunasDemarcacao, int quantidadeTipos)
        {
            String splitOn = null;
            if (colunasDemarcacao != null && colunasDemarcacao.Any())
            {
                if (colunasDemarcacao.Count() == quantidadeTipos
                    && colunasDemarcacao.All(demarcacao => !String.IsNullOrWhiteSpace(demarcacao) && !demarcacao.Contains(",")))
                {
                    splitOn = String.Join(",", colunasDemarcacao);
                }
                else
                {
                    throw new ArgumentException("Colunas de demarcação não foram definidas para todas as entidades, ou alguma delas contém vírgula (',').");
                }
            }

            return splitOn;
        }

        protected string MontaCondicoes(IList<string> condicoes, bool primeiraCondicao)
        {
            if (condicoes.Count == 0)
                return string.Empty;
            string clausulaInicio = primeiraCondicao ? "WHERE" : "AND";
            string sql = $" {clausulaInicio} {condicoes[0]}";
            for (int i = 1; i < condicoes.Count; i++)
            {
                sql = $"{sql} AND {condicoes[i]}";
            }
            return sql;
        }

        protected virtual int ObterIdentity()
        {
            return Obter<int>("select @@identity", null);
        }

        protected virtual int ObterProximoSequencial(string nomeSequencia)
        {
            string sql = "begin tran " +
                 "update tbl_sequence set num_sequence = num_sequence + 1 where nom_sequence = '" + nomeSequencia + "' and nom_controle = '" + nomeSequencia + "' " +
                 "select (num_sequence) from tbl_sequence where nom_sequence = '" + nomeSequencia + "' and nom_controle = '" + nomeSequencia + "' " +
                 "commit tran ";

            return Obter<int>(sql, null);
        }

    }
}