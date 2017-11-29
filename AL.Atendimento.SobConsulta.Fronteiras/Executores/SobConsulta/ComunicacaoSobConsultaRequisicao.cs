using AL.Atendimento.SobConsulta.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Fronteiras.Executores.SobConsulta
{
    public class ComunicacaoSobConsultaRequisicao
    {
        public DadosComunicacao DadosComunicacaoReserva{get; set;}
        public Reserva DadosReservaNR { get; set; }
        public String CodigoUsuario { get; set; }
    }
}
