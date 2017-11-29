using AL.Atendimento.SobConsulta.Base.Repositorios;
using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using Dapper;

namespace AL.Atendimento.SobConsulta.Repositorios.Parametros
{
    public class ConfirmacaoReservaSobConsultaRepositorio : RepositorioBase<ConfirmacaoSobConsulta>, IConfirmacaoReservaSobConsultaRepositorio
    {
        public ConfirmacaoReservaSobConsultaRepositorio() : base(ConexaoBanco.TipoConexao.Aguia)
        {
        }

        #region Queries

        private static readonly string SQL_OBTER = @"
            SELECT 
                res_num AS Localizador,
                cd_status AS MotivoConfirmacao,
                qtd_frota_original AS QtdFrotaOriginal,
                qtd_frota_processada AS QtdFrotaProcessada,
                qtd_frota_alugada_original AS QtdFrotaAlugadaOriginal,
                qtd_frota_alugada_processada AS QtdFrotaAlugadaProcessada,
                rate_group_processado AS GrupoConfirmacao,
                num_processamento AS QtdProcessamentos,
                id_processamento AS IdProcessamentoAutomatico,
                dt_criacao AS DataCriacao,
                dt_ultima_alteracao AS DataAlteracao,
                cd_responsavel_alteracao AS CodigoUsuarioAlteracao
            FROM 
	            res_sobconsulta 
            WHERE 
	            res_num = @localizador
        ";

        private static readonly string SQL_ATUALIZAR = @"
            UPDATE res_sobconsulta
            SET 
	            cd_status = @motivoConfirmacao,
	            qtd_frota_original = @qtdFrotaOriginal,
	            qtd_frota_processada = @qtdFrotaProcessada,
	            qtd_frota_alugada_original = @qtdFrotaAlugadaOriginal,
	            qtd_frota_alugada_processada = @qtdFrotaAlugadaProcessada,
	            rate_group_processado = @grupoConfirmacao,
	            num_processamento = @qtdProcessamentos,
	            id_processamento = @idProcessamentoAutomatico,
	            dt_ultima_alteracao = @dataAlteracao,
	            cd_responsavel_alteracao = @codigoUsuarioAlteracao
            WHERE res_num = @localizador 
        ";

        private static readonly string SQL_INSERIR = @"
            INSERT INTO res_sobconsulta
	            (
	            res_num,
	            cd_status,
	            qtd_frota_original,
	            qtd_frota_processada,
	            qtd_frota_alugada_original,
	            qtd_frota_alugada_processada,
	            rate_group_processado,
	            num_processamento,
	            id_processamento,
	            dt_criacao,
	            dt_ultima_alteracao,
	            cd_responsavel_alteracao
	            )
            VALUES 
	            (
	            @localizador,
	            @motivoConfirmacao,
	            @qtdFrotaOriginal,
	            @qtdFrotaProcessada,
	            @qtdFrotaAlugadaOriginal,
	            @qtdFrotaAlugadaProcessada,
	            @grupoConfirmacao,
	            @qtdProcessamentos,
	            @idProcessamentoAutomatico,
	            @dataCriacao,
	            @dataAlteracao,
	            @codigoUsuarioAlteracao
	            )
        ";

        #endregion


