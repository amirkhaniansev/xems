CREATE TABLE [dbo].[Sessions]
(
	[Id]            BIGINT			 NOT NULL  IDENTITY(1,1),
	[UserId]        INT				 NOT NULL,
	[IpAddress]     NVARCHAR(100)	 NULL,
	[StartTime]     DATETIME2		 NOT NULL,
	[EndTime]       DATETIME2		 NOT NULL,

	CONSTRAINT   PK_Session PRIMARY KEY ([Id])
)
