using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using Localiza.SDK.Fronteira;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Executores.SobConsulta
{
    public class ListarMotivosCancelamentoExecutor : IExecutorSemRequisicao<ListarMotivosCancelamentoResultado>
    {
        private readonly IMotivoCancelamentoRepositorio motivoCancelamentoRepositorio;

        public ListarMotivosCancelamentoExecutor(IMotivoCancelamentoRepositorio motivoCancelamentoRepositorio)
        {
            this.motivoCancelamentoRepositorio = motivoCancelamentoRepositorio;
        }

        public ListarMotivosCancelamentoResultado Executar()
        {
            List<MotivoCancelamento> motivos = motivoCancelamentoRepositorio.ListarMotivos();

            return new ListarMotivosCancelamentoResultado
            {
                MotivosCancelamento = MotivoCancelamentoDto.CriarAPartirDeEntidade(motivos)
            };
        }
    }
}
