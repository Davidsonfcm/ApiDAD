using System;
using AL.Atendimento.SobConsulta.Entidades;

namespace AL.Atendimento.SobConsulta.Repositorios.Reservas
{
    public interface IReservaWsRepositorio
    {
        DadosComunicacao obterDadosReserva(string localizador, string codigoUsuario);
        void EnviarEmailConsultorAGVIGSeNecessario(DadosComunicacao dadosComunicacao, string codigoUsuario);
        void EnviarEmailConfirmacaoSolicitante(string localizador, string codigoUsuario);
        void AlterarStatusReservaWs(DadosComunicacao reservaWS, string codigoUsuario);
        string BuscarSituacaoReserva(string numeroReserva, string codigoUsuario);
    }
}