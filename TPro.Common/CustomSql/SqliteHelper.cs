using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPro.Common.CustomSql
{
    public class SQLiteHelper
    {
        private static object syncObj = new object();
        private static SQLiteHelper instance = null;
        public static SQLiteHelper GetInstance()
        {
            if (instance == null)
            {
                lock (syncObj)
                {
                    if (instance == null)
                    {
                        instance = new SQLiteHelper();
                    }
                }
            }
            return instance;
        }

        public string dbPath;
        public SqliteConnection sqliteConn;
        public string MinuteData = "MinData";
        public string RealData = "RealData";
        public SQLiteHelper()
        {
            dbPath = System.AppDomain.CurrentDomain.BaseDirectory + "MinData.db";
            sqliteConn = new SqliteConnection("data source=" + System.AppDomain.CurrentDomain.BaseDirectory + "MinData.db");
        }

        /// <summary>
        /// 判断表是否存在
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool TableExist(string table)
        {
            if (sqliteConn.State == ConnectionState.Closed) sqliteConn.Open();
            SqliteCommand mDbCmd = sqliteConn.CreateCommand();
            mDbCmd.CommandText = "SELECT COUNT(*) FROM sqlite_master where type='table' and name='" + table + "';";
            int row = Convert.ToInt32(mDbCmd.ExecuteScalar());
            sqliteConn.Close();
            if (0 < row)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="dbPath">指定数据库文件</param>
        /// <param name="tableName">表名称</param>
        public void CreateTable(string table, List<string> Columns)
        {
            if (sqliteConn.State != System.Data.ConnectionState.Open) sqliteConn.Open();
            string Column = "";
            for (int i = 0; i < Columns.Count; i++)
            {
                Column += Columns[i] + ",";
            }
            Column = Column.Substring(0, Column.Length - 1);
            SqliteCommand cmd = new SqliteCommand();
            cmd.Connection = sqliteConn;
            cmd.CommandText = " CREATE TABLE " + table + "(" + Column + ")";
            cmd.ExecuteNonQuery();
            sqliteConn.Close();
        }
        /// <summary>
        /// 添加字段
        /// </summary>
        /// <param name="Colms"></param>
        public void CreateColunm(string table, List<string> Colms)
        {
            if (sqliteConn.State != System.Data.ConnectionState.Open) sqliteConn.Open();
            SqliteCommand cmd = new SqliteCommand();
            var sql = "select sql from sqlite_master where tbl_name='" + table + "' and type='table';";
            cmd.Connection = sqliteConn;
            cmd.CommandText = sql;
            var com = cmd.ExecuteScalar();
            for (var i = 0; i < Colms.Count; i++)
            {
                try
                {
                    if (!com.ToString().Contains(Colms[i]))
                    {
                        var sql2 = "alter table " + table + " add column " + Colms[i] + ";";
                        cmd.Connection = sqliteConn;
                        cmd.CommandText = sql2;
                        cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                }
            }
            sqliteConn.Close();
        }
        /// <summary>
        /// 获取类的属性名称和类型
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<string> GetColumns<T>(T model) where T : class
        {
            List<string> Columns = new List<string>();
            var properties = model.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            if (properties.Length <= 0)
            {
                throw new Exception("类属性长度为零");
            }
            foreach (var item in properties)
            {
                string Name = "";
                if (item.Name.ToLower() != "id")
                {
                    if (item.PropertyType.ToString().Contains("Time") || item.PropertyType.ToString().Contains("String"))
                    {
                        Name = item.Name + " " + " varchar(100) default NULL";
                    }
                    else
                    {
                        Name = item.Name + " " + item.PropertyType.ToString().Split('.').Last().Replace("]", "") + "(10, 4) default NULL";
                    }
                }
                else
                {
                    Name = item.Name + "  integer PRIMARY KEY autoincrement";
                }
                Columns.Add(Name);
            }
            return Columns;
        }

        public string[] GetKeys<T>(T model) where T : class
        {
            System.Reflection.PropertyInfo[] properties = model.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            if (properties.Length <= 0)
            {
                throw new Exception("类属性长度为零");
            }
            string[] Columns = new string[properties.Length - 1];
            int con = 0;
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                if (item.Name != "ID")
                {
                    Columns[con++] = item.Name;
                }
            }
            return Columns;
        }
        public string AddString(string TableName, string[] keys, string[] values)
        {
            string keys_string = "(" + keys[0];
            string value_string = "('" + values[0] + "'";

            for (int i = 1; i < keys.Length; i++)
            {
                keys_string += "," + keys[i];
            }
            for (int i = 1; i < values.Length; i++)
            {
                value_string += ",'" + values[i] + "'";
            }
            keys_string += ")";
            value_string += ")";
            string sql = string.Format("INSERT INTO " + TableName + " {0} VALUES {1} ;", keys_string, value_string);
            return sql;
        }
        public string AddString(string TableName, Dictionary<string, decimal> keyValues)
        {
            string keys_string = "( Time ";
            string value_string = "('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:00") + "'";
            foreach (var item in keyValues)
            {
                keys_string += "," + item.Key;
                value_string += ",'" + item.Value + "'";
            }
            keys_string += ")";
            value_string += ")";
            string sql = string.Format("INSERT INTO " + TableName + " {0} VALUES {1} ;", keys_string, value_string);
            return sql;
        }

        public string AddStringReal(string TableName, Dictionary<string, decimal> keyValues)
        {
            string keys_string = "( Time ";
            string value_string = "('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            foreach (var item in keyValues)
            {
                keys_string += "," + item.Key;
                value_string += ",'" + item.Value + "'";
            }
            keys_string += ")";
            value_string += ")";
            string sql = string.Format("INSERT INTO " + TableName + " {0} VALUES {1} ;", keys_string, value_string);
            return sql;
        }

        private object LockObj = new object();
        public bool SqliteDbTransaction(string sqlString)
        {
            lock (LockObj)
            {
                if (sqliteConn.State == ConnectionState.Closed) sqliteConn.Open();
                DbTransaction trans = sqliteConn.BeginTransaction();
                try
                {
                    using (SqliteCommand cmd = new SqliteCommand("VACUUM", sqliteConn))
                    {
                        int rows = 0;
                        cmd.CommandText = sqlString;
                        rows = cmd.ExecuteNonQuery();
                        trans.Commit();//提交事务
                        sqliteConn.Close();
                        return rows > 0;
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();//回滚事务
                    sqliteConn.Close();
                    return false;
                }
            }
        }
    }
}
