/* 
 * GNU GPL-3.0 29 June 2007
 * SQL script of Students table.
 * Copyright (C) 2018  Sevak Amirkhanaian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

CREATE TABLE [dbo].[Students]
(
	[Id]            INT             NOT NULL,
	[Department]    NVARCHAR(100)   NULL,
	[EntranceDate]  DATE            NULL,
	[ProfileId]     INT             NOT NULL,

	CONSTRAINT      PK_Student      PRIMARY KEY ([Id]),
	CONSTRAINT      FK_Student_Profile		FOREIGN KEY ([ProfileId]) REFERENCES [dbo].[Profiles]([Id])
)
