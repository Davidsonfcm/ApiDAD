using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Executores.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.Email;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.Parametros;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.SobConsulta;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using AL.Atendimento.SobConsulta.Util;
using AL.Atendimento.SobConsulta.Util.Enumeracoes;
using AL.Atendimento.SobConsulta.Util.Excecoes;
using Localiza.SDK.Fronteira;
using Localiza.SDK.InversaoControle;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Executores.Contrato
{
    public class ConfirmarReservaExecutor : IExecutor<ConfirmarReservaRequisicao, ConfirmarReservaResultado>
    {
        private readonly IConfirmacaoReservaSobConsultaRepositorio confirmacaoReservaSobConsultaRepositorio;
        private readonly ILockSobConsultaRepositorio lockSobConsutaRepositorio;

        public ConfirmarReservaExecutor(IConfirmacaoReservaSobConsultaRepositorio confirmacaoReservaSobConsultaRepositorio, ILockSobConsultaRepositorio lockSobConsutaRepositorio)
        {
            this.confirmacaoReservaSobConsultaRepositorio = confirmacaoReservaSobConsultaRepositorio;
            this.lockSobConsutaRepositorio = lockSobConsutaRepositorio;
        }

        public ConfirmarReservaResultado Executar(ConfirmarReservaRequisicao requisicao)
        {
            Reserva dadosReservaNr = null;

            if (requisicao != null)
            {
                VerificarSeReservaPodeSerConfirmada(requisicao);
                var gravacaoResultado = GravarAlteracoesDaReserva(requisicao);

                ConfirmarReservaSobConsulta(requisicao);

                EnviarEmailReservaAssincrono(requisicao, gravacaoResultado.DadosReserva, gravacaoResultado.DadosReservaNr);

                dadosReservaNr = gravacaoResultado.DadosReservaNr;
            }

            return new ConfirmarReservaResultado
            {
                Reserva = ReservaDto.CriarAPartirDeEntidade(dadosReservaNr),
                Estado = EstadoResultado.OK
            };
        }

        private void VerificarSeReservaPodeSerConfirmada(ConfirmarReservaRequisicao requisicao)
        {
            ValidarParametrosNulos(requisicao);

            LockSobConsulta bloqueioReserva = lockSobConsutaRepositorio.ObterBloqueio(requisicao.Localizador);

            if (bloqueioReserva != null && bloqueioReserva.UsuarioLock != null && bloqueioReserva.UsuarioLock.CodigoUsuario != requisicao.CodigoUsuario)
                throw new NegocioException("Reserva bloqueada para outro usuário.", CodigosErro.RESERVA_BLOQUEADA_POR_OUTRO_USUARIO);
        }

        private static void ValidarParametrosNulos(ConfirmarReservaRequisicao requisicao)
        {
            if (String.IsNullOrEmpty(requisicao.CodigoUsuario))
            {
                throw new ParametroNuloException("CodigoUsuario");
            }
            if (String.IsNullOrEmpty(requisicao.Localizador))
            {
                throw new ParametroNuloException("Localizador");
            }
            if (String.IsNullOrEmpty(requisicao.Situacao))
            {
                throw new ParametroNuloException("Situacao");
            }
            if (requisicao.Situacao == SituacaoReserva.NaoConfirmada && (String.IsNullOrEmpty(requisicao.MotivoConfirmacao.ToString()) && String.IsNullOrEmpty(requisicao.MotivoNaoConfirmada)))

            {
                throw new ParametroNuloException("MotivoNaoConfirmada");
            }
            if (requisicao.Situacao == SituacaoReserva.Cancelada && (String.IsNullOrEmpty(requisicao.MotivoConfirmacao.ToString()) && String.IsNullOrEmpty(requisicao.MotivoCancelamento)))
            {
                throw new ParametroNuloException("MotivoCancelamento");
            }
        }

        private void EnviarEmailReserva(ConfirmarReservaRequisicao requisicao, DadosComunicacao dadosComunicacao, Reserva dadosReservaNR)
        {
            try
            {
                ComunicacaoSobConsultaRequisicao requisicaoComunicacao = new ComunicacaoSobConsultaRequisicao();
                requisicaoComunicacao.DadosComunicacaoReserva = dadosComunicacao;
                requisicaoComunicacao.CodigoUsuario = requisicao.CodigoUsuario;
                requisicaoComunicacao.DadosReservaNR = dadosReservaNR;

                var resultado = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IExecutor<ComunicacaoSobConsultaRequisicao, ComunicacaoSobConsultaResultado>>().Executar(requisicaoComunicacao);
            }
            catch (Exception e)
            {
                var executor = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IExecutorSemResultado<EnviarEmailErroRequisicao>>();

                EnviarEmailErroRequisicao requisicaoEmail = new EnviarEmailErroRequisicao();
                requisicaoEmail.Erro = e;
                requisicaoEmail.Parametros = requisicao.Localizador;

                executor.Executar(requisicaoEmail);
            }
        }

        private void EnviarEmailReservaAssincrono(ConfirmarReservaRequisicao requisicao, DadosComunicacao dadosComunicacao, Reserva dadosReservaNR)
        {
            Task taskEmail = new Task(() => EnviarEmailReserva(requisicao, dadosComunicacao, dadosReservaNR));
            taskEmail.Start();
        }

        private AlterarReservaSobConsultaResultado GravarAlteracoesDaReserva(ConfirmarReservaRequisicao requisicao)
        {
            AlterarReservaSobConsultaRequisicao requisicaoAlteracaoReserva = new AlterarReservaSobConsultaRequisicao();

            requisicaoAlteracaoReserva.Localizador = requisicao.Localizador;
            requisicaoAlteracaoReserva.Observacao = requisicao.Observacao;
            requisicaoAlteracaoReserva.SituacaoReserva = requisicao.Situacao;
            requisicaoAlteracaoReserva.CodigoUsuario = requisicao.CodigoUsuario;
            requisicaoAlteracaoReserva.LocalizadorExterno = requisicao.LocalizadorExterno;
            requisicaoAlteracaoReserva.MotivoCancelamento = requisicao.MotivoCancelamento;
            requisicaoAlteracaoReserva.MotivoNaoConfirmada = requisicao.MotivoNaoConfirmada;
            requisicaoAlteracaoReserva.EnviarEmail = requisicao.EnviarEmail;
            requisicaoAlteracaoReserva.EnviarSMS = requisicao.EnviarSMS;

            AlterarReservaSobConsultaResultado resultado = ResolvedorDeDependencias.Instance().ObterInstanciaDe<IExecutor<AlterarReservaSobConsultaRequisicao, AlterarReservaSobConsultaResultado>>().Executar(requisicaoAlteracaoReserva);

            return resultado;
        }

        private void ConfirmarReservaSobConsulta(ConfirmarReservaRequisicao requisicao)
        {
            if (requisicao != null && requisicao.ConfirmacaoAutomatica != null)
            {
                ConfirmarReservaSobConsultaAutomaticamente(requisicao);
            }
            else
            {
                ConfirmarReservaSobConsultaManualmente(requisicao);
            }
        }

        private void ConfirmarReservaSobConsultaManualmente(ConfirmarReservaRequisicao requisicao)
        {
            GravarConfirmacaoReserva(requisicao.Localizador, requisicao.MotivoConfirmacao, null, null, null, null, null, null, requisicao.CodigoUsuario);
        }

        private void ConfirmarReservaSobConsultaAutomaticamente(ConfirmarReservaRequisicao requisicao)
        {
            GravarConfirmacaoReserva(requisicao.Localizador, requisicao.MotivoConfirmacao, requisicao.ConfirmacaoAutomatica.QtdFrotaOriginal, requisicao.ConfirmacaoAutomatica.QtdFrotaProcessada,
                requisicao.ConfirmacaoAutomatica.QtdFrotaAlugadaOriginal, requisicao.ConfirmacaoAutomatica.QtdFrotaAlugadaProcessada,
                requisicao.ConfirmacaoAutomatica.GrupoConfirmacao, requisicao.ConfirmacaoAutomatica.idProcessamentoAutomatico, requisicao.CodigoUsuario);
        }

        private void GravarConfirmacaoReserva(String localizador, MotivoConfirmacaoSobConsulta motivo, double? QtdFrotaOriginal, double? QtdFrotaProcessada,
            double? QtdFrotaAlugadaOriginal, double? QtdFrotaAlugadaProcessada, String GrupoConfirmacao, int? IdProcessamentoAutomatico,
                String CodigoUsuarioAlteracao)
        {
            var confirmacaoreserva = confirmacaoReservaSobConsultaRepositorio.Obter(localizador);

            if (confirmacaoreserva == null)
            {
                confirmacaoreserva = new Entidades.ConfirmacaoSobConsulta()
                {
                    Localizador = localizador,
                    MotivoConfirmacao = motivo,
                    QtdFrotaOriginal = QtdFrotaOriginal,
                    QtdFrotaProcessada = QtdFrotaProcessada,
                    QtdFrotaAlugadaOriginal = QtdFrotaAlugadaOriginal,
                    QtdFrotaAlugadaProcessada = QtdFrotaAlugadaProcessada,
                    GrupoConfirmacao = GrupoConfirmacao,
                    IdProcessamentoAutomatico = IdProcessamentoAutomatico,
                    DataCriacao = DateTime.Now,
                    DataAlteracao = DateTime.Now,
                    CodigoUsuarioAlteracao = CodigoUsuarioAlteracao,
                    QtdProcessamentos = 1
                };

                confirmacaoReservaSobConsultaRepositorio.CriarConfirmacaoReserva(confirmacaoreserva);
            }
            else
            {
                confirmacaoreserva.DataAlteracao = DateTime.Now;
                confirmacaoreserva.CodigoUsuarioAlteracao = CodigoUsuarioAlteracao;
                confirmacaoreserva.QtdProcessamentos = !confirmacaoreserva.QtdProcessamentos.HasValue ? 1 : (confirmacaoreserva.QtdProcessamentos + 1);
                confirmacaoreserva.MotivoConfirmacao = motivo;
                confirmacaoreserva.QtdFrotaOriginal = QtdFrotaOriginal;
                confirmacaoreserva.QtdFrotaProcessada = QtdFrotaProcessada;
                confirmacaoreserva.QtdFrotaAlugadaOriginal = QtdFrotaAlugadaOriginal;
                confirmacaoreserva.QtdFrotaAlugadaProcessada = QtdFrotaAlugadaProcessada;
                confirmacaoreserva.GrupoConfirmacao = GrupoConfirmacao;
                confirmacaoreserva.IdProcessamentoAutomatico = IdProcessamentoAutomatico;

                confirmacaoReservaSobConsultaRepositorio.AtualizarConfirmacaoReserva(confirmacaoreserva);
            }
        }
    }
}
