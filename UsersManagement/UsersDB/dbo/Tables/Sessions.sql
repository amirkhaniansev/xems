/* 
 * GNU GPL-3.0 29 June 2007
 * SQL script of Addresses table.
 * Copyright (C) 2018  Sevak Amirkhanaian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

CREATE TABLE [dbo].[Sessions]
(
	[Id]            BIGINT           NOT NULL  IDENTITY(1,1),
	[UserId]        INT              NOT NULL,
	[IpAddress]     NVARCHAR(100)    NULL,
	[StartTime]     DATETIME2        NOT NULL,
	[EndTime]       DATETIME2        NOT NULL,

	CONSTRAINT   PK_Session PRIMARY KEY ([Id])
)
