[ CACHE ]
Para a utiliza��o do cache existem dois modos para faz�-lo, um deles atrav�s do carregamento via c�digo e 
outro atrav�s de configura��o do arquivo de configura��o.

1 - O primeiro exemplo via c�digo, ap�s a instala��o do pacote de cache que deseja executar, o c�digo para a utiliza��o do cache �:

		//Cache utilizando mem�ria e o objeto a ser gravado no cache � um INT o cache tem o nome 'MeuCache'
		var cache = FabricaCache.Construir<int>(s => { s.UsarCacheEmMemoria("MeuCache"); });
        cache.Adicionar("Chave", 30); //Adicionando item no cache

		//Para appFabric
		var cache = FabricaCache.Construir<int>(s => 
		{ 
		        s.UsarAppFabric(new AppFabricCacheOptions
                {
                    Nome = "SDKCachePoc"
                });
		 });
        cache.Adicionar("Chave", 30); //Adicionando item no cache

1.1 - Para efetuar a utiliza��o do AppFabric ou MemoryCache, � necess�rio instalar Localiza.SDK.Cache.AppFabric ou Localiza.SDK.Cahce.Momery.

2 - Para fazer atrav�s de arquivo de configura��o, o procedimento � o seguinte:
	- Adicionar a se��o dentro do <configSections>
    <section name="cacheProvider" type="Localiza.SDK.Cache.Configuracao.CacheSection, Localiza.SDK.Cache" />

	E depois adicionar a configura��o de qual provider ser� utilizado

	//Para AppFabric
	<cacheProvider>
		<Cache Handler="Localiza.SDK.Cache.AppFabric.AppFabricHandler`1, Localiza.SDK.Cache.AppFabric" />
	</cacheProvider>

	//Para mem�ria
	<cacheProvider>
		<Cache Handler="Localiza.SDK.Cache.Memory.MemoryCacheHandler`1, Localiza.SDK.Cache.Memory" />
	</cacheProvider>

	E deve ser utilizado o seguinte c�digo
	var cache = FabricaCache.BaseadoConfiguracao<int>("cacheProvider", "MeuCache");
    cache.Adicionar("Chave", 30); //Adicionando item no cache


3 - Para lembrar que o objeto cache � disposible, logo deve fazer dispose dele ap�s o seu uso.

4 - E outra resolva � que para o appfabric, deve informar os endere�os do servidor no arquivo de configura��o. 
    Segue abaixo um exemplo, � sempre importante verificar com a infraestrutura.

	<section name="dataCacheClient"
          type="Microsoft.ApplicationServer.Caching.DataCacheClientSection,
            Microsoft.ApplicationServer.Caching.Core, Version=1.0.0.0, 
            Culture=neutral, PublicKeyToken=31bf3856ad364e35"
           allowLocation="true"
           allowDefinition="Everywhere"/>

	<dataCacheClient requestTimeout="15000" channelOpenTimeout="6000" maxConnectionsToServer="1">
		<hosts>
			<host name="srv-appfbcdev01" cachePort="22233"/>
			<host name="srv-appfbcdev02" cachePort="22233"/>
		</hosts>
	</dataCacheClient>

