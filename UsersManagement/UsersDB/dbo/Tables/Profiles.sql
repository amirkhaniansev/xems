CREATE TABLE [dbo].[Profiles]
(
	[Id]				INT				NOT NULL IDENTITY(1,1),
	[UserId]			INT				NOT NULL,
	[Type]				VARCHAR(10)		NOT NULL,
	[CurrentUniversity]	NVARCHAR(100)	NULL,
	[Occupation]		NVARCHAR(100)	NOT NULL,
	[CreationDate]		DATE			NOT NULL,
	
	CONSTRAINT		PK_Profile		PRIMARY KEY([Id])
)
