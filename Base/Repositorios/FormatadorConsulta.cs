namespace AL.Atendimento.SobConsulta.Base.Repositorios
{
    public class FormatadorConsulta : IFormatadorConsulta
    {
        public string Formatar(string sql, object parametros)
        {
            return sql;
        }
    }
}
