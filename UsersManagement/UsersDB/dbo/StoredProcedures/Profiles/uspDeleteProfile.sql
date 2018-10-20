/* 
 * GNU GPL-3.0 29 June 2007
 * SQL script of uspDeleteProfile stored procedure.
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

CREATE PROCEDURE [dbo].[uspDeleteProfile]
	@profileId INT
AS
	BEGIN
		BEGIN TRANSACTION DELETE_PROFILE
			BEGIN TRY
				DELETE FROM [dbo].[Profiles] 
					WHERE [Id] = @profileId
			END TRY
			BEGIN CATCH
				RETURN 0
			END CATCH
		COMMIT TRANSACTION DELETE_PROFILE
		RETURN 1
	END		
