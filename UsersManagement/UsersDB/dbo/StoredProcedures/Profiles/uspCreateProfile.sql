/* 
 * GNU GPL-3.0 29 June 2007
 * SQL script of uspCreateProfile stored procedure.
 * Copyright (C) 2018  Sevak Amirkhanaian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

CREATE PROCEDURE [dbo].[uspCreateProfile]
	@userId				INT,
	@type				VARCHAR(10),
	@currentUniversity	NVARCHAR(100),
	@occupation			NVARCHAR(100)
AS
	BEGIN
		IF EXISTS (SELECT [Type] FROM [dbo].[Profiles] 
					WHERE [UserId] = @userId and [Type] = @type)
			RETURN 0
		
		DECLARE @profileID INT
		BEGIN TRANSACTION CREATE_PROFILE
			INSERT INTO [dbo].[Profiles]
				VALUES (@userId,
						@type,
						@currentUniversity,
						@occupation,
						GETDATE())
			SET @profileId = SCOPE_IDENTITY()
		COMMIT TRANSACTION CREATE_PROFILE
		
		RETURN @profileId
	END