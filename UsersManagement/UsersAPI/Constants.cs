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
        public const string ForbiddenLectureCreation = "Lecturer profile creation is forbidden due to wrong UserId.";
        public const string CreateLecturerProfile = "CreateLecturerProfile";
        public const string LecturerAlreadyExists = "User already has lectuere profile.";
        public const string InvalidUserId = "User ID cannot be 0.";
        public const string LecturerCreationUnknownError = "Error occured during creation of lecturer profile.";
        public const string GetLecturerByUsername = "GetLecturerByUsername";
        public const string LecturerNotFound = "Lecturer is not found";
        public const string LecturerSearchError = "Error occured during lecturer search";
        public const string GetLecturers = "GetLecturers";
    }
}