/* 
 * GNU GPL-3.0 29 June 2007
 * SQL script of uspCreateLectu stored procedure.
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

CREATE PROCEDURE [dbo].[uspCreateLecturer]
	@userId				INT,
	@currentUniversity	NVARCHAR(100),
	@occupation			NVARCHAR(100),
	@degree				NVARCHAR(100),
	@thesis				NVARCHAR(100),
	@workStartingDate   NVARCHAR(100)
AS
	BEGIN
		DECLARE @profileId INT 
		EXEC @profileId = uspCreateProfile
					@userId,'lecturer',@currentUniversity,@occupation
		
		IF @profileId = 0
			RETURN 0

		DECLARE @lecturerId INT
		BEGIN TRANSACTION CREATE_LECTURER
			INSERT INTO [dbo].[Lecturers] 
				VALUES (@degree,@thesis,@workStartingDate,@profileId)
			SET @lecturerId = SCOPE_IDENTITY()
		COMMIT TRANSACTION CREATE_LECTURER
		
		RETURN @lecturerId
	END