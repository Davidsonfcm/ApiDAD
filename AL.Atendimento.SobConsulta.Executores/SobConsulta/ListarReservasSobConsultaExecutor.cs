using AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using AL.Atendimento.SobConsulta.Repositorios.Reservas;
using AL.Atendimento.SobConsulta.Util;
using AL.Atendimento.SobConsulta.Util.Excecoes;
using Localiza.SDK.Fronteira;
using System.Linq;

namespace AL.Atendimento.SobConsulta.Executores.Contrato
{
    public class ListarReservasSobConsultaExecutor : IExecutor<ListarReservasSobConsultaRequisicao, ListarReservasSobConsultaResultado>
    {
        private readonly IReservaNrRepositorio reservaRepositorio;
 
        private readonly ILockSobConsultaRepositorio lockSobConsultaRepositorio;

        public ListarReservasSobConsultaExecutor(IReservaNrRepositorio reservaRepositorio, ILockSobConsultaRepositorio lockSobConsultaRepositorio)
        {
            this.reservaRepositorio = reservaRepositorio;
            this.lockSobConsultaRepositorio = lockSobConsultaRepositorio;
        }

        public ListarReservasSobConsultaResultado Executar(ListarReservasSobConsultaRequisicao requisicao)
        {
            var reservas = reservaRepositorio.ListarReservasSobConsulta(requisicao.Internacional);

            if (reservas != null && reservas.Any())
            {
                var reservasBloqueadas = lockSobConsultaRepositorio.VerificarSeReservasEstaoBloqueadas(reservas.Select(x => x.Localizador).ToArray());

                if (reservasBloqueadas != null)
                {
                    foreach (var reservaBloqueada in reservasBloqueadas)
                    {
                        Entidades.ReservaSobConsulta reserva = reservas.Where(x => x.Localizador == reservaBloqueada.Localizador).FirstOrDefault();
                        reserva.Bloqueada = true;
                        reserva.UsuarioBloqueio = reservaBloqueada.UsuarioLock;
                    }
                }
            }
            return new ListarReservasSobConsultaResultado
            {
                ReservasSobConsulta = ReservaSobConsultaDto.CriarAPartirDeEntidade(reservas.ToList()),
                Estado = EstadoResultado.OK
            };
        }
    }
}
