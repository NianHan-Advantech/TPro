using Microsoft.Extensions.Logging;
using System;

namespace TPro.Common.NianLog
{
    public sealed class NianLogProvider : ILoggerProvider
    {
        private readonly NianLogConfiguration logConfiguration = new();

        public NianLogProvider(Action<NianLogConfiguration> action)
        {
            action.Invoke(logConfiguration);
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new NianLogger(logConfiguration, categoryName);
        }

        public void Dispose()
        {
            return;
        }
    }
}