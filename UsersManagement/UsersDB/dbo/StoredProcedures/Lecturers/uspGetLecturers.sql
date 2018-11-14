CREATE PROCEDURE [dbo].[uspGetLecturers]
	AS
		BEGIN
			SELECT  l.Id,
				    l.Degree,
				    l.Thesis,
					l.WorkStartingDate,	
					p.UserId,
					p.CurrentUniversity,
					p.Occupation
			FROM [dbo].[Lecturers] l
			INNER JOIN [dbo].[Profiles] p ON l.ProfileId = p.Id
		END
	RETURN 0