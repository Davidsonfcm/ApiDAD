using System;
using System.Collections;
using System.Collections.Generic;

namespace AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.SobConsulta
{
    public class ReservaSobConsultaDto : DtoBase<Atendimento.SobConsulta.Entidades.ReservaSobConsulta, ReservaSobConsultaDto>
    {
        public string Localizador { get; set; }
        public DateTime DataRetirada { get; set; }
        public DateTime DataDevolucao { get; set; }
        public DateTime DataCriacaoReserva { get; set; }
        public string NomeCliente { get; set; }
        public string Grupo { get; set; }
        public string Agencia { get; set; }
        public string NomeAgencia { get; set; }
        public string Filial { get; set; }
        public int QuantidadeDiarias { get; set; }
        public bool TarifaMensal { get; set; }
        public bool Bloqueada { get; set; }
        public string CodigoOrigem { get; set; }
        public string DescricaoOrigem { get; set; }
        public UsuarioLockDto UsuarioBloqueio { get; set; }
    }
}
