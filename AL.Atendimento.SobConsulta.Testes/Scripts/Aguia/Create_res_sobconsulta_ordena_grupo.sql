CREATE TABLE res_sobconsulta_ordena_grupo
	(
	rate_group               VARCHAR (2) NOT NULL,
	num_ordena               INT NOT NULL,
	idc_status               BIT DEFAULT 1 NOT NULL,
	dt_ultima_alteracao      DATETIME NOT NULL,
	cd_responsavel_alteracao CHAR (6) NOT NULL,
	vlr_faixa_minima         DECIMAL (3,1) NULL,
	vlr_faixa_maxima         DECIMAL (3,1) NULL,
	CONSTRAINT pk_res_sobconsulta_ordena_grup PRIMARY KEY (rate_group)
	)