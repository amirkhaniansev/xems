/* 
 * GNU GPL-3.0 29 June 2007
 * SQL script of Addresses table.
 * Copyright (C) 2018  Sevak Amirkhanaian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

CREATE TABLE [dbo].[Profiles]
(
	[Id]                INT	            NOT NULL IDENTITY(1,1),
	[UserId]            INT             NOT NULL,
	[Type]              VARCHAR(10)     NOT NULL,
	[CurrentUniversity] NVARCHAR(100)   NULL,
	[Occupation]        NVARCHAR(100)   NOT NULL,
	[CreationDate]      DATE            NOT NULL,
	
	CONSTRAINT          PK_Profile      PRIMARY KEY([Id])
)
