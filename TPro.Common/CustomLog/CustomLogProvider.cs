using Microsoft.Extensions.Logging;
using System;

namespace TPro.Common.CustomLog
{
    public class CustomLogProvider : ILoggerProvider
    {
        private readonly CustomLogOption _option;

        public CustomLogProvider()
        { _option = new CustomLogOption(); }

        public CustomLogProvider(CustomLogOption option)
        { _option = option; }

        public ILogger CreateLogger(string categoryName)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose();
        }
    }

    public class CustomLogger : ILogger
    {
        private readonly CustomLogOption _option;

        public CustomLogger(CustomLogOption option)
        { _option = option; }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= LogLevel.Trace;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            throw new NotImplementedException();
        }
    }

    public class CustomLogEntity
    {
        public long Id { get; set; }
        public string LogLevel { get; set; }

        public string EventId { get; set; }
        public string State { get; set; }
        public string Exception { get; set; }
        public string AppendText { get; set; }
    }

    public class CustomLogOption
    {
        public CustomLogStorage LogStorage { get; set; } = CustomLogStorage.Default;
        public string DbConnectionString { get; set; }
        public string DbName { get; set; } = "CustomLog";
    }

    public enum CustomLogStorage
    {
        Default,
        Sqlite,
    }
}