namespace AuthAPI.Globals
{
    /// <summary>
    /// Class for storing constant values
    /// </summary>
    public static class Constants
    {
        public const string AuthAPI = "AuthAPI";
        public const string GetUserByUsername = "GetUserByUsername";
        public const string GetUserById = "GetUserById";
        public const string Custom = "custom";
        public const string IncorrectPassword = "InCorrect password";
        public const string UserNotExist = "User does not exist";
        public const string InvalidUsernameOrPassword = "Invalid username or password";
        public const string CreateSession = "CreateSession";
        public const string SessionNullError = "Session entity can't be null";
        public const string SessionCreationSuccess = "Session is successfully created";
        public const string SessionError = "Unable to create session";
        public const string SessionEndInfoNullError = "Session end info can't be null";
        public const string SessionUnauthorizedEnd = "Session can't be ended, no enough access";
        public const string SessionEndSuccess = "Session is successfully ended";
        public const string SessionEndError = "Session can't be ended";
        public const string EndSession = "EndSession";
    }
}
