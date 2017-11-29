using AL.Atendimento.SobConsulta.Base.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AL.Atendimento.SobConsulta.Base.Repositorios.ConexaoBanco;

namespace AL.Atendimento.SobConsulta.Repositorios.Reservas
{
    public class CanaisWebRepositorio : RepositorioBase<CanaisWebEntidade>, ICanaisWebRepositorio
    {

        #region Constantes
        
        #endregion

        #region Construtores
        public CanaisWebRepositorio() : base(TipoConexao.Aguia) { }
        #endregion

        #region Metodos Publicos
        public void EnviarEmailConfirmacaoReservaComListaDestinatarios(string localizador, string[] listaDestinatarios, string codigoEmissor)
        {
            using (wsCanaisWeb.BasicHttpBinding_ICanaisWeb servico = new wsCanaisWeb.BasicHttpBinding_ICanaisWeb())
            {
                servico.EnviarEmailConfirmacaoReservaComListaDestinatarios(localizador,
                    codigoEmissor, listaDestinatarios.ToArray());
            }
        }
        #endregion

        #region Metodos Privados

        #endregion
    }
}
