using System;

namespace AL.Atendimento.SobConsulta.Fronteiras.Executores.Email
{
    public class EnviarEmailErroRequisicao
    {
        public Exception Erro { get; set; }
        public String Parametros { get; set; }
    }
}