        public bool CriarConfirmacaoReserva(ConfirmacaoSobConsulta confirmacaoReservaSobConsulta)
        {
            if (confirmacaoReservaSobConsulta != null)
            {
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@localizador", confirmacaoReservaSobConsulta.Localizador, TipoParametro.StringComTamanhoVariavel);
                parametros.Add("@motivoConfirmacao", (int)confirmacaoReservaSobConsulta.MotivoConfirmacao, TipoParametro.Inteiro);
                parametros.Add("@qtdFrotaOriginal", confirmacaoReservaSobConsulta.QtdFrotaOriginal, TipoParametro.Decimal);
                parametros.Add("@qtdFrotaProcessada", confirmacaoReservaSobConsulta.QtdFrotaProcessada, TipoParametro.Decimal);
                parametros.Add("@qtdFrotaAlugadaOriginal", confirmacaoReservaSobConsulta.QtdFrotaAlugadaOriginal, TipoParametro.Decimal);
                parametros.Add("@qtdFrotaAlugadaProcessada", confirmacaoReservaSobConsulta.QtdFrotaAlugadaProcessada, TipoParametro.Decimal);
                parametros.Add("@grupoConfirmacao", confirmacaoReservaSobConsulta.GrupoConfirmacao, TipoParametro.StringComTamanhoVariavel);
                parametros.Add("@qtdProcessamentos", confirmacaoReservaSobConsulta.QtdProcessamentos, TipoParametro.Inteiro);
                parametros.Add("@idProcessamentoAutomatico", confirmacaoReservaSobConsulta.IdProcessamentoAutomatico, TipoParametro.Inteiro);
                parametros.Add("@dataCriacao", confirmacaoReservaSobConsulta.DataCriacao, TipoParametro.DataETempo);
                parametros.Add("@dataAlteracao", confirmacaoReservaSobConsulta.DataAlteracao, TipoParametro.DataETempo);
                parametros.Add("@codigoUsuarioAlteracao", confirmacaoReservaSobConsulta.CodigoUsuarioAlteracao, TipoParametro.StringComTamanhoVariavel);

                return Executar(SQL_INSERIR, parametros) > 0;
            }
            else
                return false;
        }

        public ConfirmacaoSobConsulta AtualizarConfirmacaoReserva(ConfirmacaoSobConsulta confirmacaoReservaSobConsulta)
        {
            if (confirmacaoReservaSobConsulta != null)
            {
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@motivoConfirmacao", (int)confirmacaoReservaSobConsulta.MotivoConfirmacao, TipoParametro.Inteiro);
                parametros.Add("@qtdFrotaOriginal", confirmacaoReservaSobConsulta.QtdFrotaOriginal, TipoParametro.Inteiro);
                parametros.Add("@qtdFrotaProcessada", confirmacaoReservaSobConsulta.QtdFrotaProcessada, TipoParametro.Inteiro);
                parametros.Add("@qtdFrotaAlugadaOriginal", confirmacaoReservaSobConsulta.QtdFrotaAlugadaOriginal, TipoParametro.Inteiro);
                parametros.Add("@qtdFrotaAlugadaProcessada", confirmacaoReservaSobConsulta.QtdFrotaAlugadaProcessada, TipoParametro.Inteiro);
                parametros.Add("@grupoConfirmacao", confirmacaoReservaSobConsulta.GrupoConfirmacao, TipoParametro.StringComTamanhoVariavel);
                parametros.Add("@qtdProcessamentos", confirmacaoReservaSobConsulta.QtdProcessamentos, TipoParametro.Inteiro);
                parametros.Add("@idProcessamentoAutomatico", confirmacaoReservaSobConsulta.IdProcessamentoAutomatico, TipoParametro.Inteiro);
                parametros.Add("@dataAlteracao", confirmacaoReservaSobConsulta.DataAlteracao, TipoParametro.DataETempo);
                parametros.Add("@codigoUsuarioAlteracao", confirmacaoReservaSobConsulta.CodigoUsuarioAlteracao, TipoParametro.StringComTamanhoVariavel);
                parametros.Add("@localizador", confirmacaoReservaSobConsulta.Localizador, TipoParametro.StringComTamanhoVariavel);

                Executar(SQL_ATUALIZAR, parametros);

                return Obter(confirmacaoReservaSobConsulta.Localizador);
            }
            else
                return null;
        }

        public ConfirmacaoSobConsulta Obter(string localizador)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@localizador", localizador, TipoParametro.StringComTamanhoVariavel);
            return Obter(SQL_OBTER, parametros);
        }
    }
}
