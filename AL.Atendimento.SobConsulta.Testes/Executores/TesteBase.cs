using AL.Atendimento.SobConsulta.Base.Repositorios;
using AL.Atendimento.SobConsulta.MapeamentosMock;
using AL.Atendimento.SobConsulta.Testes.Scripts;
using iAnywhere.Data.UltraLite;
using Localiza.SDK.AcessoDados;
using Localiza.SDK.InversaoControle;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.IO;

namespace AL.Atendimento.SobConsulta.Testes
{
    [TestClass]
    public abstract class TesteBase
    {
        [AssemblyInitialize]
        public static void Inicializar(TestContext contexto)
        {
            ResolvedorDeDependencias.Instance().CarregarMapeamentos(MapeadorMock.Mapeamentos());

            CriarBanco("Aguia");
            AdoHelper.RegistrarFormatadorConsulta(new FormatadorConsultaUltraLite());

            using (DbAccessHelper aguia = AdoHelper.ObterConexao(ConexaoBanco.TipoConexao.Aguia))
            {
                CriarBancoAguia.CriarTabelas(aguia);
                CriarBancoAguia.InserirDados(aguia);
            }
        }

        private static void CriarBanco(string nomeBanco)
        {
            string arquivoPath = ConfigurationManager.ConnectionStrings[nomeBanco].ConnectionString.Replace("dbf=", "");
            if (File.Exists(arquivoPath))
                File.Delete(arquivoPath);
            ULDatabaseManager.CreateDatabase(ConfigurationManager.ConnectionStrings[nomeBanco].ConnectionString, "");
        }

        [AssemblyCleanup]
        public static void Finalizar()
        {
            string arquivoPath = ConfigurationManager.ConnectionStrings["Aguia"].ConnectionString.Replace("dbf=", "");
            if (File.Exists(arquivoPath))
                File.Delete(arquivoPath);
        }
    }
}
