using AL.Atendimento.SobConsulta.Executores;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using Localiza.SDK.Fronteira;
using Localiza.SDK.InversaoControle;
using System.Collections.Generic;
using AL.Atendimento.SobConsulta.Repositorios.Reservas;
using AL.Atendimento.SobConsulta.Repositorios.Parametros;
using AL.Atendimento.SobConsulta.Executores.Contrato;
using AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.SobConsulta;
using AL.Atendimento.SobConsulta.Repositorios.Email;
using AL.Atendimento.SobConsulta.Executores.Email;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.Email;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.Parametros;
using AL.Atendimento.SobConsulta.Executores.Parametros;
using AL.Atendimento.SobConsulta.Executores.SobConsulta;
using AL.Atendimento.SobConsulta.Repositorios.RegraSaldo;
using AL.Atendimento.SobConsulta.Repositorios.ProcessamentoAutomatico;
using AL.Atendimento.SobConsulta.Repositorios;
using AL.Atendimento.SobConsulta.Repositorios.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.ConfiguracoesGrupo;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.Usuario;
using AL.Atendimento.SobConsulta.Executores.Usuario;
using AL.Atendimento.SobConsulta.Repositorios.Usuario;
using AL.Atendimento.SobConsulta.Repositorios.OperacoesService;

namespace AL.Atendimento.SobConsulta.Mapeamentos
{
    public static class Mapeador
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
            listaMapeamentos.Add(new Mapeamento(typeof(IExecutor<ListarReservasProcessadasRequisicao, ListarReservasProcessadasResultado>), typeof(ListarReservasProcessadasExecutor)));
            listaMapeamentos.Add(new Mapeamento(typeof(IExecutorSemRequisicao<ListarMotivosCancelamentoResultado>), typeof(ListarMotivosCancelamentoExecutor)));
            listaMapeamentos.Add(new Mapeamento(typeof(IExecutorSemRequisicao<ListarMotivosNaoConfirmacaoResultado>), typeof(ListarMotivosNaoConfirmacaoExecutor)));
            listaMapeamentos.Add(new Mapeamento(typeof(IExecutor<ObterUsuarioLogadoRequisicao, ObterUsuarioLogadoResultado>), typeof(ObterUsuarioLogadoExecutor)));

            #endregion Mapeamento de Executores

            #region Mapeamento de Repositorios

            // SERVICO

            listaMapeamentos.Add(new Mapeamento(typeof(IReservaNrRepositorio), typeof(ReservaNrRepositorio)));
            listaMapeamentos.Add(new Mapeamento(typeof(IEmailRepositorio), typeof(EmailRepositorio)));
            listaMapeamentos.Add(new Mapeamento(typeof(IParametroSpocRepositorio), typeof(ParametroSpocRepositorio)));
            listaMapeamentos.Add(new Mapeamento(typeof(IConfiguracaoGruposRepositorio), typeof(ConfiguracaoGruposRepositorio)));
            listaMapeamentos.Add(new Mapeamento(typeof(IReservaWsRepositorio), typeof(ReservaWsRepositorio)));
            listaMapeamentos.Add(new Mapeamento(typeof(ICanaisWebRepositorio), typeof(CanaisWebRepositorio)));
            listaMapeamentos.Add(new Mapeamento(typeof(IEnviarEmailReportRepositorio), typeof(EnviarEmailReportRepositorio)));
            listaMapeamentos.Add(new Mapeamento(typeof(IReservasProcessadasRepositorio), typeof(ReservasProcessadasRepositorio)));
            listaMapeamentos.Add(new Mapeamento(typeof(IOperacoesServiceRepositorio), typeof(OperacoesServiceRepositorio)));
            // BANCO DE DADOS
            listaMapeamentos.Add(new Mapeamento(typeof(IItensParametrosRepositorio), typeof(ItensParametrosRepositorio)));
            listaMapeamentos.Add(new Mapeamento(typeof(IConfirmacaoReservaSobConsultaRepositorio), typeof(ConfirmacaoReservaSobConsultaRepositorio)));
            listaMapeamentos.Add(new Mapeamento(typeof(ILockSobConsultaRepositorio), typeof(LockSobConsultaRepositorio)));
            listaMapeamentos.Add(new Mapeamento(typeof(IMotivoCancelamentoRepositorio), typeof(MotivoCancelamentoRepositorio)));
            listaMapeamentos.Add(new Mapeamento(typeof(IMotivoNaoConfirmacaoRepositorio), typeof(MotivoNaoConfirmacaoRepositorio)));
            listaMapeamentos.Add(new Mapeamento(typeof(IInformacoesUsuarioLogadoRepositorio), typeof(InformacoesUsuarioLogadoRepositorio)));

            #endregion Mapeamento de Repositorios

            return listaMapeamentos.ToArray();
        }

    }
}
