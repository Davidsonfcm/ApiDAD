using AL.Atendimento.SobConsulta.Base.Repositorios;
using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AL.Atendimento.SobConsulta.Repositorios.SobConsulta
{
    public class MotivoNaoConfirmacaoRepositorio : RepositorioBase<MotivoNaoConfirmacao>, IMotivoNaoConfirmacaoRepositorio
    {
        public MotivoNaoConfirmacaoRepositorio() : base(ConexaoBanco.TipoConexao.Aguia)
        {

        }

        #region Query
        private static readonly string SQL_SELECT = @"SELECT rmc_cod_nao_confirmada as Codigo, 
            rmc_desc_nao_confirmada as Descricao 
                from res_motivo_nao_confirmada WHERE rmc_idc_ativo = 1";

        #endregion
        
        public List<MotivoNaoConfirmacao> ListarMotivos()
        {
            return Listar<MotivoNaoConfirmacao>(SQL_SELECT, null).ToList();
        }
    }
}
