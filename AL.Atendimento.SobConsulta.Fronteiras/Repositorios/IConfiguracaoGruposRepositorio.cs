using AL.Atendimento.SobConsulta.Entidades;
using System.Collections.Generic;

namespace AL.Atendimento.SobConsulta.Fronteiras.Repositorios
{
    public interface IConfiguracaoGruposRepositorio
    {
        List<ConfiguracaoGrupos> ListarConfiguracoesGrupo();
        ConfiguracaoGrupos ObterConfiguracaoGrupo(string codigoGrupo);
        void Alterar(ConfiguracaoGrupos configuracao);
        void Gravar(ConfiguracaoGrupos configuracao);
    }
}
