using AL.Atendimento.SobConsulta.Base.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Entidades
{
    public class DadosEnvioEmailCeres : IEntidade
    {
        
        public string Localizador { get; set; }
        public bool EnviarEmail { get; set; }
        public string EmailRequisitante { get; set; }
        public string DestinatarioEmail { get; set; }
        public bool Alteracao { get; set; }
   
        public IChaveEntidade ObterChave()
        {
            throw new NotImplementedException();
        }
    }
}
