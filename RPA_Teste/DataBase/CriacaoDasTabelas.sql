--CREATE DATABASE ACTIVE_FINANCE

-- USE ACTIVE_FINANCE

--CREATE TABLE [ACTIVE_FINANCE].[DBO].[EXTRACOESFUNDOIMOBILIARIO]
--(
--	ID INT PRIMARY KEY IDENTITY(1,1),
--	LASTINSERTION DATETIME DEFAULT GETDATE(),
--	NOME NVARCHAR(15),
--	VALORATUAL FLOAT,
--	MIN52SEMANAS FLOAT,
--	MAX52SEMANAS FLOAT,
--	DIVIDENDYELD FLOAT,
--	VALORIZACAO12MESES FLOAT,
--	VALPATRIMONIALPORCOTA FLOAT,
--	PVP FLOAT, 
--	VALOREMCAIXA FLOAT,
--	ULTIMORENDIMENTO FLOAT,
--	RENDIMENTO FLOAT,
--	COTACAOBASE FLOAT,
--	[DATABASE] DATETIME,
--	DATAPAGAMENTO DATETIME
--)

--CREATE TABLE [ACTIVE_FINANCE].[DBO].[EXTRACOESACOES]
--(
--	ID INT PRIMARY KEY IDENTITY(1,1),
--	LASTINSERTION DATETIME DEFAULT GETDATE(),
--	NOME NVARCHAR(15),
--	VALORATUAL FLOAT,
--	MIN52SEMANAS FLOAT,
--	MAX52SEMANAS FLOAT,
--	DIVIDENDYELD FLOAT,
--	VALORIZACAO12MESES FLOAT, 
--	PRECOLUCRO FLOAT,
--	PRECOSOBREVALORPATRIMONIAL FLOAT,
--	VALORPATRIMONIALPORACAO FLOAT, 
--	LUCROPORACAO FLOAT,
--	DIVIDALIQUIDAPORPATRIMONIOLIQUIDO FLOAT,
--	MARGEMBRUTA FLOAT,
--	RETORNOSOBREPATRIMONIOLIQUIDO FLOAT,
--)

--CREATE TABLE [ACTIVE_FINANCE].[DBO].[FUNDOSIMOBILIARIOS]
--(
--	ID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
--	NOME NVARCHAR(10) NOT NULL, 
--	STATUS INT NOT NULL,
--	PRECODESEJADO DECIMAL NOT NULL DEFAULT 0.00,

--	CONSTRAINT FUNDO_NOME UNIQUE (NOME)
--)


--CREATE TABLE [ACTIVE_FINANCE].[DBO].[ACOES]
--(
--	ID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
--	NOME NVARCHAR(10) NOT NULL UNIQUE, 
--	STATUS INT NOT NULL,
--	PRECODESEJADO DECIMAL NOT NULL DEFAULT 0.00,
--	CONSTRAINT ACAO_NOME UNIQUE (NOME)
--)

--INSERT INTO [ACTIVE_FINANCE].[DBO].[ACOES] VALUES
--	('VALE3' , 1),
--	('ITUB4', 1),
--	('PETR4', 1),
--	('MGLU3', 1),
--	('ITSA3', 1);

--INSERT INTO [ACTIVE_FINANCE].[DBO].[FUNDOSIMOBILIARIOS] VALUES
--	('XPML11' , 1),
--	('MXRF11', 1),
--	('BVAR11', 1),
--	('SNCI11', 1),
--	('BTRA11', 1);


SELECT * FROM [ACTIVE_FINANCE].[DBO].[EXTRACOESFUNDOIMOBILIARIO]
SELECT * FROM [ACTIVE_FINANCE].[DBO].[EXTRACOESACOES]

SELECT * FROM [ACTIVE_FINANCE].[DBO].[FUNDOSIMOBILIARIOS]
SELECT * FROM [ACTIVE_FINANCE].[DBO].[ACOES]
