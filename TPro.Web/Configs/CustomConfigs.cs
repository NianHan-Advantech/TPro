using Hangfire;
using Hangfire.Storage.SQLite;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using TPro.Common.NianLog;

namespace TPro.Web.Configs
{
    public static class CustomConfigs
    {
        public static readonly string currentpath = Directory.GetCurrentDirectory();

        #region DbContext Config
        public static readonly Action<DbContextOptionsBuilder> dbcontextoption = new(option =>
        {
            option.UseSqlite($"{currentpath}\\templatedb.db;");
        });
        #endregion

        #region Log Config

        public static NianLogProvider logProvider = new(option =>
        {
            var dic = Directory.GetCurrentDirectory();
            option.UseSqliteStorage($"Data Source={currentpath}\\templatedb.db;");
        });

        #endregion Log Config

        #region Hangfire Config

        public static readonly Action<IGlobalConfiguration> HangfireConfig = new (config =>
        {
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSQLiteStorage($"{currentpath}\\hangfire.db");
        });

        #endregion Hangfire Config

        #region Cors(跨域) Config

        public const string CorsSampleCorsPolicy = "CorsSample";

        public static readonly Action<CorsPolicyBuilder> CorsConfig = new (config =>
        {
            config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        });

        #endregion Cors(跨域) Config
    }
}