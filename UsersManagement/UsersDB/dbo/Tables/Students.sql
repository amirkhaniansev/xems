CREATE TABLE [dbo].[Students]
(
	[Id]            INT				NOT NULL,
	[Department]    NVARCHAR(100)	NULL,
	[EntranceDate]  DATE			NULL,
	[ProfileId]     INT				NOT NULL,

	CONSTRAINT      PK_Student		PRIMARY KEY ([Id]),
	CONSTRAINT      FK_Student_Profile		FOREIGN KEY ([ProfileId]) REFERENCES [dbo].[Profiles]([Id])
)
