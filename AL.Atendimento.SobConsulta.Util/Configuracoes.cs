using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Util
{
    public static class Configuracoes
    {
        public static class Geral
        {
            public static string NomeAplicacao { get { return "SobConsultaAPI"; } }

        }
        public static class ParametrosSpoc
        {
            public static string HabilitarEnvioEmailErro { get { return "HABILITAR_ENVIO_EMAIL_ERRO"; } }
            public static string RemetenteEmailErro { get { return "REMETENTE_EMAIL_ERRO"; } }
            public static string DestinatariosEmailErro { get { return "DESTINATARIO_EMAIL_ERRO"; } }
            public static string ChaveUtilizacaoBuildBlockEmail { get { return "CHAVE_UTILIZACAO_BUILD_BLOCK_EMAIL"; } }
            public static string UsuarioProcessamentoSobConsulta { get { return "USUARIO_PROCESSAMENTO_SOBCONSULTA"; } }
            public static string ChaveCacheOrdenacaoGrupos { get { return "DURACAO_CACHE_ORDENACAO_GRUPOS"; } }
            public static string TempoMaisProximoReservasSobConsulta { get { return "TEMPO_MAIS_PROXIMO_RESERVA_SOBCONSULTA"; } }
            public static string TempoLimiteInferiorReservaSobConsulta { get { return "TEMPO_LIMITE_INFERIOR_RESERVA_SOBCONSULTA"; } }
            public static string TempoLimiteSuperiorReservaSobConsulta { get { return "TEMPO_LIMITE_SUPERIOR_RESERVA_SOBCONSULTA"; } }
            public static string ParametroHoraConfirmacaoReservaFilial { get { return "HORAS_CONFIRMACAO_RESERVA_SOBCONSULTA_PELA_FILIAL"; } }
            public static string ParametroSaldoFrotaProdutivaParaConfirmarReserva { get { return "PARAMETRO_SALDO_FROTA_PRODUTIVA_PARA_CONFIRMAR_RESERVA"; } }

        }

        public static class ParametroReport
        {
            public static string UsuarioReport { get { return ConfigurationManager.AppSettings["UsuarioReport"]; } }
            public static string SenhaReport { get { return ConfigurationManager.AppSettings["SenhaReport"]; } }
            public static string DominioReport { get { return ConfigurationManager.AppSettings["DominioReport"]; } }
            public static string URLRelatorio { get { return ConfigurationManager.AppSettings["URLRelatorio"]; } }
            public static string FormatoReport { get { return ConfigurationManager.AppSettings["FormatoReport"]; } }
            public static string CaminhoRelatorioConfirmacao { get { return ConfigurationManager.AppSettings["CaminhoRelatorioConfirmacao"]; } }
            public static string URLCorpoEmailConfirmacao { get { return ConfigurationManager.AppSettings["URLCorpoEmailConfirmacao"]; } }
            public static string RemetenteEmail { get { return ConfigurationManager.AppSettings["RemetenteEmail"]; } }
        }

        public static class ItensParametro
        {
            public static string OrigemNucleo { get { return "NR_ORIG_VCH_NR"; } }
            public static string NovoFormatoInternetMobile { get { return "CR_EMAILSEMOFERTA_NR"; } }
        }

        public static class ChaveSequencial
        {
            public static string SequencialProcessamentoSobconsulta { get { return "SEQ_SOBCONSULTA_PROCESSA"; } }
        }
    }
}
