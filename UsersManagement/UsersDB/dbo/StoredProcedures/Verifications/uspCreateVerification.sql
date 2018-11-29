/* 
 * GNU GPL-3.0 29 June 2007
 * SQL script of uspCreateVerification stored procedure.
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

CREATE PROCEDURE [dbo].[uspCreateVerification]
	@username         VARCHAR(30),
	@verificationCode NVARCHAR(100),
	@creationDate     DATETIME,
	@expirationDate   DATETIME
AS
	BEGIN
		IF NOT EXISTS (SELECT [Id] FROM [dbo].[Users] 
			WHERE [Username] = @username AND [IsVerified] = 0)
		   RETURN 0

		INSERT INTO [dbo].[Verifications]
			VALUES (@username,
					@verificationCode,
				    @creationDate,
					@expirationDate,
					0)

		RETURN 1
	END