using AccessCore.Repository;
using XemsLogger;
using XemsMailer.Mailers;
using XemsPasswordHash;

namespace UsersAPI
{
    public static class Globals
    {        
        public static IXemsLogger Logger { get; set; }

        public static MessageMailer Mailer { get; set; }

        public static DataManager DataManager { get; set; }
        
        public static PasswordHashService Hasher { get; set; }
    }
}
