CREATE TABLE [dbo].[Verifications]
(
	[Id]                BIGINT				NOT NULL	IDENTITY (1,1),
	[UserId]            INT					NOT NULL,
	[VerificationCode]  VARCHAR(100)		NOT NULL,
	[CreationDate]      DATETIME2			NOT NULL,
	[ExpirationDate]    DATETIME2			NOT NULL,
	[IsVerified]        BIT					NOT NULL	DEFAULT(0)
	
	CONSTRAINT  PK_Verification		PRIMARY KEY ([Id]) 
)
