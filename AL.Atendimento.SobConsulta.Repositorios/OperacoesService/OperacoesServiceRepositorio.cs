using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AL.Atendimento.SobConsulta.Base.Repositorios.ConexaoBanco;

namespace AL.Atendimento.SobConsulta.Repositorios.OperacoesService
{
    public class OperacoesServiceRepositorio : IOperacoesServiceRepositorio
    {

        public AgenciaEntidade ObterCodigoSupervisorRegionalAgencia(string codigoAgencia)
        {
            if (!string.IsNullOrEmpty(codigoAgencia))
            {
                using (OperacoesService.OperacoesClient cliente = new OperacoesClient())
                {
                    var CodigosSupervisorRegionalAgencia = cliente.ObterLiderancaAgencia(codigoAgencia);
                    if (CodigosSupervisorRegionalAgencia != null)
                    {
                        AgenciaEntidade DadosSupervisoresAgencia = new AgenciaEntidade
                        {
                            MatriculaGerente = CodigosSupervisorRegionalAgencia.MatriculaGerente,
                            MatriculaSupervisor = CodigosSupervisorRegionalAgencia.MatriculaSupervisor
                        };
                        return DadosSupervisoresAgencia;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
                return null;
        }

    }
}
