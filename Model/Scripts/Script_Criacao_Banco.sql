USE [AppDAD]
GO
/****** Object:  Table [dbo].[consulta]    Script Date: 30/11/2017 23:19:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[consulta](
	[identificador] [int] IDENTITY(1,1) NOT NULL,
	[data] [datetime] NOT NULL,
	[animal] [varchar](100) NOT NULL,
	[usuarioCpf] [nvarchar](50) NOT NULL,
	[diagnostico] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[identificador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuario]    Script Date: 30/11/2017 23:19:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario](
	[cpf] [nvarchar](50) NOT NULL,
	[senha] [nvarchar](50) NULL,
	[tipo] [nvarchar](50) NULL,
	[tentativas] [int] NULL,
	[bloqueado] [bit] NULL,
	[nome] [varchar](100) NULL,
	[email] [varchar](100) NULL,
 CONSTRAINT [PK_usuario] PRIMARY KEY CLUSTERED 
(
	[cpf] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[consulta]  WITH CHECK ADD FOREIGN KEY([usuarioCpf])
REFERENCES [dbo].[usuario] ([cpf])
GO
