using Model;
using Model.DTO;
using Services;
using System.Web.Http;

namespace ApiDAD.Controllers
{
    [RoutePrefix("Api/V1/Consulta")]
    public class ConsultaController : ApiController
    {
        private ConsultaServices consultaServices;

        public ConsultaController()
        {
            this.consultaServices = new ConsultaServices();
        }

        [HttpPost]
        [Route("Agendar")]
        public ResponseDTO Agendar([FromBody]Consulta consulta)
        {
            return this.consultaServices.AgendarConsulta(consulta);
        }

        [HttpPost]
        [Route("Diagnosticar")]
        public ResponseDTO Diagnosticar([FromBody]Consulta consulta)
        {
            return this.consultaServices.AgendarConsulta(consulta);
        }

        [HttpGet]
        [Route("Filtrar/{cpf}")]
        public ResponseDTO Filtrar(string cpf)
        {
            return this.consultaServices.Buscar(cpf);
        }

        [HttpGet]
        [Route("FiltrarId/{identificador}")]
        public ResponseDTO FiltrarId(int identificador)
        {
            return this.consultaServices.BuscarConsulta(identificador);
        }

        [HttpGet]
        [Route("Todos")]
        public ResponseDTO Todos()
        {
            return this.consultaServices.BuscarTodos();
        }

        [HttpGet]
        [Route("Cancelar/{identificador}")]
        public ResponseDTO Cancelar(int identificador)
        {
            return this.consultaServices.CancelarConsulta(identificador);
        }

    }
}