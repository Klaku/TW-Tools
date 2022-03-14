using System;
using System.Diagnostics;
using System.Threading;
using NLog;

namespace CoreApi.Helpers
{
    public sealed class MonitoredScope : IDisposable
    {
        private string _name;
        private Stopwatch _stopWatch;
        internal int ThreadId { get; private set; }
        private Logger _logger;
        public string Name
        {
            get => _name;
            set => _name = string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        public MonitoredScope(string name, Logger logger)
        {
            StartScope(name,logger);
        }

        public void StartScope(string name, Logger logger)
        {
            ThreadId = Thread.CurrentThread.ManagedThreadId;
            _stopWatch = new Stopwatch();
            _name = name;
            _stopWatch.Start();
            _logger = logger;
            Debug("Started");
        }

        public void Info(string message)
        {
            _logger
                .WithProperty("name",_name.ToString())
                .WithProperty("time",_stopWatch.ElapsedMilliseconds.ToString())
                .WithProperty("thread",ThreadId.ToString())
                .Info(message);
        }
        public void Debug(string message)
        {
            _logger
                .WithProperty("name", _name.ToString())
                .WithProperty("time", _stopWatch.ElapsedMilliseconds.ToString())
                .WithProperty("thread", ThreadId.ToString())
                .Debug(message);
        }
        public void Error(string message)
        {
            _logger
                .WithProperty("name", _name.ToString())
                .WithProperty("time", _stopWatch.ElapsedMilliseconds.ToString())
                .WithProperty("thread", ThreadId.ToString())
                .Error(message);
        }

        void EndScope()
        {
            _stopWatch.Stop();
            Debug("Finished");
            Trace.Flush();
        }
        public void Dispose()
        {
            Dispose(true);
        }

        public void Dispose(bool flag)
        {
            EndScope();
        }
    }
}
