using AL.Atendimento.SobConsulta.Base.Repositorios;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using AL.Atendimento.SobConsulta.Repositorios.Entidade;
using LockSobConsultaEntidade = AL.Atendimento.SobConsulta.Entidades.LockSobConsulta;
using AL.Atendimento.SobConsulta.Util;
using Dapper;
using AL.Atendimento.SobConsulta.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AL.Atendimento.SobConsulta.Repositorios.ProcessamentoAutomatico
{
    public class LockSobConsultaRepositorioMock : RepositorioBase<LockSobConsultaEntidade>, ILockSobConsultaRepositorio
    {
        public LockSobConsultaRepositorioMock() : base(ConexaoBanco.TipoConexao.Aguia)
        {
        }

        private const string SQL_INSERT = @"INSERT INTO res_sobconsulta_lock_processa(res_num, cd_usuario_lock, dt_lock)
                                                VALUES (@localizador, @usuarioBloqueio, getdate())";

        private const string SQL_SELECT = @"SELECT res_num as Localizador, cd_usuario_lock as CodigoUsuario, dt_lock as DataLock 
                                            FROM res_sobconsulta_lock_processa where res_num = @localizador";

        private const string SQL_DESBLOQUEIO = @"DELETE res_sobconsulta_lock_processa where res_num = @localizador";

        private const string SQL_SELECT_RESERVAS_BLOQUEADAS = @"SELECT res_num as Localizador, cd_usuario_lock as CodigoUsuario, dt_lock as DataLock
        FROM res_sobconsulta_lock_processa 
        WHERE res_num IN @reservas";

        public List<LockSobConsultaEntidade> VerificarSeReservasEstaoBloqueadas(string[] reservas)
        {
            if (reservas != null)
            {
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@reservas", reservas);

                return Listar<LockSobConsultaEntidade>(SQL_SELECT_RESERVAS_BLOQUEADAS, parametros).ToList();
            }
            return null;
        }

        public bool BloquearReserva(string localizador, string usuarioBloqueio)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@localizador", localizador);
            parametros.Add("@usuarioBloqueio", usuarioBloqueio);

            Executar(SQL_INSERT, parametros);

            return true;
        }

        public LockSobConsultaEntidade Obter(string localizador)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@localizador", localizador);

            return Obter(SQL_SELECT, parametros);
        }

        public bool DesbloquearReserva(string localizador, string usuarioBloqueio, bool forcarDesbloqueio)
        {
            var bloqueio = Obter(localizador);

            if (bloqueio == null)
                return true;

            if (bloqueio.UsuarioLock != null && bloqueio.UsuarioLock.CodigoUsuario == usuarioBloqueio || forcarDesbloqueio)
            {
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@localizador", localizador);
                Executar(SQL_DESBLOQUEIO, parametros);

                return true;
            }

            return false;
        }

        public LockSobConsultaEntidade ObterBloqueio(string localizador)
        {
            return Obter(localizador);
        }
    }
}
