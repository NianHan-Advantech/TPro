using Microsoft.Extensions.Logging;
using System;

namespace TPro.Common.NianLog
{
    public sealed class NianLogger : ILogger
    {
        private readonly NianLogConfiguration _configuration = new();
        private readonly string _categoryName;

        public NianLogger(NianLogConfiguration configuration, string categoryName)
        {
            _configuration = configuration;
            _categoryName = categoryName;
        }

        public IDisposable BeginScope<TState>(TState state) => default!;

        public bool IsEnabled(LogLevel logLevel)
        {
            return _configuration.LogConditionfunc(logLevel);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;
            var entity = new NianLogEntity()
            {
                LogLevel = logLevel.ToString(),
                EventId = eventId.ToString(),
                State = state.ToString(),
                Exception = exception?.ToString() ?? "",
                CategoryName = _categoryName
            };
            _configuration.LogStorage.WriteToStorage(entity);
        }
    }
}