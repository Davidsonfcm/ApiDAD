using Localiza.SDK.Fronteira;
using Localiza.SDK.InversaoControle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Testes.Executores
{
    public abstract class TesteExecutorBase<Requisicao, Resultado> : TesteBase where Resultado : IResultado 
    {
        protected Resultado ChamarExecutor(Requisicao requisicao)
        {
            return ResolvedorDeDependencias.Instance().ObterInstanciaDe<IExecutor<Requisicao, Resultado>>().Executar(requisicao);
        }
    }
}
