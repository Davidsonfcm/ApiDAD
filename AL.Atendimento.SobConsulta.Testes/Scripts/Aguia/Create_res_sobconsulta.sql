CREATE TABLE res_sobconsulta
	(
	res_num                      VARCHAR (12) NOT NULL,
	cd_status                    INT NULL,
	qtd_frota_original           INT NULL,
	qtd_frota_processada         INT NULL,
	qtd_frota_alugada_original   INT NULL,
	qtd_frota_alugada_processada INT NULL,
	rate_group_processado        VARCHAR (2) NULL,
	num_processamento            INT NULL,
	id_processamento             INT NULL,
	dt_criacao                   DATETIME NOT NULL,
	dt_ultima_alteracao          DATETIME NOT NULL,
	cd_responsavel_alteracao     CHAR (6) NOT NULL,
	CONSTRAINT pk_res_sobconsulta PRIMARY KEY (res_num),
	CONSTRAINT fk_res_sobc_fk_res_so_res_sobc FOREIGN KEY (cd_status) REFERENCES dbo.res_sobconsulta_motivo (cd_justificativa),
	CONSTRAINT fk_res_sobc_res_processa FOREIGN KEY (id_processamento) REFERENCES dbo.res_sobconsulta_processa (id_processamento)
	)