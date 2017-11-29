using AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.SobConsulta;
using Localiza.SDK.Fronteira;
using System.Collections.Generic;

namespace AL.Atendimento.SobConsulta.Fronteiras.Executores.SobConsulta
{
    public class ObterReservaSobConsultaResultado : IResultado
    {
        public EstadoResultado Estado { get; set; }
        public ReservaDto ReservaSobConsulta { get; set; }
    }
}
