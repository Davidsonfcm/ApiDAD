using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Al.GestaoDeContratos.Core.Base.Repositorios
{
    public class Request : IDisposable
    {
        private HttpClientHandler handler;
        private HttpClient client;
        private Verbo verbo;
        private object objetoJson;

        public Request(Verbo verbo, string url, string usuario, string senha, object objetoJson)
        {
            this.verbo = verbo;
            this.objetoJson = objetoJson;
            if (String.IsNullOrEmpty(usuario))
            {
                client = new HttpClient();
            }
            else
            {
                handler = new HttpClientHandler { Credentials = new NetworkCredential(usuario, senha) };
                client = new HttpClient(handler);
            }
            client.BaseAddress = new Uri(url);
        }

        public HttpResponseMessage Executar()
        {
            switch (verbo)
            {
                case Verbo.Get:
                    return client.GetAsync("").Result;
                case Verbo.Post:
                    return client.PostAsJsonAsync("", objetoJson).Result;
                case Verbo.Put:
                    return client.PutAsJsonAsync("", objetoJson).Result;
                case Verbo.Delete:
                    return client.DeleteAsync("").Result;
                default:
                    throw new NotSupportedException();
            }
        }

        public void Dispose()
        {
            if (client != null)
                client.Dispose();
            if (handler != null)
                handler.Dispose();
        }

    }
}
