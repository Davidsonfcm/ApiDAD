using AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using Localiza.SDK.Fronteira;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Executores.SobConsulta
{
    public class ListarReservasProcessadasExecutor : IExecutor<ListarReservasProcessadasRequisicao, ListarReservasProcessadasResultado>
    {
        private readonly IReservasProcessadasRepositorio reservasProcessadasRepositorio;

        public ListarReservasProcessadasExecutor(IReservasProcessadasRepositorio reservasProcessadasRepositorio)
        {
            this.reservasProcessadasRepositorio = reservasProcessadasRepositorio;
        }

        public ListarReservasProcessadasResultado Executar(ListarReservasProcessadasRequisicao requisicao)
        {
            var reservas = reservasProcessadasRepositorio.Obter(requisicao.Localizador, requisicao.DataInicio, requisicao.DataFim);

            return new ListarReservasProcessadasResultado
            {
                ReservasProcessadas = ReservasProcessadasDto.CriarAPartirDeEntidade(reservas)
            };
        }
    }
}
