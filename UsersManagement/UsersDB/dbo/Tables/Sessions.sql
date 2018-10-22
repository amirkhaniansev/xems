/* 
 * GNU GPL-3.0 29 June 2007
 * SQL script of Sessions table.
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

CREATE TABLE [dbo].[Sessions]
(
	[UserId]        INT              NOT NULL,
	[IpAddress]     NVARCHAR(100)    NOT NULL  DEFAULT(N'0.0.0.0'), 
	[StartTime]     DATETIME2        NOT NULL,
	[EndTime]       DATETIME2        NULL,
	[Platform]		NVARCHAR(100)    NULL,
	[Description]	NVARCHAR(2500)	 NULL,

	CONSTRAINT   PK_Session     PRIMARY KEY ([UserId],[IpAddress],[StartTime])
)
