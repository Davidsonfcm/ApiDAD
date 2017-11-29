using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Base.Repositorios;
using Dapper;

namespace AL.Atendimento.SobConsulta.Repositorios.Usuario
{
    public class InformacoesUsuarioLogadoRepositorio : RepositorioBase<Entidades.InformacoesUsuario>, IInformacoesUsuarioLogadoRepositorio
    {
        #region Construtor
        public InformacoesUsuarioLogadoRepositorio() : base(ConexaoBanco.TipoConexao.Aguia)
        {
        }
        #endregion
        #region Query
        private static readonly string SQL_OBTER = @"
            SELECT name AS NomeUsuario, 
                   email AS EmailUsuario, 
                   id AS CodigoUsuario
            FROM 
                users 
            WHERE 
                id = @CodigoUsuarioLogado 
        ";
        #endregion
        #region Metodos Públicos
        public InformacoesUsuario ObterUsuarioLogado(string CodigoUsuarioLogado)
        {

            if (CodigoUsuarioLogado != null)
            {
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@CodigoUsuarioLogado", CodigoUsuarioLogado, TipoParametro.StringComTamanhoVariavel);

                Executar(SQL_OBTER, parametros);
                return Obter(SQL_OBTER, parametros);
            }
            else
                return null;

        }
        #endregion

    }
}
