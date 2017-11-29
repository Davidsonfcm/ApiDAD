using AL.Atendimento.SobConsulta.Base.Repositorios;
using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AL.Atendimento.SobConsulta.Repositorios.SobConsulta
{
    public class ReservasProcessadasRepositorio : RepositorioBase<ReservasProcessadas>, IReservasProcessadasRepositorio
    {
        #region Query
        private static readonly string SQL_SELECT = @"SELECT 
            rs.res_num AS Localizador,
            rs.dt_ultima_alteracao AS DataAtendimento, 
            rs.cd_status AS JustificativaConfirmacao,
            r.res_status AS Situacao,
            r.cod_motivo_canc AS CodigoMotivoCancelamento,
            mc.descricao AS DescricaoMotivoCancelamento, 
            r.rmc_cod_nao_confirmada AS CodigoMotivoNaoConfirmacao, 
            mn.rmc_desc_nao_confirmada AS DescricaoMotivoNaoConfirmacao,
            rs.qtd_frota_original AS SaldoOriginal,
            rs.qtd_frota_processada AS SaldoProcessado,
            rs.qtd_frota_alugada_original AS AlugadoOriginal,
            rs.qtd_frota_alugada_processada AS AlugadoProcessado,
            rs.rate_group_processado AS GrupoProcessado,
            rs.cd_responsavel_alteracao AS Responsavel,
            r.create_date AS DataCriacaoReserva
        FROM res_sobconsulta rs
        INNER JOIN reservations r
            ON rs.res_num = r.res_num
        LEFT JOIN motivos_canc_res  mc
            ON r.cod_motivo_canc = mc.cod_motivo
        LEFT JOIN res_motivo_nao_confirmada mn
            ON r.rmc_cod_nao_confirmada = mn.rmc_cod_nao_confirmada ";

        private static readonly string SQL_WHERE_LOCALIZADOR =
            @" WHERE rs.res_num = @localizador";

        private static readonly string SQL_WHERE_DATAS =
            @" WHERE CAST(rs.dt_ultima_alteracao AS DATE) >= @inicio AND
            CAST(rs.dt_ultima_alteracao AS DATE) <= @fim";

        private static readonly string SQL_WHERE_LOCALIZADOR_E_DATAS =
            @" WHERE rs.res_num = @localizador AND
           CAST(rs.dt_ultima_alteracao AS DATE) >= @inicio AND
            CAST(rs.dt_ultima_alteracao AS DATE) <= @fim";
        #endregion

        public List<ReservasProcessadas> Obter(string localizador, string dataInicio, string dataFim)
        {
            DynamicParameters parametros = new DynamicParameters();

            if (!string.IsNullOrEmpty(localizador) && dataInicio != null && dataFim != null)
            {
                parametros.Add("@localizador", localizador);
                parametros.Add("@inicio", dataInicio);
                parametros.Add("@fim", dataFim);

                return Listar<ReservasProcessadas>(SQL_SELECT + SQL_WHERE_LOCALIZADOR_E_DATAS, parametros).ToList();
            }
            else if (!string.IsNullOrEmpty(localizador))
            {
                parametros.Add("@localizador", localizador);
                return Listar<ReservasProcessadas>(SQL_SELECT + SQL_WHERE_LOCALIZADOR, parametros).ToList();
            }
            else if (dataInicio != null && dataFim != null)
            {
                parametros.Add("@inicio", dataInicio);
                parametros.Add("@fim", dataFim);
                return Listar<ReservasProcessadas>(SQL_SELECT + SQL_WHERE_DATAS, parametros).ToList();
            }
            return Listar<ReservasProcessadas>(SQL_SELECT, null).ToList();
        }

        public ReservasProcessadasRepositorio() : base(ConexaoBanco.TipoConexao.Aguia)
        {

        }
    }
}
