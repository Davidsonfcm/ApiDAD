using System;
using Localiza.SDK.Fronteira;
using AL.Atendimento.SobConsulta.Entidades;

namespace AL.Atendimento.SobConsulta.Executores.SobConsulta
{
    public class AlterarReservaSobConsultaResultado : IResultado
    {
        public EstadoResultado Estado { get; set; }
        public DadosComunicacao DadosReserva { get; set; }
        public Reserva DadosReservaNr { get; set; }
    }
}