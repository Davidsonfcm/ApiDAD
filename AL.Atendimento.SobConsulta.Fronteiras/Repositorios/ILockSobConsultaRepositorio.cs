using AL.Atendimento.SobConsulta.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Fronteiras.Repositorios
{
    public interface ILockSobConsultaRepositorio
    {
        bool BloquearReserva(string localizador, string usuarioBloqueio);
        LockSobConsulta ObterBloqueio(string localizador);
        bool DesbloquearReserva(string localizador, string usuarioBloqueio, bool forcarDesbloqueio);
        List<LockSobConsulta> VerificarSeReservasEstaoBloqueadas(string[] reservas);
    }
}
