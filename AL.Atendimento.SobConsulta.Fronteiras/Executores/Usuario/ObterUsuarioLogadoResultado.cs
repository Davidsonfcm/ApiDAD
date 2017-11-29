using AL.Atendimento.SobConsulta.Entidades;
using AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.SobConsulta;
using Localiza.SDK.Fronteira;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Fronteiras.Executores.Usuario
{
    public class ObterUsuarioLogadoResultado : IResultado
    {
        public InformacoesUsuarioDto UsuarioLogado{ get; set;}
        public EstadoResultado Estado { get; set; }
    }
}
