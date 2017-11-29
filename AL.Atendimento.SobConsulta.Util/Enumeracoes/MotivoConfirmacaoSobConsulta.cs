using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Util.Enumeracoes
{
    public enum MotivoConfirmacaoSobConsulta
    {
        ConfirmacaoGrupoCluster = 1,
        ConfirmacaoUpgradeCluster = 2,
        ConfirmacaoGrupoFilial = 3,
        ConfirmacaoUpgradeFilial = 4,
        NaoConfirmacaoGrupoPonta = 5,
        NaoConfirmacaoGrupoUpgrade = 6,
        AguardandoConfirmacaoManual = 7,
        AtendidaManualmente = 8,
        GrupoInativo = 9,
    }
}
