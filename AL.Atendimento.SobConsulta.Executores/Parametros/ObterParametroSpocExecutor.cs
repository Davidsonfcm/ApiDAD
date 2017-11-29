using AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades.Parametros;
using AL.Atendimento.SobConsulta.Fronteiras.Executores.Parametros;
using AL.Atendimento.SobConsulta.Fronteiras.Repositorios;
using Localiza.SDK.Fronteira;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Executores.Parametros
{
    public class ObterParametroSpocExecutor : IExecutor<ObterParametroSpocRequisicao, ObterParametroSpocResultado>
    {
        private readonly IParametroSpocRepositorio parametroSpocRepositorio;


        public ObterParametroSpocExecutor(IParametroSpocRepositorio parametroSpocRepositorio)
        {
            this.parametroSpocRepositorio = parametroSpocRepositorio;
        }

        public ObterParametroSpocResultado Executar(ObterParametroSpocRequisicao requisicao)
        {
            return new ObterParametroSpocResultado() { Parametro = ParametroSpocDto.CriarAPartirDeEntidade(parametroSpocRepositorio.ObterParametros(requisicao.NomeParametro)) };
        }
    }
}
