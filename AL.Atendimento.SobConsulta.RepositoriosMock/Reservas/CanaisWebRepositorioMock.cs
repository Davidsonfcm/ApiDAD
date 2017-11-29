using System;
using AL.Atendimento.SobConsulta.Repositorios.Reservas;

namespace AL.Atendimento.SobConsulta.MapeamentosMock
{
    public class CanaisWebRepositorioMock : ICanaisWebRepositorio
    {
        public void EnviarEmailConfirmacaoReservaComListaDestinatarios(string localizador, string[] listaDestinatarios, string codigoEmissor)
        {
            throw new NotImplementedException();
        }
    }
}