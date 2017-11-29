using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Web.Http;
using Services;
using ApiDAD.Models;


namespace ApiDAD
{
    [RoutePrefix("api/v1/apiDAD")]
    public class MainController : ApiController
    {
        /// <summary>
        /// Método Index
        /// </summary>
        /// <remarks>Valida o funcionamento da API.</remarks>
        [HttpGet]
        [Route("index")]
        public IHttpActionResult Index()
        {
            return Ok("API OK");
        }

        /// <summary>
        /// Retorna a lista de reservas que estão sobconsulta
        /// </summary>
        /// <remarks>Retorna a lista de reservas que estão sobconsulta.</remarks>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Parâmetro de entrada inválido.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Erro no processamento do servidor.")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Valor do parâmetro de entrada não corresponde a um objeto existente.")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Validar Usuario", Type = typeof(bool))]
        [Route("validarUsuario")]
        public IHttpActionResult ValidarUsuario([FromBody] UsuarioInput input)
        {
            var usuarioServices = new UsuarioServices();
            usuarioServices.ValidarUsuario(input.Login, input.Senha);
           

            return Ok(true);
        }
      
    }
}