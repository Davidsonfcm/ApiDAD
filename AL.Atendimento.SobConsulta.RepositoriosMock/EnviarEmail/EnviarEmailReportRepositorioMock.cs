using System;
using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;

namespace AL.Atendimento.SobConsulta.MapeamentosMock
{
    public class EnviarEmailReportRepositorioMock : IEnviarEmailReportRepositorio
    {
        public void EnviarEmailViaReport(DadosEnvioEmailCeres dadosEmail)
        {
            throw new NotImplementedException();
        }
    }
}