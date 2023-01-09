using Hangfire;
using Hangfire.Storage.SQLite;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System;
using TPro.Common.NianLog;

namespace TPro.Web.Configs
{
    public static class CustomConfigs
    {
        #region Log Config

        public static NianLogProvider logProvider = new NianLogProvider(option =>
        {
            option.UseSqliteStorage("Data Source=E:\\MvcTest\\templatedb.db;");
        });

        #endregion Log Config

        #region Hangfire Config

        public static Action<IGlobalConfiguration> HangfireConfig = new Action<IGlobalConfiguration>(config =>
        {
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSQLiteStorage("E:\\MvcTest\\hangfire.db");
        });

        #endregion Hangfire Config

        #region Cors(跨域) Config

        public const string CorsSampleCorsPolicy = "CorsSample";

        public static Action<CorsPolicyBuilder> CorsConfig = new Action<CorsPolicyBuilder>(config =>
        {
            config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        });

        #endregion Cors(跨域) Config
    }
}