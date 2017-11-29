Por favor verifique a URL: XXXXX (Video) para ver como habilitar a transa��o distribu�da

---------------------------------------------------------------------------------------------------------------------------
[ Transa��o Distribu�da com o Sybase ]
** Para configurar a transa��o � necess�rio do pacote Localiza.SDK.Configuracoes
---------------------------------------------------------------------------------------------------------------------------

Para trabalhar com transa��es � necess�rio seguir os passos abaixo:

1 - Decorar o M�todo do executor com o attribute [LocalizaTransacao] assim como o c�digo abaixo:

	public class RealizarReservaEmDuasTabelasEmBancosDiferentesSqlServer2005ESybase : IExecutor
	{
		[LocalizaTransacao]
		public void Executar()
		{
			var repositorio = new ReservaDataRepositorio();
			repositorio.SalvarSqlServer2005("646957");

			repositorio.SalvarAguia("646957");
		}
	}
	
2 - Para que a transa��o distribu�da funcione no Sybase � necess�rio seguir a seguinte conven��o:
  - Nas instru��es SQL do Sybase deve-se utilizar o "Full Path" <banco de dados>..<tabela>
  
  Exemplo de c�digo:
  
   public class ReservaDataRepositorio : IReservaDataRepositorio
    {
        public bool SalvarAguia(string codigoVeiculo)
        {
            const string QUERY = @"INSERT INTO aguia..reserva_data VALUES (@veh_num, @data)";

            using (var connection = ConnectionFactory.Instance.GetConnection("SybaseAguia"))
            {
                var data = DateTime.Now.ToString("ddMMyy");

                var parameters = new DynamicParameters();
                parameters.Add("veh_num", codigoVeiculo, DbType.AnsiString);
                parameters.Add("data", data, DbType.AnsiString);

                return connection.Execute(QUERY, parameters) > 0;
            }
        }

        public bool SalvarFrota(string codigoVeiculo)
        {
            const string QUERY = @"INSERT INTO frota..reserva_data VALUES (@veh_num, @data)";

            using (var connection = ConnectionFactory.Instance.GetConnection("SybaseFrota"))
            {
                var data = DateTime.Now.ToString("ddMMyy");

                var parameters = new DynamicParameters();
                parameters.Add("veh_num", codigoVeiculo, DbType.AnsiString);
                parameters.Add("data", data, DbType.AnsiString);

                return connection.Execute(QUERY, parameters) > 0;
            }
        }

3 - Toda transa��o ser� abortada se for levantada uma Exception ou se o Response da execu��o estiver com o estado Abortado 

---------------------------------------------------------------------------------------------------------------------------
[ Transfer�ncia de contexto de log para o TM ]
** Para configurar o TM � necess�rio do pacote Localiza.SDK.Configuracoes
---------------------------------------------------------------------------------------------------------------------------

Para trabalhar com contexto de logs para o Transaction Monitor � necess�rio seguir os passos abaixo:

1 - Colocar uma propridade nomeada "InformacoesLog" na Requisicao e no Executor. O Tipo da vari�vel pode ser qualquer um.

	public object InformacoesLog { get; set; }

2 - Decorar o m�todo Executar de cada executor envolvido com o atributo [LocalizaTm] assim como o c�digo abaixo:

	public class RealizarOperacaoComControleDeLogsDoTmExecutor : IExecutor
	{
		[LocalizaTm]
		public void Executar()
		{
			Log.Log("In�cio", InformacoesLog);

			try
			{
				//Executar primeira opera��o
				Log.Log("Sucesso - Opera��o 1", InformacoesLog);

				//Executar segunda opera��o
				Log.Log("Sucesso - Opera��o 2", InformacoesLog);
			}
			catch (Exception e)
			{
				Log.Log("Erro", InformacoesLog);	
			}

			Log.Log("Fim", InformacoesLog);
		}
	}

3 - A implementa��o da classe "Log" utilizada no exemplo acima � de responsabilidade do sistema que for utilizar.