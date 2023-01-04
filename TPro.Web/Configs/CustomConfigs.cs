using Hangfire;
using Hangfire.Storage.SQLite;
using System;

namespace TPro.Web.Configs
{
    public static class CustomConfigs
    {
        #region Hangfire Config

        public static Action<IGlobalConfiguration> HangfireConfig = new Action<IGlobalConfiguration>(config =>
        {
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSQLiteStorage("E:\\MvcTest\\hangfire.db");
        });

        #endregion AutoMapper Config
    }
}