/* 
 * GNU GPL-3.0 29 June 2007
 * SQL script of Addresses table.
 * Copyright (C) 2018  Sevak Amirkhanaian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

CREATE TABLE [dbo].[Verifications]
(
	[Id]                BIGINT              NOT NULL	IDENTITY (1,1),
	[UserId]            INT                 NOT NULL,
	[VerificationCode]  VARCHAR(100)        NOT NULL,
	[CreationDate]      DATETIME2           NOT NULL,
	[ExpirationDate]    DATETIME2           NOT NULL,
	[IsVerified]        BIT                 NOT NULL     DEFAULT(0)
	
	CONSTRAINT  PK_Verification     PRIMARY KEY ([Id]) 
)
