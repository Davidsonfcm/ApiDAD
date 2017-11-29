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
    public class LockSobConsultaRepositorio : RepositorioBase<LockSobConsultaEntidade>, ILockSobConsultaRepositorio
    {
        public LockSobConsultaRepositorio() : base(ConexaoBanco.TipoConexao.Aguia)
        {
        }

        private const string SQL_INSERT = @"INSERT INTO res_sobconsulta_lock_processa(res_num, cd_usuario_lock, dt_lock)
                                                VALUES (@localizador, @usuarioBloqueio, getdate())";

        private const string SQL_SELECT = @"SELECT res_num as Localizador, dt_lock as DataLock, cd_usuario_lock as CodigoUsuario, name as NomeUsuario
                                                FROM res_sobconsulta_lock_processa
                                                INNER JOIN users
                                                ON users.id = res_sobconsulta_lock_processa.cd_usuario_lock where res_num = @localizador";

        private const string SQL_DESBLOQUEIO = @"DELETE res_sobconsulta_lock_processa where res_num = @localizador";

        private const string SQL_SELECT_RESERVAS_BLOQUEADAS = @"SELECT res_num as Localizador, dt_lock as DataLock, cd_usuario_lock as CodigoUsuario, name as NomeUsuario
                                                                    FROM res_sobconsulta_lock_processa 
                                                                        INNER JOIN users
                                                                        ON users.id = res_sobconsulta_lock_processa.cd_usuario_lock 
                                                                    WHERE res_num IN @reservas";

        public List<LockSobConsultaEntidade> VerificarSeReservasEstaoBloqueadas(string[] reservas)
        {
            if (reservas != null)
            {
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@reservas", reservas);

                var bloqueiosReserva = QueryComListaFilho<LockSobConsultaEntidade, UsuarioLock>(SQL_SELECT_RESERVAS_BLOQUEADAS, parametros, new string[] { "CodigoUsuario" }, MetodoFilho);
                return bloqueiosReserva.ToList();
            }
            return null;
        }

        public LockSobConsultaEntidade MetodoFilho(LockSobConsultaEntidade lockSobConsulta, UsuarioLock usuarioLock)
        {
            lockSobConsulta.UsuarioLock = usuarioLock;
            return lockSobConsulta;
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

            var bloqueioReserva = QueryComListaFilho<LockSobConsultaEntidade, UsuarioLock>(SQL_SELECT, parametros, new string[] { "CodigoUsuario" }, MetodoFilho);

            return bloqueioReserva.ToList() != null && bloqueioReserva.ToList().Count > 0 ? bloqueioReserva.ToList().FirstOrDefault() : null;
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
