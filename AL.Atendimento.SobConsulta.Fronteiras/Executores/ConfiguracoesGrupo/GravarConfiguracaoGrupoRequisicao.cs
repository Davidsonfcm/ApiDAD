using System;
using System.Collections.Generic;

namespace AL.Atendimento.SobConsulta.Fronteiras.Executores.ConfiguracoesGrupo
{
    public class GravarConfiguracaoGrupoRequisicao
    {
        public string CodigoGrupo { get; set; }
        public int Ordenacao { get; set; }
        public List<string> ListaUpgrade { get; set; }
        public double MinimoProdutivo { get; set; }
        public double MaximoProdutivo { get; set; }
        public bool Ativo { get; set; }
        public string UsuarioLogado { get; set; }
    }
}
