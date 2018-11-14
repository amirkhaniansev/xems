CREATE PROCEDURE [dbo].[uspGetStudentByUsername]
	@username VARCHAR(30)
AS
	BEGIN
		SELECT  s.Id,
				s.Department,
				s.EntranceDate,
				p.CurrentUniversity,
				p.Occupation,
				p.UserId
		FROM [dbo].Students s
		INNER JOIN [dbo].[Profiles] p ON s.ProfileId = p.Id
		INNER JOIN [dbo].[Users] u ON p.UserId = u.Id
		WHERE u.Username = @username
	END
RETURN 0