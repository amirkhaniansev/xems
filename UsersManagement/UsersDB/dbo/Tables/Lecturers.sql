/* 
 * GNU GPL-3.0 29 June 2007
 * SQL script of Addresses table.
 * Copyright (C) 2018  Sevak Amirkhanaian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

CREATE TABLE [dbo].[Lecturers]
(
	[Id]                INT             NOT NULL IDENTITY (1,1),
	[Degree]            NVARCHAR(100)   NULL,
	[Thesis]            NVARCHAR(100)   NULL,
	[WorkStartingDate]  DATE            NOT NULL,
	[ProfileId]         INT             NOT NULL

	CONSTRAINT      PK_Lecturer                 PRIMARY KEY ([Id]),
	CONSTRAINT      FK_Lecturer_Profile         FOREIGN KEY ([ProfileId]) REFERENCES [dbo].[Profiles]([Id])
)
