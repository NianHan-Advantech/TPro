using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace TPro.Common.NianLog
{
    public sealed class NianLogProvider : ILoggerProvider
    {
        private readonly NianLogConfiguration logConfiguration = new();

        public NianLogProvider(IOptionsMonitor<NianLogConfiguration> options) => logConfiguration = options.CurrentValue;

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