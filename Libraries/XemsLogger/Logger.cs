using System;
using System.IO;
using System.Text;
using System.Timers;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace XemsLogger
{
    public class Logger : IXemsLogger
    {
        private readonly string _appName;
        
        private readonly ConcurrentDictionary<DateTime?, LogInfo> _logs;

        private readonly List<string> _paths;

        private string _initialPath;

        private Timer _timer;

        private BackgroundWorker _writer;

        private string _path;

        private int _storingInterval;

        private bool _isStoring;

        private long _fileMaxSize;

        private int _fileCount;

        public string AppName => this._appName;

        public int FileCount => this._fileCount;

        public long FileMaxSize
        {
            get => this._fileMaxSize;

            set => this._fileMaxSize = value;
        }

        public string Path
        {
            get => this._path;

            set => this._path = value;
        }

        public int StoringInterval
        {
            get => this._storingInterval;

            set => this.InitializeTimer(value);
        }

        public bool IsStoring
        {
            get
            {
                return this._isStoring;
            }
            set
            {
                if (this._isStoring == value)
                    return;

                this._isStoring = value;

                if (this._isStoring == true)
                    this._timer.Start();
                else this._timer.Stop();
            }
        }

        public Logger(string appName, string path, int storingInterval = 60)
        {
            this._appName = appName;
            this._path = path;
            this._initialPath = path;
            this._storingInterval = storingInterval;
            this._isStoring = true;
            this._fileMaxSize = 0x400000;

            this._logs = new ConcurrentDictionary<DateTime?, LogInfo>();
            this._paths = new List<string>
            {
                this._path
            };



            this.InitializeTimer(storingInterval);
            this.InitializeWriter();            
        }

        public void Log(LogInfo logInfo)
        {
            var log = default(LogInfo);

            if (logInfo == null)
            {
                log = new LogInfo
                {
                    Time = DateTime.Now,
                    LogType = LogType.Default,
                    Message = "Unknown log"
                };
            }
            else log = logInfo;

            this._logs.TryAdd(log.Time, log);
        }

        public void Log(string logInfo)
        {
            this.Log(this.ConstructLog(logInfo));
        }

        public void ClearCache()
        {
            this._logs.Clear();
        }

        public void Clear()
        {
            this.ClearCache();

            foreach (var path in this._paths)
            {
                File.Delete(path);
            }

            this._paths.Clear();

            this._fileCount = 0;
            this._path = this._initialPath;
        }

        public void Dispose()
        {
            this._timer.Dispose();
            this._writer.Dispose();
        }

        private void InitializeTimer(int interval)
        {
            if (interval < 1)
                throw new ArgumentException("Interval");

            this._storingInterval = interval;

            if (this._timer != null)
                this._timer.Elapsed -= this.ExecWhenTimerElapses;

            this._timer = new Timer(this._storingInterval * 60000);
            this._timer.Elapsed += this.ExecWhenTimerElapses;

            this._timer.Start();
        }

        private void InitializeWriter()
        {
            this._writer = new BackgroundWorker();
            this._writer.DoWork += Write;
        }

        private void Write(object sender, DoWorkEventArgs e)
        {
            this.CheckFileSize();

            using (var stream = File.AppendText(this._path))
            {
                foreach (var log in this._logs)
                {
                    stream.WriteLine(this.GetLogAsLine(log.Value));
                }
            }
        }

        private void ExecWhenTimerElapses(object sender, ElapsedEventArgs e)
        {
           if (!this._writer.IsBusy)
                this._writer.RunWorkerAsync();
        }

        private string GetLogAsLine(LogInfo logInfo)
        {          
            var time = logInfo.Time == null ? DateTime.Now : logInfo.Time;
            var logType = logInfo.LogType == null ? LogType.Default : logInfo.LogType;
            var message = logInfo.Message == null ? "" : logInfo.Message;
            var exception = logInfo.Exception == null ? "" : logInfo.Exception.ToString();

            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"{time}    ");
            stringBuilder.Append($"{logType}    ");
            stringBuilder.Append($"{message}    ");
            stringBuilder.Append($"{exception}");

            return stringBuilder.ToString();
        }

        private LogInfo ConstructLog(string log)
        {
            return new LogInfo
            {
                Time = DateTime.Now,
                LogType = LogType.Default,
                Message = log
            };
        }

        private void CheckFileSize()
        {
            try
            {
                var fileInfo = new FileInfo(this._path);
                var fileSize = fileInfo.Length;

                if (fileSize > this._fileMaxSize)
                {
                    this._fileCount++;

                    var fileName = System.IO.Path.GetFileNameWithoutExtension(this._path);
                    var newPath = $"{fileName}_{this._fileCount}.{fileInfo.Extension}";

                    this._paths.Add(newPath);
                    this._path = newPath;
                }
            }
            catch
            {
                Debug.WriteLine("FileSizeCheckError");
            }
        }
    }
}