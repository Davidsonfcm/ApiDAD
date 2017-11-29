using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Util.Enumeracoes;
using System.Collections.Generic;

namespace AL.Atendimento.SobConsulta.Fronteiras.Repositorios
{
    public interface IReservaNrRepositorio
    {
        Reserva ObterReserva(string localizador);
        ReservaSobConsulta[] ListarReservasSobConsulta(bool internacional = false);
        bool VerificarSeOfertasEstaoAtivasParaAgencia(string codigoAgenciaRetirada);
        void EnviarEmailNucleoReserva(string localizador);
        void AlterarStatusReservaNr(DadosComunicacao reservaWs);
    }
}
