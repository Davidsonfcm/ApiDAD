CREATE TABLE res_sobconsulta_lock_processa
	(
	res_num         VARCHAR (12) NOT NULL,
	cd_usuario_lock CHAR (6) NOT NULL,
	dt_lock         DATETIME NOT NULL,
	CONSTRAINT pk_res_sobconsulta_lock_proces PRIMARY KEY (res_num)
	)