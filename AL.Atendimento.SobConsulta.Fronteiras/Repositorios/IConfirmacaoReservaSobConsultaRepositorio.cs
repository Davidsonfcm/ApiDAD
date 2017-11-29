using AL.Atendimento.SobConsulta.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Fronteiras.Repositorios
{
    public interface IConfirmacaoReservaSobConsultaRepositorio
    {
        bool CriarConfirmacaoReserva(ConfirmacaoSobConsulta confirmacaoReservaSobConsulta);
        ConfirmacaoSobConsulta AtualizarConfirmacaoReserva(ConfirmacaoSobConsulta confirmacaoReservaSobConsulta);
        ConfirmacaoSobConsulta Obter(string localizador);
    }
}
