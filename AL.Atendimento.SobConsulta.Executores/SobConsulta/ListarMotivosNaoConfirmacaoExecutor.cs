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
    public class ListarMotivosNaoConfirmacaoExecutor : IExecutorSemRequisicao<ListarMotivosNaoConfirmacaoResultado>
    {
        private readonly IMotivoNaoConfirmacaoRepositorio motivoNaoConfirmacaoRepositorio;

        public ListarMotivosNaoConfirmacaoExecutor(IMotivoNaoConfirmacaoRepositorio motivoNaoConfirmacaoRepositorio)
        {
            this.motivoNaoConfirmacaoRepositorio = motivoNaoConfirmacaoRepositorio;
        }

        public ListarMotivosNaoConfirmacaoResultado Executar()
        {
            List<MotivoNaoConfirmacao> motivos = motivoNaoConfirmacaoRepositorio.ListarMotivos();

            return new ListarMotivosNaoConfirmacaoResultado
            {
                MotivosNaoConfirmacao = MotivoNaoConfirmacaoDto.CriarAPartirDeEntidade(motivos)
            };
        }
    }
}
