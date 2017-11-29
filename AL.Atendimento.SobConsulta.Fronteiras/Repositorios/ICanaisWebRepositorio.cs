namespace AL.Atendimento.SobConsulta.Repositorios.Reservas
{
    public interface ICanaisWebRepositorio
    {
        void EnviarEmailConfirmacaoReservaComListaDestinatarios(string localizador, string[] listaDestinatarios, string codigoEmissor);
    }
}