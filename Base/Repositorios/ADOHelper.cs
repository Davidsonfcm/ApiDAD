using Localiza.SDK.AcessoDados;
using System;

namespace AL.Atendimento.SobConsulta.Base.Repositorios
{
    public class AdoHelper
    {
        private readonly ConexaoBanco.TipoConexao conexao;
        private static IFormatadorConsulta _formatadorConsulta = new FormatadorConsulta();

        public IFormatadorConsulta FormatadorConsulta { get { return _formatadorConsulta; } }

        public static void RegistrarFormatadorConsulta(IFormatadorConsulta formatadorConsulta)
        {
            _formatadorConsulta = formatadorConsulta;
        }

        protected static string Formatar(string sql, object parametros)
        {
            return _formatadorConsulta.Formatar(sql, parametros);
        }

        public AdoHelper(ConexaoBanco.TipoConexao conexao)
        {
            this.conexao = conexao;
        }

        public DbAccessHelper ObterConexao()
        {
            return ObterConexao(conexao);
        }

        public static DbAccessHelper ObterConexao(ConexaoBanco.TipoConexao conexao)
        {
            return ConnectionFactory.Instance.GetConnection(Enum.GetName(typeof(ConexaoBanco.TipoConexao), conexao));
        }

        public int Executar(String sql, Object parametros)
        {
            using (DbAccessHelper conexao = ObterConexao())
            {
                return conexao.Execute(Formatar(sql, parametros), parametros);
            }
        }
    }
}
