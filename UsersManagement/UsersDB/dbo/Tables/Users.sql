/* 
 * GNU GPL-3.0 29 June 2007
 * SQL script of Users table.
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

CREATE TABLE [dbo].[Users]
(
	[Id]                INT             NOT NULL	IDENTITY (1,1),
	[FirstName]         NVARCHAR(100)   NOT NULL,
	[MiddleName]        NVARCHAR(100)   NULL,
	[LastName]          NVARCHAR(100)   NULL,
	[BirthDate]         DATE            NOT NULL,
	[Email]             NVARCHAR(100)   NOT NULL,
	[Phone]             NVARCHAR(100)   NULL,
	[Gender]            CHAR(1)         NULL,
	[Profession]        NVARCHAR(100)   NULL,
	[Description]       NVARCHAR(1000)  NULL,
	[Username]          VARCHAR(30)     NOT NULL,
	[PasswordHash]      NVARCHAR(200)    NOT NULL,
	[IsVerified]        BIT             NOT NULL,
	[IsActive]          BIT				NULL,					
	[RegisterDate]      DATE            NOT NULL,
	[VerificationDate]  DATE            NULL,
	[CurrentProfileId]  INT             NULL,
	[AddressId]         INT             NULL

	CONSTRAINT PK_User                  PRIMARY KEY ([Id]),
	CONSTRAINT FK_User_Address          FOREIGN KEY ([AddressId])		 REFERENCES [dbo].[Addresses]([Id]),
	CONSTRAINT FK_User_Current_Profile  FOREIGN KEY ([CurrentProfileId]) REFERENCES [dbo].[Profiles]([Id])
)
