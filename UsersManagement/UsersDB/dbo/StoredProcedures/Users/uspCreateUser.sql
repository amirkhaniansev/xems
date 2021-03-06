﻿/* 
 * GNU GPL-3.0 29 June 2007
 * SQL script of uspCreateUser stored procedure.
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

CREATE PROCEDURE [dbo].[uspCreateUser]
	@firstName     NVARCHAR(100),
	@middleName	   NVARCHAR(100),
	@lastName	   NVARCHAR(100),
	@birthdate	   DATE,
	@email		   NVARCHAR(100),
	@phone		   NVARCHAR(100),
	@gender		   CHAR(1),
	@profession    NVARCHAR(100) = N' ',
	@description   NVARCHAR(1000) = N' ',
	@username      varchar(30),
	@passwordHash  NVARCHAR(200),
	@continent     NVARCHAR(100) = N' ',
	@country       NVARCHAR(100),
	@state         NVARCHAR(100),
	@cityOrVillage NVARCHAR(100),
	@street        NVARCHAR(100),
	@building      NVARCHAR(100),
	@zipCode       VARCHAR(50)
AS
	BEGIN
		IF EXISTS (SELECT [Username] FROM [dbo].[Users] 
					WHERE [Username] = @username) OR
			EXISTS (SELECT [EMAIL] FROM [dbo].[Users]
					 WHERE [Email] = @email)		
			RETURN 0

		DECLARE @addressId INT
		EXEC @addressId = uspCreateAddress
					@continent,
					@country,
					@state,
					@cityOrVillage,
					@street,
					@building,
					@zipCode

		DECLARE @userId INT
		BEGIN TRANSACTION CREATE_USER
			INSERT INTO [dbo].[Users]
				values (@firstName,
						@middleName,
						@lastName,
						@birthdate,
						@email,
						@phone,
						@gender,
						@profession,
						@description,
						@username,
						@passwordHash,
						0,
						0,
						GETDATE(),
						NULL,
						NULL,
						N' ',
						@addressId
						)
			SET @userId = SCOPE_IDENTITY()
		COMMIT TRANSACTION CREATE_USER

		RETURN @userId
	END
