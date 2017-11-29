[ CACHE ]
Para a utilização do cache existem dois modos para fazê-lo, um deles através do carregamento via código e 
outro através de configuração do arquivo de configuração.

1 - O primeiro exemplo via código, após a instalação do pacote de cache que deseja executar, o código para a utilização do cache é:

		//Cache utilizando memória e o objeto a ser gravado no cache é um INT o cache tem o nome 'MeuCache'
		var cache = FabricaCache.Construir<int>(s => { s.UsarCacheEmMemoria("MeuCache"); });
        cache.Adicionar("Chave", 30); //Adicionando item no cache

		//Para appFabric
		var cache = FabricaCache.Construir<int>(s => { s.UsarAppFabric("MeuCache"); });
        cache.Adicionar("Chave", 30); //Adicionando item no cache

1.1 - Para efetuar a utilização do AppFabric ou MemoryCache, é necessário instalar Localiza.SDK.Cache.AppFabric ou Localiza.SDK.Cahce.Momery.

2 - Para fazer através de arquivo de configuração, o procedimento é o seguinte:
	- Adicionar a seção dentro do <configSections>
    <section name="cacheProvider" type="Localiza.SDK.Cache.Configuracao.CacheSection, Localiza.SDK.Cache" />

	E depois adicionar a configuração de qual provider será utilizado

	//Para AppFabric
	<cacheProvider>
		<Cache Handler="Localiza.SDK.Cache.AppFabric.AppFabricHandler`1, Localiza.SDK.Cache.AppFabric" />
	</cacheProvider>

	//Para memória
	<cacheProvider>
		<Cache Handler="Localiza.SDK.Cache.Memory.MemoryCacheHandler`1, Localiza.SDK.Cache.Memory" />
	</cacheProvider>

	E deve ser utilizado o seguinte código
	var cache = FabricaCache.BaseadoConfiguracao<int>("cacheProvider", "MeuCache");
    cache.Adicionar("Chave", 30); //Adicionando item no cache


3 - Para lembrar que o objeto cache é disposible, logo deve fazer dispose dele após o seu uso.

4 - E outra resolva é que para o appfabric, deve informar os endereços do servidor no arquivo de configuração. 
    Segue abaixo um exemplo, é sempre importante verificar com a infraestrutura.

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

