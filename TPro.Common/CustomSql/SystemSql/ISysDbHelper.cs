using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace TPro.Common.CustomSql.SystemSql
{
    public interface ISysDbHelper<TConnection> : IDisposable where TConnection : IDbConnection, new()
    {
        IDbConnection _dbConnection { get; set; }

        int ExecuteNonQuery(string querystr, params IDbDataParameter[] parameter);

        void Close();
    }

    public abstract class SysDbHelper<TConnection> : ISysDbHelper<TConnection> where TConnection : IDbConnection, new()
    {
        public string _keyLowerName { get; set; } = "id";
        public IDbConnection _dbConnection { get; set; } = new TConnection();

        public SysDbHelper(string connectionString)
        {
            _dbConnection.ConnectionString = connectionString;
        }

        public SysDbHelper(TConnection connection)
        {
            _dbConnection = connection;
        }

        public virtual int ExecuteNonQuery(string querystr, params IDbDataParameter[] parameter)
        {
            if (_dbConnection.State != ConnectionState.Open) _dbConnection.Open();
            IDbCommand command = _dbConnection.CreateCommand();
            command.CommandText = querystr;
            if (parameter.Length != 0) command.Parameters.Add(parameter);
            return command.ExecuteNonQuery();
        }

        public virtual int Add<T>(T entity) where T : class
        {
            return 1;
        }
        public virtual IEnumerable<string> GetFields<T>(T entity) where T : class
        {
            var properties = entity.GetType().GetProperties();
            foreach (var item in properties)
            {
                if (item.Name.ToLower() != _keyLowerName)
                {
                    yield return item.Name;
                }
            }
        }

        protected abstract List<string> GetColumns<T>(T model);

        public void Close()
        {
            _dbConnection?.Close();
        }

        public void Dispose()
        {
            _dbConnection?.Dispose();
        }
    }

    public class MySqlDbHelper : SysDbHelper<SQLiteConnection>
    {
        public MySqlDbHelper(SQLiteConnection connection) : base(connection)
        {
        }

        protected override List<string> GetColumns<T>(T model)
        {
            var list = new List<string>();
            return list;
        }
    }
}