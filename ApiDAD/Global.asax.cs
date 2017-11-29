using System.Web.Http;
using System.Web.Routing;

namespace AL.Atendimento.SobConsulta.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
