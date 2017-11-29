using System;
using Localiza.SDK.Fronteira;
using AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.SobConsulta;

namespace AL.Atendimento.SobConsulta.Executores.SobConsulta
{
    public class BloquearReservaSobConsultaResultado : IResultado
    {
        public EstadoResultado Estado { get; set; }
        public ReservaDto ReservaSobConsulta { get; set; }
        public bool Sucesso { get; set; }
    }
}