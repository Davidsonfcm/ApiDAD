CREATE TABLE res_sobconsulta_motivo
	(
	cd_justificativa         INT NOT NULL,
	desc_justificativa       CHAR (50) NOT NULL,
	dt_ultima_alteracao      DATETIME NOT NULL,
	cd_responsavel_alteracao CHAR (6) NOT NULL,
	CONSTRAINT pk_res_sobconsulta_motivo PRIMARY KEY (cd_justificativa)
	)
