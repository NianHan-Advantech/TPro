using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TPro.Common.CustomLog;
using TPro.Web.Configs;

namespace TPro.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            //CreateHostBuilder(args).ConfigureLogging(builder =>
            //    //config.ClearProviders().AddProvider(CustomConfigs.logProvider)
            //    builder.ClearProviders().AddColorConsoleLogger(config =>
            //    {
            //        config.LogLevelToColorMap[LogLevel.Warning]=System.ConsoleColor.Yellow;
            //        config.LogLevelToColorMap[LogLevel.Error]=System.ConsoleColor.DarkRed;
            //    })
            //).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}