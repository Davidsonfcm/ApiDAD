using AL.Atendimento.SobConsulta.Executores.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using AL.Atendimento.SobConsulta.Repositorios.Reservas;
using AL.Atendimento.SobConsulta.Repositorios.Parametros;
using Localiza.SDK.Fronteira;
using Localiza.SDK.InversaoControle;
using System.Collections.Generic;
using AL.Atendimento.SobConsulta.Repositorios;
using AL.Atendimento.SobConsulta.Executores.Contrato;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.Email;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.Parametros;
using AL.Atendimento.SobConsulta.Executores.Parametros;
using AL.Atendimento.SobConsulta.Executores.Email;
using AL.Atendimento.SobConsulta.RepositoriosMock.Parametros;
using AL.Atendimento.SobConsulta.Repositorios.ProcessamentoAutomatico;
using AL.Atendimento.SobConsulta.Repositorios.RegraSaldo;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.ConfiguracoesGrupo;

namespace AL.Atendimento.SobConsulta.MapeamentosMock
{
    public static class MapeadorMock
    {
        public static Mapeamento[] Mapeamentos()
        {
            MapeadorDto.RegistrarMapeamentos();
            var listaMapeamentos = new List<Mapeamento>();

            #region Mapeamento de Executores

            listaMapeamentos.Add(new Mapeamento(typeof(IExecutorSemResultado<EnviarEmailErroRequisicao>), typeof(EnviarEmailErroExecutor)));
            listaMapeamentos.Add(new Mapeamento(typeof(IExecutor<ListarReservasSobConsultaRequisicao, ListarReservasSobConsultaResultado>), typeof(ListarReservasSobConsultaExecutor)));
            listaMapeamentos.Add(new Mapeamento(typeof(IExecutor<ObterReservaSobConsultaRequisicao, ObterReservaSobConsultaResultado>), typeof(ObterReservaSobConsultaExecutor)));
            listaMapeamentos.Add(new Mapeamento(typeof(IExecutor<ConfirmarReservaRequisicao, ConfirmarReservaResultado>), typeof(ConfirmarReservaExecutor)));
            listaMapeamentos.Add(new Mapeamento(typeof(IExecutor<ComunicacaoSobConsultaRequisicao, ComunicacaoSobConsultaResultado>), typeof(ComunicacaoSobConsultaExecutor)));
            listaMapeamentos.Add(new Mapeamento(typeof(IExecutor<ObterParametroSpocRequisicao, ObterParametroSpocResultado>), typeof(ObterParametroSpocExecutor)));
            listaMapeamentos.Add(new Mapeamento(typeof(IExecutor<AlterarReservaSobConsultaRequisicao, AlterarReservaSobConsultaResultado>), typeof(AlterarReservaSobConsultaExecutor)));
            listaMapeamentos.Add(new Mapeamento(typeof(IExecutor<BloquearReservaSobConsultaRequisicao, BloquearReservaSobConsultaResultado>), typeof(BloquearReservaSobConsultaExecutor)));
            listaMapeamentos.Add(new Mapeamento(typeof(IExecutor<DesbloquearReservaSobConsultaRequisicao, DesbloquearReservaSobConsultaResultado>), typeof(DesbloquearReservaSobConsultaExecutor)));
            listaMapeamentos.Add(new Mapeamento(typeof(IExecutor<ListarConfiguracoesGrupoRequisicao, ListarConfiguracoesGrupoResultado>), typeof(ListarConfiguracoesGrupoExecutor)));
            listaMapeamentos.Add(new Mapeamento(typeof(IExecutor<GravarConfiguracaoGrupoRequisicao, GravarConfiguracaoGrupoResultado>), typeof(GravarConfiguracoesGrupoExecutor)));


            #endregion Mapeamento de Executores

            #region Mapeamento de Repositorios

            // SERVICO
            listaMapeamentos.Add(new Mapeamento(typeof(IReservaNrRepositorio), typeof(ReservaNrRepositorioMock)));
            listaMapeamentos.Add(new Mapeamento(typeof(IReservaWsRepositorio), typeof(ReservaWsRepositorioMock)));
            listaMapeamentos.Add(new Mapeamento(typeof(IParametroSpocRepositorio), typeof(ParametroSpocRepositorioMock)));
            listaMapeamentos.Add(new Mapeamento(typeof(ICanaisWebRepositorio), typeof(CanaisWebRepositorioMock)));
            listaMapeamentos.Add(new Mapeamento(typeof(IEnviarEmailReportRepositorio), typeof(EnviarEmailReportRepositorioMock)));
            // BANCO DE DADOS
            listaMapeamentos.Add(new Mapeamento(typeof(IItensParametrosRepositorio), typeof(ItensParametrosRepositorioMock)));
            listaMapeamentos.Add(new Mapeamento(typeof(ILockSobConsultaRepositorio), typeof(LockSobConsultaRepositorioMock)));
            listaMapeamentos.Add(new Mapeamento(typeof(IConfiguracaoGruposRepositorio), typeof(ConfiguracaoGruposRepositorioMock)));

            #endregion Mapeamento de Repositorios

            return listaMapeamentos.ToArray();
        }

    }
}
