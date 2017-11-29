using System.Web.Http;
using WebActivatorEx;
using AL.Atendimento.SobConsulta.Api;
using Swashbuckle.Application;
using System;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace AL.Atendimento.SobConsulta.Api
{
    public static class SwaggerConfig
    {
        public static void Register()
        {
             GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "ApiDAD");
                    c.IncludeXmlComments(GetXmlCommentsPath());
                    //c.SchemaFilter<SwaggerAddEnumDescriptions>();
                    //c.OperationFilter<SwaggerAddEnumDescriptions>();
                    c.UseFullTypeNameInSchemaIds();
                })
                .EnableSwaggerUi(c =>
                {
                });
        }

        private static string GetXmlCommentsPath()
        {
            return string.Format(@"{0}\bin\ApiDAD.XML", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
