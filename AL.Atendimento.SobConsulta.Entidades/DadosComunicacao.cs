using AL.Atendimento.SobConsulta.Base.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Entidades
{
    public class DadosComunicacao : IEntidade
    {
        public string Localizador { get; set; }
        public string GrupoReserva { get; set; }
        public DateTime DataRetirada { get; set; }
        public DateTime DataRetorno { get; set; }
        public string DestinatarioEmail { get; set; }
        public string CodigoAGVIG { get; set; }
        public string NomeAgenciaViagemSeguradora { get; set; }
        public string NomeCliente { get; set; }
        public string NomeCidadeRetirada { get; set; }
        public string NomeAgenciaRetirada { get; set; }
        public string NomeCidadeRetorno { get; set; }
        public string NomeAgenciaRetorno { get; set; }
        public string TipoProtecao { get; set; }
        public string NomeConsultor { get; set; }
        public string TipoOrigem { get; set; }
        public string SituacaoReserva { get; set; }
        public string CodigoCliente { get; set; }
        public char TipoCliente { get; set; }
        public string CodigoUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public bool EnviarEmail { get; set; }
        public string EmailRequisitante { get; set; }
        public bool EnviarEmailSolicitante { get; set; }
        public string EmailSolicitante { get; set; }
        public bool FaturaAgenciaViagem { get; set; }
        public string CodigoEvento { get; set; }
        public char TipoClienteAgenciaViagemSeguradora { get; set; }
        public int? IdOferta { get; set; }
        public bool IndicadorPrePagamento { get; set; }
        public string BinaRequisitante { get; set; }
        public string TelefoneCelularRequisitante { get; set; }
        public string CodigoEmpresaAerea { get; set; }
        public string TipoReserva { get; set; }
        public DateTime? DataVoo { get; set; }
        public bool EnviarSMS { get; set; }
        public string NomeRequisitante { get; set; }
        public string NumeroVoo { get; set; }
        public string ObservacaoReserva { get; set; }
        public string TelefoneSolicitante { get; set; }
        public short CodigoMotivoCancelamento { get; set; }
        public short MotivoNaoConfirmacao { get; set; }
        public string CodigoCartao { get; set; }
        public string CodigoAdministradoraCartao { get; set; }
        public DateTime DataValidadeCartrao { get; set; }
        public string CodigoEmissor { get; set; }
        public string PNR { get; set; }
        public string NumeroSinistro { get; set; }
        public int? CodigoDepartamento { get; set; }
        public int? TipoUpgradeCliente { get; set; }
        public string GrupoDestino { get; set; }
        public string TelefoneRequisitante { get; set; }
        public string CelularSMS { get; set; }
        public string CpfUsuario { get; set; }
        public string Cpf { get; set; }
        public int? TipoNacionalidadeClienteUsuario { get; set; }
        public string EmailConsultorEnvio { get; set; }
        public char TipoMotorista { get; set; }
        public string CodigoTipoMotorista { get; set; }
        public string Destino { get; set; }
        public string LocalAtendimentoMotorista { get; set; }
        public string Roteiro { get; set; }
        public bool MotoristaDisponivelViagem { get; set; }
        public bool MotoristaTerno { get; set; }
        public ContatosAssociadosReserva[] ContatosAssociados { get; set; }
        
        public IChaveEntidade ObterChave()
        {
            return new ChaveEntidadeString(Localizador);
        }
    }
}
