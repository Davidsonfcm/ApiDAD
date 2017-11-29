using Localiza.SDK.AcessoDados;
using System;

namespace AL.Atendimento.SobConsulta.Base.Repositorios
{
    public class ConexaoBanco
    {
        public enum TipoConexao
        {
            Aguia
        }

        public static DbAccessHelper ObterConexao(TipoConexao conexao)
        {
            return ConnectionFactory.Instance.GetConnection(Enum.GetName(typeof(TipoConexao), conexao));
        }
    }
}
