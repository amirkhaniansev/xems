namespace UsersAPI
{
    public enum Error : int
    {
        VerificationCodeCreationSuccess = 1,
        VerificationCodeCreationFail = 0,
        UserNotFound = 2,
        VerificationFail = 3,
        ExpiredKey = 4,
        VerificationSuccess = 5,
        LecturerAlreadyExists = 0,
        StudentAlreadyExists = 0
    }
}