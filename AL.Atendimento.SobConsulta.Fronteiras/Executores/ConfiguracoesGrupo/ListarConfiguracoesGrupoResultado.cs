using AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.SobConsulta;
using Localiza.SDK.Fronteira;
using System.Collections.Generic;

namespace AL.Atendimento.SobConsulta.Fronteiras.Executores.ConfiguracoesGrupo
{
    public class ListarConfiguracoesGrupoResultado : IResultado
    {
        public EstadoResultado Estado { get; set; }
        public IEnumerable<ConfiguracaoGrupoDto> ConfiguracoesGrupo { get; set; }
    }
}
