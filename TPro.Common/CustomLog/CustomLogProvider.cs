using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System;

namespace TPro.Common.CustomLog
{
    public class CustomLogProvider : ILoggerProvider
    {
        private readonly CustomLogOption _option;

        public CustomLogProvider() => _option = new CustomLogOption();

        public CustomLogProvider(CustomLogOption option) => _option = option;

        public CustomLogProvider(Action<CustomLogOption> action)
        {
            _option = new CustomLogOption();
            action.Invoke(_option);
        }

        public ILogger CreateLogger(string categoryName) => new CustomLogger(_option, categoryName);

        public void Dispose()
        {
            return;
        }
    }

    public class CustomLogger : ILogger
    {
        private readonly CustomLogOption _option;

        public CustomLogger(CustomLogOption option, string categoryName)
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
            if (!IsEnabled(logLevel))
                return;
            var logEntity = new CustomLogEntity()
            {
                LogLevel = logLevel.ToString(),
                EventId = eventId.ToString(),
                State = state.ToString(),
                Exception = exception?.ToString()??"",
            };
            _option.Storage.WriteFunc(logEntity);
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
        public LogLevel MinLevel { get; set; } = LogLevel.Debug;
        public CustomLogStorage Storage { get; set; } = new CustomLogStorage();
    }

    public class CustomLogStorage
    {
        public Func<CustomLogEntity, int> WriteFunc = (entity) => { Console.WriteLine(entity); return 0; };
    }

    public static class SqliteStorageExtention
    {
        public sealed class SqliteStorageOption
        {
            public string DbConnectionString { get; set; }
            public string DbName { get; set; } = "CustomLog";
        }

        public static void UseSqliteStorage(this CustomLogStorage logStorage, string connectstring)
        {
            var option = new SqliteStorageOption();
            option.DbConnectionString = connectstring;
            Func<CustomLogEntity, int> WriteFunc = (entity) =>
            {
                return WriteToStorage(option, entity);
            };
            logStorage.WriteFunc = WriteFunc;
        }

        public static void UseSqliteStorage(this CustomLogStorage logStorage, SqliteStorageOption option)
        {
            Func<CustomLogEntity, int> WriteFunc = (entity) =>
            {
                return WriteToStorage(option, entity);
            };
            logStorage.WriteFunc = WriteFunc;
        }

        private static int WriteToStorage(SqliteStorageOption optioin, CustomLogEntity logEntity)
        {
            using (var connection = new SqliteConnection(optioin.DbConnectionString))
            {
                connection.Open();
                string isexistsql = $"SELECT count(*) FROM sqlite_master WHERE type='table' AND name = '{optioin.DbName}'";
                var command = new SqliteCommand(isexistsql, connection);
                var count = command.ExecuteNonQuery();
                if (count != 1)
                {
                    string createsql = $"create table {optioin.DbName} (Id INTEGER Primary Key, LogLevel TEXT,EventId TEXT,State TEXT,Exception TEXT,AppendText TEXT)";
                    command.CommandText = createsql;
                    command.ExecuteNonQuery();
                }
                command.CommandText = $"insert into {optioin.DbName} (LogLevel,EventId,State,Exception,AppendText) Values('{logEntity.LogLevel}','{logEntity.EventId}','{logEntity.State}','{logEntity.Exception}','{logEntity.AppendText}')";
                var i = command.ExecuteNonQuery();
                return i;
            }
        }
    }
}