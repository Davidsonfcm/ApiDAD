using Al.GestaoDeContratos.Core.Base.Repositorios;
using AL.Atendimento.SobConsulta.Base.Excecoes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AL.Atendimento.SobConsulta.Base.Repositorios
{
    public static class ClienteApi
    {


        public static T ChamarApiGet<T>(string url, NameValueCollection valoresGet, bool receberNaoEncontradoComoNulo)
        {
            return ChamarApi<T>(url, Verbo.Get, valoresGet, null, receberNaoEncontradoComoNulo, null, null);
        }

        public static T ChamarApi<T>(string url, Verbo verbo, object objetoJson, bool receberNaoEncontradoComoNulo)
        {
            return ChamarApi<T>(url, verbo, null, objetoJson, receberNaoEncontradoComoNulo, null, null);
        }

        public static void ChamarApi<T>(string url, Verbo verbo, NameValueCollection valoresPost, object objetoJson, bool receberNaoEncontradoComoNulo)
        {
            ChamarApi<T>(url, verbo, valoresPost, objetoJson, receberNaoEncontradoComoNulo, null, null);
        }

        public static void ChamarApi(string url, Verbo verbo, NameValueCollection valoresPost, object objetoJson, bool receberNaoEncontradoComoNulo)
        {
            ChamarApi<string>(url, verbo, valoresPost, objetoJson, receberNaoEncontradoComoNulo, null, null);
        }


        public static T ChamarApi<T>(string url, Verbo verbo, NameValueCollection valoresGet, object objetoJson,
            bool receberNaoEncontradoComoNulo, string usuario, string senha)
        {
            try
            {
                var urlComplete = MontarUrlGet(valoresGet, url);
                using (var servico = new Request(verbo, urlComplete, usuario, senha, objetoJson))
                {
                    var response = servico.Executar();

                    var retorno = response.Content.ReadAsStringAsync().Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return JsonConvert.DeserializeObject<T>(retorno);
                    }
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return default(T);
                    }

                    if (response.StatusCode == HttpStatusCode.NotFound || response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        var erro = JsonConvert.DeserializeObject<RetornoErroProcessamento>(retorno);
                        if (erro.ErroNegocio)
                        {
                            if (receberNaoEncontradoComoNulo && response.StatusCode == HttpStatusCode.NotFound)
                            {
                                return default(T);
                            }
                            throw new NegocioApiException(erro.Mensagem ?? "Erro chamada Api", erro.CodigoErro ?? "E001", objetoJson);
                        }
                    }
                    throw new Exception($"Erro chamada Api. HttpCode {response.StatusCode}. Mensagem: {retorno}");
                }
            }
            catch (WebException ex)
            {
                try
                {
                    var response = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    dynamic erro = JsonConvert.DeserializeObject(response);

                    throw new Exception(erro.ExceptionMessage?.Value);
                }
                catch (Exception)
                {
                    throw ex;
                }
            }
        }

        private static string MontarUrlGet(NameValueCollection valoresGet, string urlComplete)
        {
            if (valoresGet != null && valoresGet.Count > 0)
            {
                var get = HttpUtility.ParseQueryString(string.Empty);

                foreach (string key in valoresGet)
                    get.Add(key, valoresGet[key]);

                return $"{urlComplete}?{get}";
            }

            return urlComplete;
        }
    }
}
