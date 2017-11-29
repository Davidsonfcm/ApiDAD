using System;
using System.Collections;
using System.Collections.Generic;

namespace AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.SobConsulta
{
    public class UsuarioLockDto : DtoBase<Atendimento.SobConsulta.Entidades.UsuarioLock, UsuarioLockDto>
    {
        public String CodigoUsuario { get; set; }
        public String NomeUsuario { get; set; }
    }
}
