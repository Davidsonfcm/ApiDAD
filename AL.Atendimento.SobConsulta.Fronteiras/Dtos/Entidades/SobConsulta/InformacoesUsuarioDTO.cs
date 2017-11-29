using AL.Atendimento.SobConsulta.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.SobConsulta
{
    public class InformacoesUsuarioDto : DtoBase<InformacoesUsuario, InformacoesUsuarioDto>
    {
        public string NomeUsuario { get; set; }
        public string CodigoUsuario { get; set; }
        public string EmailUsuario { get; set; }
    }
}
