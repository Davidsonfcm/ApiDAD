using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Web.Http;
using Services;
using ApiDAD.Models;
using Model.DTO;

namespace ApiDAD
{
    [RoutePrefix("Api/V1/Main")]
    public class MainController : ApiController
    {
        /// <summary>
        /// Método Index
        /// </summary>
        /// <remarks>Valida o funcionamento da API.</remarks>
        [HttpGet]
        [Route("Index")]
        public IHttpActionResult Index()
        {
            return Ok("API OK");
        }

        /// <summary>
        /// Retorna a lista de reservas que estão sobconsulta
        /// </summary>
        /// <remarks>Retorna a lista de reservas que estão sobconsulta.</remarks>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Parâmetro de entrada inválido.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Erro no processamento do servidor.")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Valor do parâmetro de entrada não corresponde a um objeto existente.")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Validar Usuario", Type = typeof(bool))]
        [Route("ValidarUsuario")]
        public IHttpActionResult ValidarUsuario([FromBody]LoginInput loginInput)
        {
            var usuarioServices = new UsuarioServices();
            ResponseDTO response = usuarioServices.ValidarUsuario(loginInput.Cpf, loginInput.Senha);
           
            return Ok(response);
        }
      
    }
}