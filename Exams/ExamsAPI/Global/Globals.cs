using XemsLogger;
using XemsMailer.Mailers;
using ExamsAPI.Repositories.Interfaces;

namespace ExamsAPI.Global
{
    public static class Globals
    {        
        public static IXemsLogger Logger { get; set; }

        public static MessageMailer Mailer { get; set; }

        public static IExamRepository ExamRepository { get; set; }
    }
}
