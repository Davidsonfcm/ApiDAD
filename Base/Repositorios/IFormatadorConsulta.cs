namespace AL.Atendimento.SobConsulta.Base.Repositorios
{
    public interface IFormatadorConsulta
    {
        string Formatar(string sql, object parametros);
    }
}
