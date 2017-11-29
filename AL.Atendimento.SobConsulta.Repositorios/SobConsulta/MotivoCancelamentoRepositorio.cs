using AL.Atendimento.SobConsulta.Base.Repositorios;
using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AL.Atendimento.SobConsulta.Repositorios.SobConsulta
{
    public class MotivoCancelamentoRepositorio : RepositorioBase<ReservasProcessadas>, IMotivoCancelamentoRepositorio
    {
        public MotivoCancelamentoRepositorio() : base(ConexaoBanco.TipoConexao.Aguia)
        {

        }

        #region Query
        private static readonly string SQL_SELECT = @"SELECT m.cod_motivo AS Codigo, m.descricao as Descricao
                                                        FROM motivos_canc_res m
                                                        INNER JOIN res_motivo_sistema sm ON m.cod_motivo = sm.cod_motivo
                                                        INNER JOIN sistema s ON sm.cod_sistema = s.cod_sistema
                                                        WHERE s.cod_sistema = 8";

        #endregion
        
        public List<MotivoCancelamento> ListarMotivos()
        {
            return Listar<MotivoCancelamento>(SQL_SELECT, null).ToList();
        }
    }
}
