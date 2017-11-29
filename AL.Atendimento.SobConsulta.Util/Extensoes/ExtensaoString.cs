using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Util.Extensoes
{
    public static class ExtensaoString
    {
        private const string EXCECAO_MAIUSCULAS = "ELX,LT,LS,GLX,HLX,GPS,CPF,SERASA,CNH,GC,GII,GIII,FATAC,ADMCC,ASCLI,ADMUL,SAP";
        private const string EXCECAO_MINUSCULAS = "de,do,da,e,das,dos";

        public static String FormatarTexto(this String stringIrregular, char corteCaractere = ' ')
        {
            if (String.IsNullOrEmpty(stringIrregular) || String.IsNullOrEmpty(stringIrregular.Trim()))
            {
                return "";
            }
            else
            {
                String retorno = "";
                String[] palavras = stringIrregular.Trim().Split(corteCaractere);

                for (int i = 0; i < palavras.Length; i++)
                {
                    var palavra = palavras[i];
                    palavras[i] = Excecoes(palavra);
                }
                retorno = string.Join(corteCaractere.ToString(), palavras);

                return retorno.TrimEnd(corteCaractere);
            }
        }


        public static String Excecoes(this String stringIrregular)
        {
            if (String.IsNullOrEmpty(stringIrregular))
            {
                return "";
            }
            else
            {
                if (ExcecaoPalavraMinuscula(stringIrregular))
                {
                    return stringIrregular.ToLower();
                }
                if (ExcecaoPalavraMaiuscula(stringIrregular))
                {
                    return stringIrregular.ToUpper();
                }
                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(stringIrregular.ToLower());
            }
        }

        private static bool ExcecaoPalavraMaiuscula(string stringIrregular)
        {
            return EXCECAO_MAIUSCULAS.Split(',').Select(palavra => palavra.ToUpper()).Contains(stringIrregular.ToUpper());
        }

        private static bool ExcecaoPalavraMinuscula(string stringIrregular)
        {
            return EXCECAO_MINUSCULAS.Split(',').Select(palavra => palavra.ToUpper()).Contains(stringIrregular.ToUpper());
        }
    }
}
