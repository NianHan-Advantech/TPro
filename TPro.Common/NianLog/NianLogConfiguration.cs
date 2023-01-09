using Microsoft.Extensions.Logging;
using System;

namespace TPro.Common.NianLog
{
    public sealed class NianLogConfiguration
    {
        public NianLogConfiguration()
        {
        }

        public NianLogConfiguration(Func<LogLevel, bool> logcondition)
        {
            LogConditionfunc = logcondition;
        }

        public Func<LogLevel, bool> LogConditionfunc { get; set; } = level => level == LogLevel.Information;

        public INianLogStorage LogStorage { get; set; } = new NianLogStorage();
    }
}