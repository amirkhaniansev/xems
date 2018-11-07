using System;

namespace XemsLogger
{
    public class LogInfo
    {
        public DateTime? Time { get; set; }

        public LogType? LogType { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }
    }
}