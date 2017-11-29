using Localiza.SDK.AcessoDados;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL.Atendimento.SobConsulta.Testes.Scripts
{
    public static class CriarBancoAguia
    {
        public static void CriarTabelas(DbAccessHelper banco)
        {
            banco.Execute(File.ReadAllText("..//..//Scripts//Aguia//Create_res_sobconsulta_motivo.sql"), null);
            banco.Execute(File.ReadAllText("..//..//Scripts//Aguia//Create_res_sobconsulta_processa.sql"), null);
            banco.Execute(File.ReadAllText("..//..//Scripts//Aguia//Create_res_sobconsulta_ordena_grupo.sql"), null);
            banco.Execute(File.ReadAllText("..//..//Scripts//Aguia//Create_res_sobconsulta_upgrade.sql"), null);
            banco.Execute(File.ReadAllText("..//..//Scripts//Aguia//Create_res_sobconsulta.sql"), null);
            banco.Execute(File.ReadAllText("..//..//Scripts//Aguia//Create_res_sobconsulta_lock_processa.sql"), null);
        }

        public static void InserirDados(DbAccessHelper banco)
        {
            File.ReadAllLines("..//..//Scripts//Aguia//Insert_res_sobconsulta_motivo.sql").ToList().ForEach(sql => banco.Execute(sql, null));
            File.ReadAllLines("..//..//Scripts//Aguia//Insert_res_sobconsulta_ordena_grupo.sql").ToList().ForEach(sql => banco.Execute(sql, null));
        }
    }
}
