namespace UsersAPI
{
    public class Constants
    {
        public const string CreateUser = "CreateUser";
        public const string UserCreationSuccess = "User is successfully created";
        public const string WelcomeSubject = "Welcome to Xems.";
        public const string WelcomeMessage = "You successfully signed up. Verify your account to use our system.";
        public const string AddVerificationKey = "AddVerificationKey";
        public const string VerificationKeyCreationFail = "User does not exist or is already verified.";
        public const string VerificationSubject = "Verify your Xems account";
        public const string VerificationCreationSuccess = "Verification Key is successfully created.";
        public const string UserSignUpFail = "Unable to sign up the user";
        public const string UserCreationError = "Error occured while creating the user";
        public const string GetUserById = "GetUserById";
        public const string VerifyUser = "VerifyUser";
        public const string UserNotFoundMessage = "User is not found.";
        public const string InvalidKeyMessage = "Invalid key is provided.";
        public const string KeyIsExpired = "Verification key is expired.";
        public const string UserVerificationSuccess = "User is successfully verified.";
        public const string VerificationError = "Error occured while verifiying the user.";
    }
}