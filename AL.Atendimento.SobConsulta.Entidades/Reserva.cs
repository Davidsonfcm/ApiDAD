using System;
using AL.Atendimento.SobConsulta.Base.Entidades;
using AL.Atendimento.SobConsulta.Util.Enumeracoes;
using System.Collections.Generic;

namespace AL.Atendimento.SobConsulta.Entidades
{
    public class Reserva : IEntidade
    {
        public string Localizador { get; set; }
        public DateTime DataRetirada { get; set; }
        public DateTime DataCriacaoReserva { get; set; }
        public bool PossuiTarifaPromocional { get; set; }
        public string CodigoNegociacao { get; set; }
        public string Grupo { get; set; }
        public string Agencia { get; set; }
        public string Filial { get; set; }
        public int QuantidadeDiarias { get; set; }
        public bool TarifaMensal { get; set; }
        public bool Bloqueada { get; set; }
        public string UsuarioBloqueio { get; set; }

        public string NomeAgencia { get; set; }
        public DateTime DataDevolucaoReserva { get; set; }

        public string TelefoneAgenciaRetirada { get; set; }
        public List<HorarioFuncionamento> HorarioFuncionamentoAgenciaRetirada { get; set; }
        public string EnderecoAgenciaRetirada { get; set; }


        public bool Motorista { get; set; }
        public bool Fast { get; set; }
        public bool Entrega { get; set; }

        public string AgenciaDevolucao { get; set; }
        public string NomeAgenciaDevolucao { get; set; }
        public string NomeCliente { get; set; }
        public string SegmentoCliente { get; set; }
        public TelefoneCliente ContatoCliente { get; set; }
        public string EmailCliente { get; set; }

        public string Observacao { get; set; }
        public string Status { get; set; }
        public TipoFormaPagamento FormaPagamento { get; set; }

        public string SituacaoFidelidade { get; set; }

        public string Consultor { get; set; }

        public InformacoesUsuario SupervisorAgenciaRetirada { get; set; }
        public InformacoesUsuario RegionalAgenciaRetirada { get; set; }
        public IChaveEntidade ObterChave()
        {
            return new ChaveEntidadeString(Localizador);
        }

    }
}
