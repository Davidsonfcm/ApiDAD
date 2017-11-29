Por favor verifique a URL: XXXXX (Video) para ver como habilitar a transação distribuída

---------------------------------------------------------------------------------------------------------------------------
[ Transação Distribuída com o Sybase ]
** Para configurar a transação é necessário do pacote Localiza.SDK.Configuracoes
---------------------------------------------------------------------------------------------------------------------------

Para trabalhar com transações é necessário seguir os passos abaixo:

1 - Decorar o Método do executor com o attribute [LocalizaTransacao] assim como o código abaixo:

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
	
2 - Para que a transação distribuída funcione no Sybase é necessário seguir a seguinte convenção:
  - Nas instruções SQL do Sybase deve-se utilizar o "Full Path" <banco de dados>..<tabela>
  
  Exemplo de código:
  
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

3 - Toda transação será abortada se for levantada uma Exception ou se o Response da execução estiver com o estado Abortado 

---------------------------------------------------------------------------------------------------------------------------
[ Transferência de contexto de log para o TM ]
** Para configurar o TM é necessário do pacote Localiza.SDK.Configuracoes
---------------------------------------------------------------------------------------------------------------------------

Para trabalhar com contexto de logs para o Transaction Monitor é necessário seguir os passos abaixo:

1 - Colocar uma propridade nomeada "InformacoesLog" na Requisicao e no Executor. O Tipo da variável pode ser qualquer um.

	public object InformacoesLog { get; set; }

2 - Decorar o método Executar de cada executor envolvido com o atributo [LocalizaTm] assim como o código abaixo:

	public class RealizarOperacaoComControleDeLogsDoTmExecutor : IExecutor
	{
		[LocalizaTm]
		public void Executar()
		{
			Log.Log("Início", InformacoesLog);

			try
			{
				//Executar primeira operação
				Log.Log("Sucesso - Operação 1", InformacoesLog);

				//Executar segunda operação
				Log.Log("Sucesso - Operação 2", InformacoesLog);
			}
			catch (Exception e)
			{
				Log.Log("Erro", InformacoesLog);	
			}

			Log.Log("Fim", InformacoesLog);
		}
	}

3 - A implementação da classe "Log" utilizada no exemplo acima é de responsabilidade do sistema que for utilizar.