using System;
using System.Threading;
using XemsLogger;

namespace XemsLoggerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new Logger("Test", "E://log.txt", 1);

            for (var counter = 0L; counter < long.MaxValue; counter++)
            {
                Thread.Sleep(10);

                var logMsg = $"Log_{counter}";

                var log = new LogInfo
                {
                    LogType = LogType.Default,
                    Message = logMsg,
                    Time = DateTime.Now
                };

                logger.Log(logMsg);
            }
        }
    }
}
