using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using TPro.Common.Extentions;

namespace TPro.Common.CustomSql.SystemSql
{
    public class SQLiteHelper : IDisposable
    {
        private readonly SQLiteConnection _connection;

        public SQLiteHelper(string connectstring)
        {
            _connection = new SQLiteConnection(connectstring);
        }

        public SQLiteHelper(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public SQLiteHelper(IOptions<SQLiteConnection> options)
        {
            _connection = options.Value;
        }

        /// <summary>
        /// 创建一个数据库文件。如果存在同名数据库文件，则会覆盖。
        /// </summary>
        /// <param name="dbName"></param>
        public static void CreateDbFile(string dbName)
        {
            if (!string.IsNullOrEmpty(dbName))
            {
                try
                {
                    SQLiteConnection.CreateFile(dbName);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 判断表是否存在
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool TableExist(string table)
        {
            var CommandText = "SELECT COUNT(*) FROM sqlite_master where type='table' and name='" + table + "';";
            var rtable = ExecuteQuery(CommandText);
            return rtable.Rows.Count > 0;
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="dbPath">指定数据库文件</param>
        /// <param name="tableName">表名称</param>
        public void CreateTable(string table, List<string> Columns)
        {
            var Column = "";
            foreach (var item in Columns)
            {
                Column += item + ",";
            }
            Column = Column[..^1];
            var sql = " CREATE TABLE " + table + "(" + Column + ")";
            ExecuteNonQuery(sql);
        }
        public void CreateTable<T>(string table, T entity) where T : class
        {
            var columns = GetColumns(entity);
            CreateTable(table, columns);
        }

        /// <summary>
        /// 获取类的属性名称和类型
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<string> GetColumns<T>(T model) where T : class
        {
            var Columns = new List<string>();
            var properties = model.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Length <= 0)
            {
                throw new Exception("类属性长度为零");
            }
            foreach (var item in properties)
            {
                var Name = "";
                if (item.Name.ToLower() != "id")
                {
                    if (item.PropertyType.ToString().Contains("Time") || item.PropertyType.ToString().Contains("String"))
                    {
                        Name = item.Name + " " + " TEXT default NULL";
                    }
                    else
                    {
                        Name = item.Name + " " + item.PropertyType.ToString().Split('.').Last().Replace("]", "") + "(10, 4) default NULL";
                    }
                }
                else
                {
                    Name = item.Name + "  INTEGER PRIMARY KEY autoincrement";
                }
                Columns.Add(Name);
            }
            return Columns;
        }

        public int Add<T>(string table, T t) where T : class
        {
            var properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var fileds = properties.Where(e => e.Name.ToLower() != "id").Select(e => e.Name).ToArray();
            var filedstr = string.Join(",", fileds);
            var values = properties.Where(e => e.Name.ToLower() != "id").Select(e => e.PropertyType.IsValueType ? e.GetValue(t) : $"'{e.GetValue(t)}'").ToArray();
            var valuestr = string.Join(",", values);
            var sql = $"INSERT INTO {table} ({filedstr}) VALUES ({valuestr})";
            return ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 对SQLite数据库执行增删改操作，返回受影响的行数。
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, params SQLiteParameter[] parameters)
        {
            int affect = 0;
            if (_connection.State != ConnectionState.Open) _connection.Open();
            using (var command = new SQLiteCommand(sql, _connection))
            {
                try
                {
                    if (parameters.Length != 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    affect = command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            _connection.Close();
            return affect;
        }

        public DataTable ExecuteQuery(string sql, params SQLiteParameter[] parameters)
        {
            if (_connection.State != ConnectionState.Open) _connection.Open();
            var data = new DataTable();
            using (var command = new SQLiteCommand(sql, _connection))
            {
                if (parameters.Length != 0)
                {
                    command.Parameters.AddRange(parameters);
                }
                var adapter = new SQLiteDataAdapter(command);
                try { adapter.Fill(data); }
                catch (Exception) { throw; }
            }
            _connection.Close();
            return data;
        }

        public SQLiteDataReader ExecuteReader(string sql, params SQLiteParameter[] parameters)
        {
            if (_connection.State != ConnectionState.Open) _connection.Open();
            var command = new SQLiteCommand(sql, _connection);
            try
            {
                if (parameters.Length != 0)
                {
                    command.Parameters.AddRange(parameters);
                }
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception) { throw; }
        }

        public List<T> ExecuteQueryEntity<T>(string sql, params SQLiteParameter[] parameters) where T : class, new()
        {
            if (_connection.State != ConnectionState.Open) _connection.Open();

            using (var command = new SQLiteCommand(sql, _connection))
            {
                try
                {
                    if (parameters.Length != 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return command.ExecuteReader(CommandBehavior.CloseConnection).MapToTEntities<T>();
                }
                catch (Exception) { throw; }
            }
        }
        public void Close()
        {
            _connection.Close();
        }
        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
        }
    }
}