/* 
 * GNU GPL-3.0 29 June 2007
 * SQL script of uspCreateAddress stored procedure.
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

CREATE PROCEDURE [dbo].[uspCreateAddress]
	@continent     NVARCHAR(100),
	@country       NVARCHAR(100),
	@state         NVARCHAR(100),
	@cityOrVillage NVARCHAR(100),
	@street        NVARCHAR(100),
	@building      NVARCHAR(100),
	@zipCode       VARCHAR(50)
	AS
		BEGIN
			DECLARE @id INT 
				BEGIN
					BEGIN TRANSACTION INSERT_ADDRESS
						INSERT INTO [dbo].[Addresses]
							VALUES (@continent,
									@country,
									@state,
									@cityOrVillage,
									@street,
									@building,
									@zipCode)			
						SET @id = SCOPE_IDENTITY()
					COMMIT TRANSACTION INSERT_ADDRESS
				END				  
			RETURN @id
		END