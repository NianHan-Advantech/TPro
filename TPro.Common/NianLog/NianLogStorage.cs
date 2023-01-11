using System;
using TPro.Common.CustomSql.SystemSql;

namespace TPro.Common.NianLog
{
    public sealed class NianLogStorage : INianLogStorage
    {
        public void WriteToStorage(NianLogEntity entity)
        {
            Console.WriteLine(entity.ToString());
        }
    }

    public static class NianLogSqliteStorageExtention
    {
        private static NianLogSqliteOption _option = new();

        public static void UseSqliteStorage(this NianLogConfiguration configuration, string connectionstring)
        {
            _option.ConnectionString = connectionstring;
            configuration.LogStorage = new NianLogSqliteStorage();
        }

        public static void UseSqliteStorage(this NianLogConfiguration configuration, NianLogSqliteOption option)
        {
            _option = option;
            configuration.LogStorage = new NianLogSqliteStorage();
        }

        public sealed class NianLogSqliteStorage : INianLogStorage
        {
            public void WriteToStorage(NianLogEntity entity)
            {
                using (var sqlitehelper = new SQLiteHelper(_option.ConnectionString))
                {
                    var isexist = sqlitehelper.TableExist(_option.TableName);
                    if (isexist)
                    {
                        sqlitehelper.Add(_option.TableName, entity);
                    }
                    else if (!isexist && _option.IsAutoCreateTable)
                    {
                        sqlitehelper.CreateTable(_option.TableName, entity);
                        sqlitehelper.Add(_option.TableName, entity);
                    }
                }
            }
        }

        public sealed class NianLogSqliteOption
        {
            public string ConnectionString { get; set; }
            public string TableName { get; set; } = "NianLog";
            public bool IsAutoCreateTable { get; set; } = true;
        }
    }
}