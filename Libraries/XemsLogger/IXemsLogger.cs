using System;

namespace XemsLogger
{
    public interface IXemsLogger : IDisposable
    {
        void Log(LogInfo logInfo);

        void Log(string logInfo);

        void Clear();
    }
}