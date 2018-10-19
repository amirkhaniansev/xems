CREATE TABLE [dbo].[Addresses]
(
	[Id]                INT			    NOT NULL IDENTITY (1,1),
	[Continent]         NVARCHAR(100)   NULL,
	[Country]           NVARCHAR(100)   NULL,
	[State]             NVARCHAR(100)   NULL,
	[CityOrVillage]     NVARCHAR(100)   NULL,
	[Street]            NVARCHAR(100)   NULL,
	[Building]          NVARCHAR(100)   NULL,
	[ZipCode]           VARCHAR(50)		NULL,

	CONSTRAINT PK_Address PRIMARY KEY ([Id]),	
)
