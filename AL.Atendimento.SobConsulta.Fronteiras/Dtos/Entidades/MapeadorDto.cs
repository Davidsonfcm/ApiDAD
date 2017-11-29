
using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.SobConsulta;
using AL.Atendimento.SobConsulta.Entidades.Parametros;

namespace AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades
{
    public static class MapeadorDto
    {
        internal static TDestino Mapear<TDestino>(object entidade)
        {
            if (entidade == null)
            {
                return default(TDestino);
            }
            return AutoMapper.Mapper.Map<TDestino>(entidade);
        }

        public static void RegistrarMapeamentos()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<TelefoneCliente, TelefoneClienteDto>();
                cfg.CreateMap<HorarioFuncionamento, HorarioFuncionamentoDto>();
                cfg.CreateMap<ReservasProcessadas,ReservasProcessadasDto>();
                cfg.CreateMap<ReservaSobConsulta, ReservaSobConsultaDto>();
                cfg.CreateMap<Reserva, ReservaDto>();
                cfg.CreateMap<ParametroSpoc, Parametros.ParametroSpocDto>();
                cfg.CreateMap<ConfiguracaoGrupos, ConfiguracaoGrupoDto>();
                cfg.CreateMap<MotivoCancelamento, MotivoCancelamentoDto>();
                cfg.CreateMap<MotivoNaoConfirmacao, MotivoNaoConfirmacaoDto>();
                cfg.CreateMap<UsuarioLock, UsuarioLockDto>();
                cfg.CreateMap<InformacoesUsuario, InformacoesUsuarioDto>();
            });
        }
    }
}
