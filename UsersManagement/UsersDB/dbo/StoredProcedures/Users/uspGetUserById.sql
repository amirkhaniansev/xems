CREATE PROCEDURE [dbo].[uspGetUserById]
	@id int
	AS
		BEGIN
			SELECT * from [dbo].[Users] WHERE [Id] = @id
		END
	RETURN 0