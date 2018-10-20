/* 
 * GNU GPL-3.0 29 June 2007
 * SQL script of Addresses table.
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

CREATE TABLE [dbo].[Addresses]
(
	[Id]                INT             NOT NULL IDENTITY (1,1),
	[Continent]         NVARCHAR(100)   NULL,
	[Country]           NVARCHAR(100)   NULL,
	[State]             NVARCHAR(100)   NULL,
	[CityOrVillage]     NVARCHAR(100)   NULL,
	[Street]            NVARCHAR(100)   NULL,
	[Building]          NVARCHAR(100)   NULL,
	[ZipCode]           VARCHAR(50)     NULL,

	CONSTRAINT PK_Address PRIMARY KEY ([Id]),	
)
