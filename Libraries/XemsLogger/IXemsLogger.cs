namespace XemsLogger
{
    public interface IXemsLogger
    {
        void Log(LogInfo logInfo);

        void Log(string logInfo);

        void Clear();
    }
}