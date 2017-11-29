using System;
using AL.Atendimento.SobConsulta.Base.Entidades;
using AL.Atendimento.SobConsulta.Util.Enumeracoes;
using System.Collections.Generic;

namespace AL.Atendimento.SobConsulta.Entidades
{
    public class ReservaSobConsulta : IEntidade
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
        public UsuarioLock UsuarioBloqueio { get; set; }
        public string CodigoOrigem { get; set; }
        public string DescricaoOrigem { get; set; }
        public string SegmentoCliente { get; set; }
        public IChaveEntidade ObterChave()
        {
            return new ChaveEntidadeString(Localizador);
        }

    }
}
