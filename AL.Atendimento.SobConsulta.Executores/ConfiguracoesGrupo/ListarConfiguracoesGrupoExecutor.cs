using AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.ConfiguracoesGrupo;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using AL.Atendimento.SobConsulta.Repositorios.Reservas;
using AL.Atendimento.SobConsulta.Util;
using AL.Atendimento.SobConsulta.Util.Excecoes;
using Localiza.SDK.Fronteira;
using System.Linq;

namespace AL.Atendimento.SobConsulta.Executores.Contrato
{
    public class ListarConfiguracoesGrupoExecutor : IExecutor<ListarConfiguracoesGrupoRequisicao, ListarConfiguracoesGrupoResultado>
    {
        private readonly IConfiguracaoGruposRepositorio configuracoesGrupoRepositorio;
        private readonly IEnviarEmailReportRepositorio email;

        public ListarConfiguracoesGrupoExecutor(IConfiguracaoGruposRepositorio configuracoesGrupoRepositorio, IEnviarEmailReportRepositorio email)
        {
            this.configuracoesGrupoRepositorio = configuracoesGrupoRepositorio;
            this.email = email;
        }

        public ListarConfiguracoesGrupoResultado Executar(ListarConfiguracoesGrupoRequisicao requisicao)
        {
            var configuracoes = configuracoesGrupoRepositorio.ListarConfiguracoesGrupo();
            
            return new ListarConfiguracoesGrupoResultado
            {
                ConfiguracoesGrupo = ConfiguracaoGrupoDto.CriarAPartirDeEntidade(configuracoes.ToList()),
                Estado = EstadoResultado.OK
            };
        }
    }
}
