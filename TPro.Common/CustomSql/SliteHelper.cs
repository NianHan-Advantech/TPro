using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPro.Common.CustomSql
{
    public static class SliteHelper
    {
        public static int WriteToDb(string connectstring = "", string tablename = "")
        {
            using (var connection = new SqliteConnection(connectstring))
            {
                connection.Open();
                string isexistsql = $"SELECT count(*) FROM sqlite_master WHERE type='table' AND name = '{tablename}';";
                var command = new SqliteCommand(isexistsql, connection);
                var count = command.ExecuteNonQuery();
                if (count == 1)
                {

                }
                else
                {
                    string createsql = $"create table {tablename} (name varchar(20), score int)";
                    command.CommandText = createsql;
                    command.ExecuteNonQuery();
                }
            }
            return 0;
        }
        public static int CreateAndWrite<T>(string connectstring, string tablename, T t)
        {
            using (var connection = new SqliteConnection(connectstring))
            {
                connection.Open();
                string isexistsql = $"SELECT count(*) FROM sqlite_master WHERE type='table' AND name = '{tablename}';";
                var command = new SqliteCommand(isexistsql, connection);
                var count = command.ExecuteNonQuery();
                if (count == 1)
                {

                }
                else
                {
                    string createsql = $"create table {tablename} (name varchar(20), score int)";
                    command.CommandText = createsql;
                    command.ExecuteNonQuery();
                }
            }
            return 0;
        }
        public static void TestConnect()
        {
            using (var connection = new SqliteConnection("Data Source=E:\\MvcTest\\hangfire.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
        SELECT name
        FROM user
        WHERE id = $id
    ";
                command.Parameters.AddWithValue("$id", 1);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);

                        Console.WriteLine($"Hello, {name}!");
                    }
                }
            }
        }
    }
}
