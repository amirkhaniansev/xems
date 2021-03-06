﻿CREATE PROCEDURE [dbo].[uspGetLecturerByUsername]
	@username VARCHAR(30)
AS
	BEGIN
		SELECT	l.Id,
				l.Degree,
				l.Thesis,
			    l.WorkStartingDate,	
				p.UserId,
				p.CurrentUniversity,
				p.Occupation
			FROM [dbo].[Lecturers] l
			INNER JOIN [dbo].[Profiles] p ON l.ProfileId = p.Id
			INNER JOIN [dbo].[Users] u ON p.UserId = u.Id
			WHERE u.Username = @username
	END
RETURN 0