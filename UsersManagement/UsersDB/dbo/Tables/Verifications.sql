/* 
 * GNU GPL-3.0 29 June 2007
 * SQL script of Verification table.
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

CREATE TABLE [dbo].[Verifications]
(
	[Id]                BIGINT              NOT NULL	IDENTITY (1,1),
	[Username]          VARCHAR(30)                 NOT NULL,
	[VerificationCode]  NVARCHAR(100)        NOT NULL,
	[CreationDate]      DATETIME           NOT NULL,
	[ExpirationDate]    DATETIME           NOT NULL,
	[IsVerified]        BIT                 NOT NULL     DEFAULT(0)
	
	CONSTRAINT  PK_Verification     PRIMARY KEY ([Id]) 
)
