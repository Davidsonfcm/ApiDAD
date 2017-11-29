using System;
using System.Collections;
using System.Collections.Generic;

namespace AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.SobConsulta
{
    public class ConfiguracaoGrupoDto : DtoBase<Atendimento.SobConsulta.Entidades.ConfiguracaoGrupos, ConfiguracaoGrupoDto>
    {
        public string CodigoGrupo { get; set; }
        public int Ordenacao { get; set; }
        public List<string> ListaUpgrade { get; set; }
        public double MinimoProdutivo { get; set; }
        public double MaximoProdutivo { get; set; }
        public bool Ativo { get; set; }
        public String UsuarioLogado { get; set; }
    }
}
