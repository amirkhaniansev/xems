/* 
 * GNU GPL-3.0 29 June 2007
 * SQL script of uspCreateVerification stored procedure.
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

CREATE PROCEDURE [dbo].[uspCreateVerification]
	@userId           INT,
	@verificationCode VARCHAR(100),
	@creationDate     DATETIME2,
	@expirationDate   DATETIME
AS
	BEGIN
		INSERT INTO [dbo].[Verifications]
			VALUES (@userId,
					@verificationCode,
				    @creationDate,
					@expirationDate,
					0)
		RETURN 0
	END