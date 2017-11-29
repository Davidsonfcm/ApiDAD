using AL.Atendimento.SobConsulta.Base.Repositorios;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AL.Atendimento.SobConsulta.Testes
{
    public class FormatadorConsultaUltraLite : IFormatadorConsulta
    {
        public string Formatar(string sql, object parametros)
        {
            string retorno = sql;
            retorno = TratarSubstituicoesQueryTesteUnitario(retorno);
            if (parametros != null)
            {
                foreach (var parametro in ((Dapper.DynamicParameters)parametros).ParameterNames.ToList())
                {
                    StringBuilder valores = new StringBuilder("?");

                    var value = ((Dapper.SqlMapper.IParameterLookup)parametros)[parametro];
                    if (value is System.Collections.ICollection)
                    {
                        valores = valores.Insert(0, "(");
                        for (var i = 0; i < ((System.Collections.ICollection)value).Count - 1; i++)
                        {
                            valores.Append(", ?");
                        }
                        valores.Append(")");
                    }

                    retorno = Regex.Replace(retorno, string.Format(@"\b{0}\b", "Regex" + parametro), valores.ToString(),
                        RegexOptions.IgnoreCase);
                }
            }
            else if (retorno.ToLowerInvariant().Contains("exec "))
            {
                retorno = "SELECT 1";
            }
            return retorno;
        }

        private static string TratarSubstituicoesQueryTesteUnitario(string query)
        {
            string retorno = query.Replace("@", "Regex").Replace("..", ".").Replace("smalldatetime", "timestamp");
            if (retorno.Contains("@@IDENTITY"))
            {
                retorno = retorno.Replace("SELECT @@IDENTITY AS Id", " GO \r\n SELECT 1 GO\r\n");
            }
            if (retorno.Contains("AT ISOLATION 0"))
            {
                retorno = retorno.Replace("AT ISOLATION 0", "");
            }
            if (retorno.Contains("WITH (NOLOCK)"))
            {
                retorno = retorno.Replace("WITH (NOLOCK)", "");
            }
            string codigoTagCorrespondencia1 = @"ORDER BY (.*?) DESC";
            string codigoTagCorrespondencia2 = @"ORDER BY (.*?) ASC";
            string codigoTagCorrespondencia3 = @"ORDER BY .*$";
            string codigoTagCorrespondencia4 = @"ORDER BY .*(.*)^[^\)]*";
            string codigoTagCorrespondencia5 = @"ORDER BY (.*?)[^ ][^)]*";

            string codigoASubstituir = "ORDER BY 1";
            retorno = Regex.Replace(retorno, codigoTagCorrespondencia1, codigoASubstituir, RegexOptions.IgnoreCase);
            retorno = Regex.Replace(retorno, codigoTagCorrespondencia2, codigoASubstituir, RegexOptions.IgnoreCase);
            retorno = Regex.Replace(retorno, codigoTagCorrespondencia3, codigoASubstituir, RegexOptions.IgnoreCase);
            retorno = Regex.Replace(retorno, codigoTagCorrespondencia4, codigoASubstituir, RegexOptions.IgnoreCase);
            retorno = Regex.Replace(retorno, codigoTagCorrespondencia5, codigoASubstituir, RegexOptions.IgnoreCase);
            retorno = retorno.Replace("SELECT ROW_NUMBER() OVER(ORDER BY 1)", "SELECT 1").Replace("AT ISOLATION READ UNCOMMITTED", "");

            return retorno;
        }
    }
}
