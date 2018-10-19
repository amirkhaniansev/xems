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
	[PasswordHash]      VARCHAR(200)    NOT NULL,
	[IsVerified]        BIT             NOT NULL,
	[RegisterDate]      DATE            NOT NULL,
	[VerificationDate]  DATE            NOT NULL,
	[CurrentProfileId]  INT             NULL,
	[AddressId]         INT             NULL,
	[LastSessionId]     BIGINT          NULL,
	
	CONSTRAINT PK_User                  PRIMARY KEY ([Id]),
	CONSTRAINT FK_User_Address          FOREIGN KEY ([AddressId])		 REFERENCES [dbo].[Addresses]([Id]),
	CONSTRAINT FK_User_Current_Profile  FOREIGN KEY ([CurrentProfileId]) REFERENCES [dbo].[Profiles]([Id]),
	CONSTRAINT FK_Last_Session          FOREIGN KEY ([LastSessionId])	 REFERENCES	[dbo].[Sessions]([Id])
)
