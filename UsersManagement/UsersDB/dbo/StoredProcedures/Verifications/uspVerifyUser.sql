CREATE PROCEDURE [dbo].[uspVerifyUser]
	@userId INT,
    @verificationKey NVARCHAR(100)
	AS
		BEGIN
			IF NOT EXISTS (SELECT [Id] FROM [dbo].[Users]
				WHERE [Id] = @userId AND [IsVerified] = 0)
			 RETURN 2 

			IF NOT EXISTS (SELECT [VerificationCode] FROM [dbo].[Verifications]
				WHERE [UserId] = @userId AND [VerificationCode] = @verificationKey)
			 RETURN 3
	
			IF NOT EXISTS (SELECT [VerificationCode] FROM [dbo].[Verifications]
				WHERE [UserId] = @userId AND [VerificationCode] = @verificationKey AND ExpirationDate > GETDATE())
			 RETURN 4
			
			BEGIN TRANSACTION VERIFY_TRANSACTION
				UPDATE [dbo].[Users]
					SET [VerificationDate] = GETDATE(),
						[IsVerified] = 1
				UPDATE [dbo].[Verifications]
					SET [IsVerified] = 1	
			COMMIT TRANSACTION VERIFY_TRANSACTION

			RETURN 5
		END	
