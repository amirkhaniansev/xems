/* 
 * GNU GPL-3.0 29 June 2007
 * SQL script of uspCreateStudent stored procedure.
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

CREATE PROCEDURE [dbo].[uspCreateStudent]
	@userId				INT,
	@currentUniversity	NVARCHAR(100),
	@occupation			NVARCHAR(100),
	@department         NVARCHAR(100),
	@entranceDate       DATE
AS
	BEGIN
		DECLARE @profileId INT 
		EXEC @profileId =  uspCreateProfile
					@userId,'student',@currentUniversity,@occupation
		
		IF @profileId = 0
			RETURN 0

		DECLARE @studentId INT
		BEGIN TRANSACTION CREATE_STUDENT
			INSERT INTO [dbo].[Students] 
				VALUES (@department,@entranceDate,@profileId)
			SET @studentId = SCOPE_IDENTITY()
		COMMIT TRANSACTION CREATE_STUDENT
		
		RETURN @studentID
	END	
