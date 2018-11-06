CREATE PROCEDURE [dbo].[uspGetUserByUsername]
	@username VARCHAR(30)
	AS
		BEGIN
			SELECT * from [dbo].[Users] WHERE [Username] = @username
		END
	RETURN 0
