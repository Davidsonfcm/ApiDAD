CREATE TABLE res_sobconsulta_processa
	(
	id_processamento          INT NOT NULL,
	dt_inicio_processamento   DATETIME NOT NULL,
	dt_fim_processamento      DATETIME NOT NULL,
	qtd_registros_processados INT NOT NULL,
	qtd_registros_confirmados INT DEFAULT 0 NOT NULL,
	qtd_registros_negados     INT DEFAULT 0 NOT NULL,
	dt_ultimo_processo_bi     DATETIME NOT NULL,
	CONSTRAINT pk_res_sobconsulta_processa PRIMARY KEY (id_processamento)
	)
