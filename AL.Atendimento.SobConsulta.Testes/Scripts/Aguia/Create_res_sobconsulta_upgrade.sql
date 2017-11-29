CREATE TABLE res_sobconsulta_upgrade
	(
	rate_group_ordena        VARCHAR (2) NOT NULL,
	rate_group_upgrade       VARCHAR (2) NOT NULL,
	dt_ultima_alteracao      DATETIME NOT NULL,
	cd_responsavel_alteracao CHAR (6) NOT NULL,
	CONSTRAINT pk_res_sobconsulta_upgrade PRIMARY KEY (rate_group_ordena,rate_group_upgrade)
	)