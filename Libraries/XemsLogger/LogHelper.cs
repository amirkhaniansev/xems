using System;

namespace XemsLogger
{
    public class LogHelper
    {
        public static LogInfo CreateLog(DateTime? time, LogType logType, string message, Exception exception)
        {
            return new LogInfo
            {
                Time = time,
                LogType = logType,
                Message = message,
                Exception = exception
            };
        }
    }
}