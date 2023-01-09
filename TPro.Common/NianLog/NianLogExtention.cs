using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System;

namespace TPro.Common.NianLog
{
    public static class NianLogExtention
    {
        public static ILoggingBuilder AddNianLogger(this ILoggingBuilder builder)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, NianLogProvider>());
            LoggerProviderOptions.RegisterProviderOptions<NianLogConfiguration, NianLogProvider>(builder.Services);
            return builder;

        }
        public static ILoggingBuilder AddNianLogger(this ILoggingBuilder builder, Action<NianLogConfiguration> action)
        {
            builder.AddNianLogger();
            builder.Services.Configure(action);
            return builder;
        }
    }
}