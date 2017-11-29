using System.Web.Http;
using System.Web.Http.Results;
using Services;
using Model.DTO;
using Model;
using System.Net;
using Swashbuckle.Swagger.Annotations;

namespace ApiDAD
{
    [RoutePrefix("Api/V1/Usuario")]
    public class UsuarioController : ApiController
    {
        private UsuarioServices usuarioServices;
        private MailServices mailServices;

        public UsuarioController()
        {
            this.usuarioServices = new UsuarioServices();
            this.mailServices = new MailServices();
        }

        [HttpGet]
        [Route("Todos")]
        public ResponseDTO Todos()
        {
            return this.usuarioServices.BuscarTodos();
        }

        [HttpGet]
        [Route("Filtrar/{cpf}")]
        public ResponseDTO Filtrar(string cpf)
        {
            return this.usuarioServices.Buscar(cpf);
        }

        [HttpPost]
        [Route("Autenticar")]
        public ResponseDTO Autenticar([FromBody]Usuario usuario)
        {
            return this.usuarioServices.ValidarUsuario(usuario.cpf, usuario.senha);
        }

        [HttpPost]
        [Route("Registrar")]
        public ResponseDTO Registrar([FromBody]Usuario usuario)
        {
            return this.usuarioServices.NovoUsuario(usuario);
        }

        [HttpPut]
        [Route("Editar")]
        public ResponseDTO Editar([FromBody]Usuario usuario)
        {
            return this.usuarioServices.EditarUsuario(usuario);
        }

        [HttpDelete]
        [Route("Deletar/{id}")]
        public ResponseDTO Deletar(int id)
        {
            return this.usuarioServices.DeleteUsuario(id);
        }

        [HttpGet]
        [Route("LembrarSenha/{cpf}")]
        public ResponseDTO LembrarSenha(string cpf)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            Usuario usuario = (Usuario)this.usuarioServices.Buscar(cpf).Contents;
            if(usuario == null)
            {
                responseDTO.Message = "Cpf não cadastrado!";
                return responseDTO;
            }

            responseDTO = this.mailServices
                .EnviaMensagemEmail(usuario.email, "Lembrete de senha", 
                "Sua senha no SBG é " + usuario.senha + ", para sua segurança altere assim que acessar o portal.");

            return responseDTO;
        }
    }
}

