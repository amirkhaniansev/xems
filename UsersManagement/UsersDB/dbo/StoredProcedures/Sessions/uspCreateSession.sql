/* 
 * GNU GPL-3.0 29 June 2007
 * SQL script of uspCreateSession stored procedure.
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

CREATE PROCEDURE [dbo].[uspCreateSession]
	@userId      INT,
	@ipAddresss  NVARCHAR(100),
	@startTime	 DATETIME2,
	@endTime	 DATETIME2,
	@platform	 NVARCHAR(100),
	@description NVARCHAR(2500)
AS
	BEGIN
		BEGIN TRANSACTION CREATE_SESSION
			BEGIN TRY
				INSERT INTO [dbo].[Sessions]
					VALUES (@userId,
							@ipAddresss,
							@startTime,
							@endTime,
							@platform,
							@description)
				COMMIT TRANSACTION CREATE_SESSION
				RETURN 1
			END TRY
			BEGIN CATCH
				ROLLBACK TRANSACTION CREATE_SESSION
				RETURN 0
			END CATCH
	END
