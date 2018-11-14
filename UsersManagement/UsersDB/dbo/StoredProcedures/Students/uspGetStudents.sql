CREATE PROCEDURE [dbo].[uspGetStudents]
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
	END
RETURN 0
